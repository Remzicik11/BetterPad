using System;
using System.Net;

/// <summary>
/// Just Tools By RemziStudios
/// </summary>
namespace JustTools
{
    public class MultiWebClient
    {
        private WebClient client = new WebClient();

        public void DisposeClient()
        {
            client.Dispose();
        }

        public MultiWebClient()
        {
            client.DownloadFileCompleted += DownloadFileCompleted;
        }

        private (string Url, string Path)[] DownloadFilesRequest;
        private int DownloadFilesIndex;
        public OnDownloadFileCompletedClass OnDownloadFileCompleted;
        public delegate void OnDownloadFileCompletedClass();

        public void DownloadFilesAsync(params (string Url, string Path)[] Request)
        {
            DownloadFilesRequest = Request;
            DownloadFilesIndex = 0;
            new System.Threading.Thread(() =>
            {
                DownloadFileCompleted(null, null);
            }).Start();

        }

        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs _event)
        {
            if (_event != null && _event.Error != null) { throw _event.Error; }

            while (client.IsBusy) ;

            if (DownloadFilesIndex < DownloadFilesRequest.Length)
            {
                client.DownloadFileAsync(new Uri(DownloadFilesRequest[DownloadFilesIndex].Url), DownloadFilesRequest[DownloadFilesIndex].Path);
            }
            else
            {
                if (OnDownloadFileCompleted != null)
                {
                    OnDownloadFileCompleted.Invoke();
                }
            }

            DownloadFilesIndex++;
        }
    }
}
