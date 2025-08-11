namespace Devfunda.Helpers
{
    public static class FileUploadHelper
    {

           public static async Task<string> UploadImageAsync(IFormFile file, string folderName, IWebHostEnvironment env)
            {
                if (file == null || file.Length == 0) return null;

                var uploadsFolder = Path.Combine(env.WebRootPath, "images", folderName);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the relative path to save in DB
                return $"/images/{folderName}/{uniqueFileName}";
            }
        }
    }


