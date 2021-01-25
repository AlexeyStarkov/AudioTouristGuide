using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using AudioTouristGuide.Back4AppApiService.DTO.BaseObjects;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioTouristGuide.Back4AppApiService.DTO.Tours
{
    [ParseClassName("SlideshowTimeCode")]
    public class SlideshowTimeCodeDTOModel : DTOModelBase
    {
        [ParseFieldName("ImageFile")]
        public ParseRelation<FileAssetDTOModel> ImageFile => GetRelationProperty<FileAssetDTOModel>();

        [ParseFieldName("TimeCodeSecond")]
        public TimeSpan TimeCode
        {
            get 
            {
                var seconds = GetProperty<long>();
                return TimeSpan.FromSeconds(seconds);
            }
        }
    }
}
