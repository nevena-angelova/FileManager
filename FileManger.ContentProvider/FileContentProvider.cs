using OneBitSoftware.Utilities;

namespace FileManager.ContentProvider
{
    public class FileContentProvider<TKey> : IContentProvider<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly IFileHandler<TKey> _file;

        public FileContentProvider(IFileHandler<TKey> file)
        {
            _file = file;
        }

        public async Task<OperationResult> StoreAsync(TKey id, StreamInfo streamInfo, CancellationToken cancellationToken)
        {
            try
            {
                await _file.CreateAsync(id, streamInfo, cancellationToken);

                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                return OperationResult.FromException(ex);
            }
        }

        public async Task<OperationResult<bool>> ExistsAsync(TKey id, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await Task.Run(() => _file.Exists(id), cancellationToken);

                return new OperationResult<bool>(exists, null);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.FromException(ex);
            }
        }

        public async Task<OperationResult<StreamInfo>> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            try
            {
                var fileStream = await Task.Run(() => _file.OpenRead(id), cancellationToken);

                return new OperationResult<StreamInfo>(new StreamInfo { Stream = fileStream });
            }
            catch (Exception ex)
            {
                return OperationResult<StreamInfo>.FromException(ex);
            }
        }

        public async Task<OperationResult<byte[]>> GetBytesAsync(TKey id, CancellationToken cancellationToken)
        {
            try
            {
                var fileBytes = await _file.ReadAllBytesAsync(id, cancellationToken);

                return new OperationResult<byte[]>(fileBytes);
            }
            catch (Exception ex)
            {
                return OperationResult<byte[]>.FromException(ex);
            }
        }

        public async Task<OperationResult> UpdateAsync(TKey id, StreamInfo fileContent, CancellationToken cancellationToken)
        {
            try
            {
                await _file.CreateAsync(id, fileContent, cancellationToken);

                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                return OperationResult.FromException(ex);
            }
        }

        public async Task<OperationResult> DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await Task.Run(() => _file.Exists(id), cancellationToken);

                if (!exists)
                    throw new FileNotFoundException("File not found");

                await Task.Run(() => _file.Delete(id), cancellationToken);

                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                return OperationResult.FromException(ex);
            }
        }

        public async Task<OperationResult<string>> GetHashAsync(TKey id, CancellationToken cancellationToken)
        {
            try
            {
                var hash = await _file.GetHashAsync(id, cancellationToken);

                return new OperationResult<string>(hash);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FromException(ex);
            }
        }
    }
}
