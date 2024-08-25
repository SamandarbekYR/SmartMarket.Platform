using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SmartMarket.Service.Helpers;
using SmartMarket.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Services.Common
{
    public class FileService : IFileService
    {
        private readonly string MEDIA = "Media";
        private readonly string IMAGES = "Images";
        private readonly string ROOTPATH;

        public FileService(IWebHostEnvironment env)
        {
            ROOTPATH = env.WebRootPath;
        }
        public async Task<bool> DeleteImageAsync(string subpath)
        {
            string path = Path.Combine(ROOTPATH, subpath);
            
            if(File.Exists(path))
            {
                await Task.Run(() =>
                {
                    File.Delete(path);
                });
                
                return true;
            }

            return false;
        }
        public async Task<string> UploadImageAsync(IFormFile image, string rootpath)
        {
            // 1. Tasvir uchun yangi nom yaratadi.
            string newImageName = ImageHelper.MakeImageName(image.FileName);

            // 2. Tasvir saqlanadigan yo'lni yaratadi.
            string subPath = Path.Combine(MEDIA, IMAGES, rootpath, newImageName);

            // 3. Tasvirni saqlash uchun to'liq yo'lni yaratadi.
            string path = Path.Combine(ROOTPATH, subPath);

            try
            {
                // 4. Tasvirni saqlash uchun FileStream obyektini yaratadi va faylni yaratadi.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // 5. Tasvirni faylga nusxalash.
                    await image.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // Xatolik bilan ishlash logikasi (logga yozish, qayta urinish va h.k.).
                throw new Exception("Image upload failed.", ex);
            }

            // 6. Tasvir saqlangan yo'lni qaytaradi.
            return subPath;
        }
    }
}
