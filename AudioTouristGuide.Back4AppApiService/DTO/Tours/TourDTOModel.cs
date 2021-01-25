using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using AudioTouristGuide.Back4AppApiService.DTO.Location;
using AudioTouristGuide.Back4AppApiService.DTO.Members;
using Parse;
using System;

namespace AudioTouristGuide.Back4AppApiService.DTO.Tours
{
    [ParseClassName("Tour")]
    public class TourDTOModel : DTOModelBase
    {
        [ParseFieldName("Name")]
        public string Name => GetProperty<string>();

        [ParseFieldName("DisplayName")]
        public string DisplayName => GetProperty<string>();

        [ParseFieldName("CoverImage")]
        public ParseRelation<FileAssetDTOModel> CoverImage => GetRelationProperty<FileAssetDTOModel>();

        [ParseFieldName("Description")]
        public string Description => GetProperty<string>();

        [ParseFieldName("Author")]
        public ParseRelation<AuthorDTOModel> Author => GetRelationProperty<AuthorDTOModel>();

        [ParseFieldName("Location")]
        public ParseRelation<LocationDTOModel> Location => GetRelationProperty<LocationDTOModel>();

        [ParseFieldName("EstimatedDurationSeconds")]
        public TimeSpan EstimatedDuration
        {
            get
            {
                var seconds = GetProperty<long>();
                return TimeSpan.FromSeconds(seconds);
            }
        }

        [ParseFieldName("GrossPrice")]
        public decimal? GrossPrice => GetProperty<decimal?>();
    }
}
