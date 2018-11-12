using System;
namespace TaskApp.Models
{
    public class DummySystem
    {
        // Storage Assumes GB
        public int AvailiableSpace
        {
            get;
            set;
        }
        public int TotalSpace
        {
            get;
            set;
        }

        public Boolean NeedsDefrag{
            get;
            set;
        }

        public DummySystem()
        {
            this.AvailiableSpace = 300;
            this.TotalSpace = 500;
        }

        public string GetDiskInfo(){
            return String.Format("{0} GB availiable from {1} GB\n", this.AvailiableSpace, this.TotalSpace);
        }

        public string GetFragmentedInfo()
        {
            if(this.NeedsDefrag){
                return "System is fragmented";
            }
            else{
                return "System is not fragmented";
            }
                  
        }
    }
}
