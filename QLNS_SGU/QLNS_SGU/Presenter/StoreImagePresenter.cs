using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using Model.ObjectModels;
using Model.Repository;
using System.Windows.Forms;
using Model;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Text.RegularExpressions;
using DevExpress.XtraSplashScreen;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace QLNS_SGU.Presenter
{
    public interface IStoreImagePresenter : IPresenterArgument
    {
        void LoadDriveFiles();
        void Upload();
        void Download();
        void Delete();
    }

    public class StoreImagePresenter : IStoreImagePresenter
    {
        private StoreImageForm _view;

        public object UI
        {
            get
            {
                return _view;
            }
        }

        public StoreImagePresenter(StoreImageForm view)
        {
            _view = view;
        }

        public void Initialize(string mathevienchuc)
        {
            _view.Attach(this);
            _view.MaTheVienChuc.EditValue = mathevienchuc;
        }

        public void LoadDriveFiles()
        {
            DataSource();
        }

        private void DataSource()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new Model.Entities.QLNSSGU_1Entities());
            string mathevienchuc = _view.MaTheVienChuc.EditValue.ToString();
            List<GoogleDriveFile> list1 = unitOfWorks.GoogleDriveFileRepository.GetDriveFiles(mathevienchuc);
            List<GoogleDriveFile> list2 = new List<GoogleDriveFile>();
            foreach (var a in list1)
            {
                list2.Add(new GoogleDriveFile
                {
                    TT = a.TT,
                    ID = a.ID,
                    Name = a.Name,
                    Size = a.Size / 1024,
                    Version = a.Version,
                    CreatedTime = a.CreatedTime
                });
            }
            _view.GridControl.DataSource = list2;            
        }

        public void Upload()
        {
            string mathevienchuc = _view.MaTheVienChuc.EditValue.ToString();
            _view.OpenFileDialog.FileName = string.Empty;
            _view.OpenFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new Model.Entities.QLNSSGU_1Entities());         
            if(unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
            {
                try
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false);
                    SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                    SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                    foreach (string filename in _view.OpenFileDialog.FileNames)
                    {
                        string[] temp = filename.Split('\\');
                        if (temp[temp.Length - 1].Contains(mathevienchuc))
                        {                            
                            unitOfWorks.GoogleDriveFileRepository.UploadFile(filename);
                        }
                        else
                        {
                            string[] split_filename = filename.Split('.');
                            string new_filename = split_filename[0] + "-" + mathevienchuc + "." + split_filename[1];
                            FileInfo fileInfo = new FileInfo(filename);
                            fileInfo.MoveTo(new_filename);
                            unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        }
                    }
                    SplashScreenManager.CloseForm();
                    DataSource();
                }
                catch
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //[DllImport("wininet.dll")]
        //private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        //private Task<bool> CheckInternetConnectionAsync()
        //{
        //    return Task.Run(() =>
        //    {
        //        int description;
        //        try
        //        {
        //            return InternetGetConnectedState(out description, 0);
        //        }
        //        catch
        //        {
        //            return InternetGetConnectedState(out description, 0);
        //        }
        //    });
        //}

        //private async void CheckInternetConnection()
        //{
        //    bool hasConnection = await CheckInternetConnectionAsync();
        //    if(hasConnection == false)
        //    {
        //        Application.ExitThread();
        //    }
        //}

        public void Download()
        {            
            UnitOfWorks unitOfWorks = new UnitOfWorks(new Model.Entities.QLNSSGU_1Entities());
            int[] rows_handle = _view.GridView.GetSelectedRows();      
            if(unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
            {
                try
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false);
                    SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                    SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
                    foreach (int row in rows_handle)
                    {
                        if (row >= 0)
                        {
                            string id = _view.GridView.GetRowCellValue(row, "ID").ToString();
                            unitOfWorks.GoogleDriveFileRepository.DownloadFile(id);                           
                        }
                    }
                    SplashScreenManager.CloseForm();
                }
                catch
                {                   
                    XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        public void Delete()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new Model.Entities.QLNSSGU_1Entities());
            int[] rows_handle = _view.GridView.GetSelectedRows();                          
            if(unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
            {                
                try
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false);
                    SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                    SplashScreenManager.Default.SetWaitFormDescription("Đang xóa tập tin....");
                    foreach (int row in rows_handle)
                    {
                        if (row >= 0)
                        {
                            string id = _view.GridView.GetRowCellValue(row, "ID").ToString();           
                            unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
                        }
                    }
                    SplashScreenManager.CloseForm();
                    DataSource();
                }
                catch
                {
                    XtraMessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                               
            }
            else
            {
                XtraMessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataSource();
            }
        }
    }
}
