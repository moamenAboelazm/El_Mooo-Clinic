using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string webRootPath = _webHostEnvironment.WebRootPath;

            string uploadPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string fullFilePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/{folderName}/{uniqueFileName}";
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            if (filePath.StartsWith("/"))
            {
                filePath = filePath.Substring(1);
            }

            string webRootPath = _webHostEnvironment.WebRootPath;
            string fullPath = Path.Combine(webRootPath, filePath.Replace("/", "\\"));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}