using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace MBran.Umbraco
{
    [PluginController("MBranApi")]
    public class CountryController : UmbracoApiController
    {
        public IEnumerable<Country> GetAll()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var countries = cultures
                .Select(culture =>
                    new RegionInfo(culture.LCID)
                ).Select(region =>
                    new Country
                    {
                        Code = region.TwoLetterISORegionName,
                        Name = region.EnglishName
                    }
                )
                .OrderBy(country => country.Name)
                .GroupBy(country => country.Code)
                .Select(country => country.FirstOrDefault());
            
            return countries;
        }
    }
}
