using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class HardDriveFileRepository : Repository<HardDriveFile>
    {
        public HardDriveFileRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public string DoUploadAndReturnLinkFileHardDrive(string generateCode, string filename, string mavienchuc, FileInfo replaceOldFileName, string selectedPath)
        {
            string[] split_filename = filename.Split('.');
            string new_filename = split_filename[0] + "-" + mavienchuc + "-" + generateCode + "." + split_filename[1];
            string[] splitFileNameBySlash = new_filename.Split('\\');
            FileInfo replaceNewFileName = new FileInfo(filename);
            replaceNewFileName.MoveTo(new_filename);
            replaceOldFileName = new FileInfo(new_filename);            
            selectedPath = selectedPath.Replace("\\\\", "\\");
            string linkFileHardDrive = string.Empty;
            if (selectedPath.ElementAt(selectedPath.Length - 1).ToString().Equals(@"\"))
                linkFileHardDrive = selectedPath + splitFileNameBySlash[splitFileNameBySlash.Length - 1]; //example: D:\
            else
                linkFileHardDrive = selectedPath + @"\" + splitFileNameBySlash[splitFileNameBySlash.Length - 1]; //example: D:\FolderA
            File.Copy(new_filename, linkFileHardDrive);
            replaceOldFileName.MoveTo(filename);   //doi lai filename cu~                    
            return linkFileHardDrive;
        }
    }
}
