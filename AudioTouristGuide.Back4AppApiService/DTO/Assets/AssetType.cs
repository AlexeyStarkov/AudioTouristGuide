using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Assets
{
    internal class AssetType : ParseObject
    {
        [ParseFieldName("Name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}
