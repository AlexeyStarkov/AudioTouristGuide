using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Location
{
    [ParseClassName("Country")]
    public class CountryDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();
    }
}
