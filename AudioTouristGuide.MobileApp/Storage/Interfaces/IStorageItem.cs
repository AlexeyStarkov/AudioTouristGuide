using LiteDB;

namespace AudioTouristGuide.MobileApp.Storage.Interfaces
{
    public interface IStorageItem
    {
        [BsonId]
        long ID { get; set; }
    }
}
