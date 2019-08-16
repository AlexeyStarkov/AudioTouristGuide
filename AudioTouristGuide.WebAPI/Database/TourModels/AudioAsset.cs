using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioTouristGuide.WebAPI.Database.TourModels
{
    public class AudioAsset : AssetBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AudioAssetId { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
