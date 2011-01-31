using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace SystemRestoreExplorer
{
    public class ShadowStorage
    {
        private long usedSpace = 0;
        private long allocatedSpace = 0;
        private long maxSpace = 0;
        private bool isSpaceValid = true;

        public ShadowStorage()
        {
            try
            {
                ManagementClass mc = new ManagementClass(@"Win32_ShadowStorage");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    usedSpace += Convert.ToInt64(mo.GetPropertyValue("UsedSpace").ToString());
                    allocatedSpace += Convert.ToInt64(mo.GetPropertyValue("AllocatedSpace").ToString());
                    maxSpace += Convert.ToInt64(mo.GetPropertyValue("MAxSpace").ToString());
                }
            }
            catch (ManagementException)
            {
                isSpaceValid = false;
            }
        }

        public string UsedSpaceString
        {
            get
            {
                string result;

                if (isSpaceValid)
                {
                    double gigaBytes;
                    double megaBytes = (double)usedSpace / (double)(1024 * 1024);

                    // If higher than 1000MB get GB
                    if (megaBytes > 1000)
                    {
                        gigaBytes = (double)usedSpace / (double)(1024 * 1024 * 1024);
                        result = String.Format("{0:0.000}", gigaBytes) + " GB";
                    }
                    else
                    {
                        result = String.Format("{0:0.000}", megaBytes) + " MB";
                    }
                }
                else
                {
                    result = "Unknown";
                }

                return result;
            }
        }
    }
}
