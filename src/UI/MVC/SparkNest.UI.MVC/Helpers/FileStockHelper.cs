using SparkNest.UI.MVC.Models;

namespace SparkNest.UI.MVC.Helpers
{
    public class FileStockHelper
    {
        ServiceApiSettings _serviceApiSettings;

        public FileStockHelper(ServiceApiSettings serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings;
        }

        public string GetFileStockUrl(string fileUrl)
        {
            return $"{_serviceApiSettings.FileStockUri}/{fileUrl}";
        }
    }
}
