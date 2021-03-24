using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessSystem.CRM.Logics.Contexts.CdnContext
{
    public class CdnContext
    {
        private string _cdnPath;
        private string _webRootPath;
        public CdnContext (string cdnPath, string webRootPath)
        {
            _cdnPath = cdnPath;
            _webRootPath = webRootPath;
        }

        private string createNewFileName()
        {
            Guid giudName = Guid.NewGuid();
            return giudName.ToString();
        }

        public async Task<string> Upload(IFormFile document)
        {
            if (document == null)
                return string.Empty;

            string filename = createNewFileName() + Path.GetExtension(document.FileName);
            string filepath = _cdnPath + filename;

            using (var fileStream = new FileStream(_webRootPath + filepath, FileMode.Create))
            {
                await document.CopyToAsync(fileStream);
            }

            return filename;
        }
    }
}
