using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class ImageManagmentService : IImageManagmentService
    {   
        private readonly IFileProvider fileProvider;
        public ImageManagmentService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImagesAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot","Images", src);
            //var ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), src);
            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var item in files)
            {
                if(item.Length> 0)
                {
                    var ImageName = item.FileName;
                    var imageSrc = $"Images/{src}/{ImageName}";
                    var filePath = Path.Combine(ImageDirectory, ImageName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(imageSrc);
                }
            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var Info = fileProvider.GetFileInfo(src);
            var filePath = Info.PhysicalPath;
            if (Info.Exists) // only use File.Delete(filePath); and remove if else
            {
                File.Delete(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found", filePath);
            }
        }
    }
}
