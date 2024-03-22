namespace FileManager.ContentProvider
{
    public class StreamInfo
    {
        public long Length { get; set; }

        public required Stream Stream { get; set; }

        /// <summary>
        /// Gets the length of the stream info as an unsigned integer.
        /// If for some reason the current instance's <see cref="Length"/> is lower than 0, this value will be Zero.
        /// </summary>
        public ulong UnsignedLength => this.Length < 0 ? 0 : (ulong)Length;
    }
}
