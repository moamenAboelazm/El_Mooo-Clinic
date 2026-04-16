using Microsoft.AspNetCore.Http;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);

        void DeleteFile(string filePath);
    }
}