using ApartmentScrapper.Domain.Data;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace ApartmentScrapper.Infrastructure.Data.External.Argenprop
{
    public static class HtmlNodeToDomainMapper
    {
        private static string _baseLink = "https://www.argenprop.com/apartment-seeker--{id}";

        public static ApartmentSearchResult ToDomain(this HtmlNode node)
        {
            var result = new ApartmentSearchResult { Source = "Argenprop" };
            var row = node.CssSelect("a").First();

            result.Id = row.Attributes.First(x => x.Name == "data-item-card").Value;

            var detailsBox = row.CssSelect(".card__details-box").First();

            var monetaryValuesAndTitle = detailsBox
                .CssSelect(".card__details-box-top")
                .First()
                .CssSelect(".card__monetary-values")
                .First();

            result.Title =
                monetaryValuesAndTitle
                .CssSelect(".card__address")
                .First()
                .InnerHtml;

            result.Permalink = _baseLink.Replace("{id}", result.Id);

            result.Thumbnail = row.CssSelect(".card__photos-box")
                .First()
                .CssSelect(".card__carousel")
                .First()
                .CssSelect(".card__photos")
                .First()
                .CssSelect("li")
                .First()
                .CssSelect("img")
                .First()
                .Attributes
                .FirstOrDefault(x => x.Name == "data-src")
                .Value;

            var priceRow = monetaryValuesAndTitle.CssSelect(".card__price");

            result.Currency = priceRow.CssSelect(".card__currency").First().InnerHtml == "$" ? "ARS" : "USD";

            result.Price = priceRow.First().ChildNodes[2].InnerHtml.GetNumericPart();

            var mainAttrs = detailsBox
                .CssSelect(".card__main-features")
                .First();

            result.CoveredArea = mainAttrs
                .CssSelect("li")
                .First()
                .CssSelect("span")
                .First()
                .InnerHtml
                .GetNumericPart();

            result.RoomsCount = (int)(mainAttrs
                .CssSelect("li")
                .ToArray()[2]
                .CssSelect("span")
                .First()
                .InnerHtml
                .GetNumericPart());

            result.Neighborhood = monetaryValuesAndTitle
                .CssSelect(".card__title--primary")
                .Last()
                .InnerHtml
                .Split(",")[0];

            return result;
        }
    }
}