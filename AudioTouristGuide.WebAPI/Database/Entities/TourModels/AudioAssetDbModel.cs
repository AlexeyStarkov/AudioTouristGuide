namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class AudioAssetDbModel : AssetDbModelBase
    {
        public long PlaceDbModelId { get; set; }
        public PlaceDbModel PlaceDbModels { get; set; }
    }
}
