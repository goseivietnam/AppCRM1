using System.Threading.Tasks;

namespace AppCRM.Controls
{
    public interface IFilePicker
    {
        Task<SJFileStream> GetImageStreamAsync();

        Task<SJFileStream> GetFileStreamAsync(string[] mimeTypes);
    }
}
