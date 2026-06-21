namespace Osanebi.WebBlazor.Service
{
    public interface IFileService
    {
        Task<string> ReadFileAsync(string path);
    }
}
