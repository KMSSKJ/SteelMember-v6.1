
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util
{
    public class FileViewModel
    {
        public int MemberID { get; set; }
        public string TreeID { get; set; }
        public string MemberModel { get; set; }
        public string RawMaterialName { get; set; }
        public string OrderNumbering { get; set; }
        public string ShipNumbering { get; set; }
        public string CollarNumbering { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public int Size { get; set; }
        public DateTime UploadTime { get; set; }
        public Nullable<DateTime> InBeginTime { get; set; }
        public Nullable<DateTime> InEndTime { get; set; }
        public string Class { get; set; }
        public DateTime ModifidBeginTime { get; set; }
        public DateTime ModifidEndTime { get; set; }
        public DateTime OverdueTime { get; set; }
        public int Enabled { get; set; }
        public string Description { get; set; }
        public int KJWZ { get; set; }
       
    }
}
