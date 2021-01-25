using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Assets
{
    [ParseClassName("AssetType")]
    internal class AssetTypeDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();
    }
}
