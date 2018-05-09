using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Microsoft.WindowsAPICodePack.Shell;
using Google.Apis.Download;
using System.Net;
using System.Runtime.InteropServices;

namespace Model.Repository
{
    public class GoogleDriveFileRepository : Repository<GoogleDriveFile>
    {
        public GoogleDriveFileRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        private string[] Scopes = { DriveService.Scope.Drive };

        public bool InternetAvailable()
        {            
            int description;
            try
            {
                return InternetGetConnectedState(out description, 0);
            }
            catch
            {
                return InternetGetConnectedState(out description, 0);
            }
        }

        private DriveService GetService()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"D:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = @"D:\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //Create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "QLNS_SGU",
            });

            return service;
        }

        //public static string GetFileId()
        //{
        //    //lay id = mã viên chức + thời gian tại thời điểm upload (dd/MM/yyyy hh:mm:ss).
        //}
        //Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File();
        public string GetIdDriveFile(string mavienchuc, string code)
        {
            string id = "";
            DriveService service = GetService(); 
            FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Q = "name contains '" + mavienchuc + "' and name contains '" + code + "' and trashed = false";
            FileListRequest.Fields = "nextPageToken, files(id)";
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    id = file.Id;
                }
            }
            return id;
        }

        public List<GoogleDriveFile> GetDriveFiles(string mathevienchuc)
        {
                DriveService service = GetService();
                // Define parameters of request.     
                FilesResource.ListRequest FileListRequest = service.Files.List();
                FileListRequest.Q = "name contains '" + mathevienchuc + "' and trashed = false";
                FileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";
                // List files.
                IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
                List<GoogleDriveFile> fileList = new List<GoogleDriveFile>();
                int count = 1;
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {

                        GoogleDriveFile googleDriveFiles = new GoogleDriveFile
                        {
                            TT = count,
                            ID = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime
                        };
                        fileList.Add(googleDriveFiles);
                        count++;
                    }
                }
                return fileList;
        }

        public void UploadFile(string filename)
        {
            DriveService driveService = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = Path.GetFileName(filename);
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                request = driveService.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                request.Upload();
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public void DownloadFile(string fileId)
        {
            DriveService service = GetService();
            FilesResource.GetRequest request = service.Files.Get(fileId);
            string fileName = request.Execute().Name;
            string newFileName = RemoveCodeInFileName(fileName);
            string FilePath = Path.Combine(KnownFolders.Downloads.Path, newFileName);
            var stream = new MemoryStream();
            request.MediaDownloader.ProgressChanged +=
                    (IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    //Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    //Console.WriteLine("Download complete.");                                    
                                    SaveStream(stream, FilePath);
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    //Console.WriteLine("Download failed.");
                                    break;
                                }
                        }
                    };
            request.Download(stream);
        }

        private string RemoveCodeInFileName(string fileName)
        {
            string newFileName = "";
            string[] arrFileName = fileName.Split('-');
            arrFileName = arrFileName.Where(x => x != arrFileName[arrFileName.Length - 1] && x != arrFileName[arrFileName.Length - 2]).ToArray();
            for (int i = 0; i < arrFileName.Length; i++)
            {
                newFileName += arrFileName[i];
            }
            return newFileName + ".pdf";
        }

        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        public void DeleteFile(string id)
        {
            DriveService service = GetService();
            service.Files.Delete(id).Execute();
            //service.Files.EmptyTrash();
        }
    }
}
