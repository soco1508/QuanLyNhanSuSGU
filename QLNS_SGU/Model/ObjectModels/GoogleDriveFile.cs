using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class GoogleDriveFile
    {
        public int TT { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }

        public GoogleDriveFile()
        {
            TT = -1;
            ID = "";
            Name = "";
            Size = -1;
            Version = -1;
            CreatedTime = Convert.ToDateTime("01/01/1900");
        }
    }
}
