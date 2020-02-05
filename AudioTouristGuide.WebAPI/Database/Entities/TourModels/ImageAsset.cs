using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioTouristGuide.WebAPI.Database.Entities.TourModels
{
    public class ImageAsset : AssetBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ImageAssetId { get; set; }
    }
}
