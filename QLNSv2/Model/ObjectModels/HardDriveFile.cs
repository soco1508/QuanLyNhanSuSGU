using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class HardDriveFile
    {
        public int TT { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }

        public HardDriveFile()
        {
            TT = -1;
            ID = string.Empty;
            Name = string.Empty;
            Size = -1;
            Version = -1;
            CreatedTime = null;
        }
    }
}
