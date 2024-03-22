using System.Security.Cryptography;

namespace FileManager.ContentProvider
{
    public class FileHandler<TKey> : IFileHandler<TKey>
    {
        private readonly string _serverPath;

        public FileHandler()
        {
            _serverPath = Configuration.ServerPath;
        }

        public Task CreateAsync(TKey id, StreamInfo streamInfo, CancellationToken cancellationToken)
        {
            using (var fileStream = File.Create(GetPath(id)))
            {
                return streamInfo.Stream.CopyToAsync(fileStream, cancellationToken);
            }
        }

        public bool Exists(TKey id)
        {
            return File.Exists(GetPath(id));
        }

        public FileStream OpenRead(TKey id)
        {
            return File.OpenRead(GetPath(id));
        }

        public Task<byte[]> ReadAllBytesAsync(TKey id, CancellationToken cancellationToken)
        {
            return File.ReadAllBytesAsync(GetPath(id), cancellationToken);
        }

        public void Delete(TKey id)
        {
            File.Delete(GetPath(id));
        }

        public async Task<string> GetHashAsync(TKey id, CancellationToken cancellationToken)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = OpenRead(id))
                {
                    var hashBytes = await md5.ComputeHashAsync(stream, cancellationToken);
                    var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    return hash;
                }
            }
        }

        private string GetPath(TKey id)
        {
            return Path.Combine(_serverPath, id?.ToString() ?? throw new InvalidOperationException());
        }
    }
}
