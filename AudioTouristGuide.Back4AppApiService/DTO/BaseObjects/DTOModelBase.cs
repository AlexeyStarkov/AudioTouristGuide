using Parse;
using System;

namespace AudioTouristGuide.Back4AppApiService.DTO.BaseObjects
{
    public class DTOModelBase : ParseObject
    {
        [ParseFieldName("objectId")]
        public string Id => GetProperty<string>();

        [ParseFieldName("updatedAt")]
        public DateTime LastChange => GetProperty<DateTime>();
    }
}
