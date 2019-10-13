using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Helpers
{
    public static class InactivityHelper
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LastInputInfo refLastInputInfo);

        public static InactivityTimeInfo GetInactiveTimeInfo()
        {
            int systemUptime = Environment.TickCount,
                lastInputTicks = 0,
                inactiveTicks = 0;

            LastInputInfo lastInputInfo = new LastInputInfo();
            lastInputInfo.infoSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.downTime = 0;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                lastInputTicks = (int)lastInputInfo.downTime;
                inactiveTicks = systemUptime - lastInputTicks;
            }

            return new InactivityTimeInfo
            {
                LastInputTime = DateTime.Now.AddMilliseconds(-1 * inactiveTicks),
                InactiveTime = new TimeSpan(0, 0, 0, 0, inactiveTicks),
                SystemUpTimeMilliseconds = systemUptime,
            };
        }
    }

    public class InactivityTimeInfo 
    {
        public DateTime LastInputTime { get; internal set; }
        public TimeSpan InactiveTime { get; internal set; }
        public int SystemUpTimeMilliseconds { get; internal set; }
    }

    internal struct LastInputInfo
    {
        public uint infoSize;
        public uint downTime;
    }
}
