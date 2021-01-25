using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using AudioTouristGuide.Back4AppApiService.DTO.Enums;
using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Assets
{
    [ParseClassName("FileAsset")]
    public class FileAssetDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();

        [ParseFieldName("Description")]
        public string Description => GetProperty<string>();

        [ParseFieldName("File")]
        public ParseFile File => GetProperty<ParseFile>();

        [ParseFieldName("FileSize")]
        public long FileSizeBytes => GetProperty<long>();

        [ParseFieldName("Type")]
        public AssetTypes AssetType => GetProperty<AssetTypes>();
    }
}
