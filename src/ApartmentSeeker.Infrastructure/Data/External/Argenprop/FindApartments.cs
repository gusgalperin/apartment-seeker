using ApartmentScrapper.Domain.Data;
using ApartmentScrapper.Utils.Logger;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace ApartmentScrapper.Infrastructure.Data.External.Argenprop
{
    public class FindApartments : IFindApartments
    {
        private readonly string _baseUrl =
            "https://www.argenprop.com/departamento-y-casa-y-ph-alquiler-localidad-capital-federal-hasta-450000-pesos-solo-orden-masnuevos-pagina-{page}";
        
        private readonly ILogger<FindApartments> _logger;

        public FindApartments(ILogger<FindApartments> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ApartmentSearchResult>> FindAsync()
        {
            var web = new HtmlWeb();
            var allItems = new List<HtmlNode>();

            var pageNumber = 1;

            var items = await GetPageContentAsync(web, pageNumber);

            while (items.Any() && pageNumber < 10)
            {
                allItems.AddRange(items);
                pageNumber++;

                items = await GetPageContentAsync(web, pageNumber);
            }

            return allItems.Select(x => 
            {
                try
                {
                    return x.ToDomain();
                }
                catch (Exception ex)
                {
                    _logger.LogError("error while mapping to domain: " + ex.Message);
                    return new ApartmentSearchResult();
                }
            }).ToList();
        }

        private async Task<IEnumerable<HtmlNode>> GetPageContentAsync(HtmlWeb web, int pageNumber)
        {
            var page = await web.LoadFromWebAsync(_baseUrl.Replace("{page}", pageNumber.ToString()));

            return page.DocumentNode.CssSelect(".listing__item ");
        }
    }
}