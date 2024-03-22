using FileManager.ContentProvider;
using FileManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManagerController : ControllerBase
    {
        private readonly IContentProvider<FileName> _provider;

        public FileManagerController(IContentProvider<FileName> provider)
        {
            _provider = provider;
        }

        [HttpPost]
        public async Task<IActionResult> Store(IFormFile file)
        {
            var result = await _provider.StoreAsync(new FileName(file.FileName), new StreamInfo { Stream = file.OpenReadStream() }, default);

            if (result.Fail)
                return BadRequest("Error in file upload.");

            return Ok("File uploaded successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> Exists(string fileName)
        {
            var result = await _provider.ExistsAsync(new FileName(fileName), default);

            if (result.Fail)
                return BadRequest("Error.");

            if (!result.ResultObject)
                return Ok("The file does not exists.");

            return Ok("The file exists.");
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string fileName)
        {
            var result = await _provider.GetBytesAsync(new FileName(fileName), default);

            if (result.Fail || result.ResultObject == null)
                return BadRequest("Error.");

            return File(result.ResultObject.ToArray(), "application/octet-stream", fileName);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string fileName)
        {
            var result = await _provider.DeleteAsync(new FileName(fileName), default);

            if (result.Fail)
                return BadRequest("Error.");

            return Ok("The file is deleted.");
        }

        [HttpGet("GetHash")]
        public async Task<IActionResult> GetHash(string fileName)
        {
            var result = await _provider.GetHashAsync(new FileName(fileName), default);

            if (result.Fail || result.ResultObject == null)
                return BadRequest("Error.");

            return Ok(result.ResultObject);
        }
    }
}
