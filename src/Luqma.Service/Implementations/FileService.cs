using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
    public class FileService : IFileService
    {

        public async Task<string?> UploadFileAsync(IFormFile file)
        {

            if (file == null) return null;

            if ( file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Menuitems", fileName);
                using (var stream = File.Create(filepath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
         



        }
        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
        {
            var filenames = new List<string>();
            foreach (var file in files)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "WWWroot/images", filename);
                using (var stream = File.Create(filepath))
                {
                    await file.CopyToAsync(stream);
                }
                filenames.Add(filename);
            }
            return filenames;
        }


    }
}


