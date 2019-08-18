using System.IO;
using System.Text.RegularExpressions;

namespace AudioTouristGuide.WebAPI.Tools
{
    public static class ApplicationConstants
    {
        public static Regex FileNameRegex = new Regex("^[a-zA-Z0-9]([\\.\\-_]|[a-zA-Z0-9])+");
        public static string ToursAssetsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "tours");
        public static string PlacesAssetsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "places");
        public static string TempDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "temp");
    }
}
