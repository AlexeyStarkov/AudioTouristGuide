using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AudioTouristGuide.WebAPI.SwaggerTools
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == "apivaluesuploadpost")
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "uploadedFile",
                    In = ParameterLocation.Query,
                    Description = "Upload File",
                    Required = true,
                    Style = ParameterStyle.Form
                });
            }
        }
    }
}
