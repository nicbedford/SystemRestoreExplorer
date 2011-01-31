using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SystemRestoreExplorer
{
    public class VolumeNativeMethods
    {
        internal const uint DDD_RAW_TARGET_PATH = 0x00000001;
        internal const uint DDD_REMOVE_DEFINITION = 0x00000002;
        internal const uint DDD_EXACT_MATCH_ON_REMOVE = 0x00000004;
        internal const uint DDD_NO_BROADCAST_SYSTEM = 0x00000008;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern bool CreateSymbolicLink([MarshalAs(UnmanagedType.LPWStr)] string lpSymLinkFileName, [MarshalAs(UnmanagedType.LPWStr)] string lpTargetFileName, uint dwFlags);

        [DllImport("srclient.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern uint SRRemoveRestorePoint(uint dwRPNum);

        [DllImport("kernel32.dll")]
        internal static extern bool DefineDosDevice(uint dwFlags, string lpDeviceName, string lpTargetPath);

        [DllImport("kernel32.dll")]
        internal static extern uint QueryDosDevice(string lpDeviceName, string lpTargetPath, uint ucchMax);

        [DllImport("kernel32.dll")]
        internal static extern bool DeleteVolumeMountPoint(string lpszVolumeMountPoint);
    }
}
