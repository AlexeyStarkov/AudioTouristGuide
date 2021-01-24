using AudioTouristGuide.Back4AppApiService.DTO.Enums;
using Parse;
using System;

namespace AudioTouristGuide.Back4AppApiService.DTO.Assets
{
    [ParseClassName("FileAsset")]
    public class FileAsset : ParseObject
    {
        [ParseFieldName("Name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("File")]
        public ParseFile File
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [ParseFieldName("FileSize")]
        public AssetTypes FileSizeBytes
        {
            get { return GetProperty<AssetTypes>(); }
            set { SetProperty<AssetTypes>(value); }
        }

        [ParseFieldName("Type")]
        public AssetTypes AssetType
        {
            get { return GetProperty<AssetTypes>(); }
            set { SetProperty<AssetTypes>(value); }
        }

        [ParseFieldName("updatedAt")]
        public DateTime LastChange
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }
    }
}
