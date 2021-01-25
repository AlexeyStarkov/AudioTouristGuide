using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using AudioTouristGuide.Back4AppApiService.DTO.Members;
using Parse;

namespace AudioTouristGuide.Back4AppApiService.DTO.Tours
{
    [ParseClassName("TourSpot")]
    public class TourSpotDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();

        [ParseFieldName("DisplayName")]
        public string DisplayName => GetProperty<string>();

        [ParseFieldName("Description")]
        public string Description => GetProperty<string>();

        [ParseFieldName("Author")]
        public ParseRelation<AuthorDTOModel> Author => GetRelationProperty<AuthorDTOModel>();

        [ParseFieldName("Latitude")]
        public double Latitude => GetProperty<double>();

        [ParseFieldName("Longitude")]
        public double Longitude => GetProperty<double>();

        [ParseFieldName("AudioAsset")]
        public ParseRelation<FileAssetDTOModel> AudioAsset => GetRelationProperty<FileAssetDTOModel>();

        [ParseFieldName("Slideshow")]
        public ParseRelation<SlideshowTimeCodeDTOModel> Slideshow => GetRelationProperty<SlideshowTimeCodeDTOModel>();
    }
}
