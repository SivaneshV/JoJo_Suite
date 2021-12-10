using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.UI
{
    public static class AppSettings
    {
        public static RecordingStatus recordingStatus { get; set; }
        public static string CurrentURL { get; set; }
        public static bool IsRoboticUser { get; set; }

        public static int isAppStartupCnt { get; set; }
    }


    public class CommandList : List<string>
    {
        public CommandList()
        {
            this.Add("openurl");
            this.Add("click");
            this.Add("selectoption");
            this.Add("sendkeys");
            this.Add("sendkeysandenter");
        }
    }

    public enum RecordingStatus
    {
        Recording,
        Paused,
        Stopped
    }
}
