using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Location
{
    [ParseClassName("Location")]
    public class LocationDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();

        [ParseFieldName("Country")]
        public ParseRelation<CountryDTOModel> Country => GetRelationProperty<CountryDTOModel>();
    }
}
