namespace BirdPlatFormEcommerce.FileService
{
    public interface IStorageService
    {
        String GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, String fileName);

        Task DeleteFileAsync(string fileName);
    }
}
