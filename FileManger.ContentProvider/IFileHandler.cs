namespace FileManager.ContentProvider
{
    public interface IFileHandler<TKey>
    {
        Task CreateAsync(TKey id, StreamInfo fileContent, CancellationToken cancellationToken);

        bool Exists(TKey id);

        FileStream OpenRead(TKey id);

        Task<byte[]> ReadAllBytesAsync(TKey id, CancellationToken cancellationToken);

        void Delete(TKey id);

        Task<string> GetHashAsync(TKey id, CancellationToken cancellationToken);
    }
}
