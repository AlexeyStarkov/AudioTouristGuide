using System.Collections.Generic;

namespace AudioTouristGuide.DTO.Models.AddNewTourZip
{
    public class AddNewTourZipPlace
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public AddNewTourZipAudio AudioTrack { get; set; }
        public IEnumerable<AddNewTourZipImage> Images { get; set; }
    }
}
