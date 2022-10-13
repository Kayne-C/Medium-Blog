using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Medium.Managers
{
    public static class FileManager
    {
        public static string GetUniqueNameAndSavePhotoToDisk(this IFormFile pictureFile, IWebHostEnvironment webHostEnviroment)
        {
            string uniqueFileName = null;
            if (pictureFile is not null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + pictureFile.Name;
                string uploadsFolder = Path.Combine(webHostEnviroment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pictureFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public static void RemoveImageFromDisk(string imageName,IWebHostEnvironment webHostEnvironment)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, imageName);
                File.Delete(filePath);
            }
        }
    }
}
