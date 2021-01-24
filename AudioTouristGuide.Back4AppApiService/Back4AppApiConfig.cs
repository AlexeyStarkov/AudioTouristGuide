using AudioTouristGuide.Back4AppApiService.DTO.Assets;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioTouristGuide.Back4AppApiService
{
    public class Back4AppApiConfig
    {
        private static bool _isInitialized;
        public static void Init()
        {
            if (!_isInitialized)
            {
                ParseObject.RegisterSubclass<FileAsset>();
                ParseObject.RegisterSubclass<AssetType>();
                ParseClient.Initialize(ParseApplicationId, ParseWindowsKey);
            }
        }
    }
}
