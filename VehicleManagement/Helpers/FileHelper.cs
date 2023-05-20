using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Helpers
{
    public static class FileHelper
    {
        public static bool checkCacheFile(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Caches", imageName);
            return File.Exists(path);
        }

        public static bool checkImageFile(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Images", imageName);
            return File.Exists(path);
        }

        public static bool checkFileInDest(string fileName, string destDir)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/" + destDir, fileName);
            return File.Exists(path);
        }
        public static void moveCacheToImages(string imageName)
        {
            var source = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Caches", imageName);
            var dest = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Images", imageName);
            if (File.Exists(source))
            {
                File.Move(source, dest);
            }
        }
        public static void moveCacheToDir(string fileName, string destinationDirectory)
        {
            var source = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Caches", fileName);
            var dest = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/" + destinationDirectory, fileName);
            if (File.Exists(source))
            {
                File.Move(source, dest);
            }
        }

        public static void removeImageFile(string imageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles/Images", imageName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
