using Microsoft.AspNetCore.Http;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.DTO
{
    public class UploadImageRequest
    {
        public IFormFile File { get; set; }
    }
}
