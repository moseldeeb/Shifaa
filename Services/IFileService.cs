namespace Shifaa.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Saves the uploaded file to the specified folder under wwwroot
        /// and returns the generated file name (guid + extension).
        /// </summary>
        Task<string> SaveFileAsync(IFormFile file, string folderPath);

        /// <summary>
        /// Deletes a file from wwwroot by folder path and file name.
        /// </summary>
        void DeleteFile(string folderPath, string fileName);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            // Generate unique file name: guid + original extension
            var fileName = Guid.NewGuid().ToString()
                         + Path.GetExtension(file.FileName).ToLower();

            var fullFolderPath = Path.Combine(
                _env.WebRootPath, folderPath);

            // Create directory if it doesn't exist
            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);

            var fullFilePath = Path.Combine(fullFolderPath, fileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return only the file name — NOT the full path
            return fileName;
        }

        public void DeleteFile(string folderPath, string fileName)
        {
            var fullFilePath = Path.Combine(
                _env.WebRootPath, folderPath, fileName);

            if (File.Exists(fullFilePath))
                File.Delete(fullFilePath);
        }
    }
}
