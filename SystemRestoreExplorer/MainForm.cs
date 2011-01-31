using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Alphaleonis.Win32.Vss;
using Microsoft.Win32;

namespace SystemRestoreExplorer
{
    public partial class MainForm : Form
    {
        private const int AboutMenuId = 0x100;
        private const int UpdateCheckMenuId = 0x101;
        private bool updateCheckEnabled = true;
        
        private int sortColumn = -1;
        private int lastSortColumn = -1;
        private SystemMenu systemMenu = null;
        private SystemRestorePoints systemRestorePoints = new SystemRestorePoints();
        private ShadowStorage shadowStorage = new ShadowStorage();
        private FileSystemWatcher fsw;
        private string systemDrive;

        public MainForm()
        {
            InitializeComponent();

            ShowSortGlyph(lastSortColumn = 0, sortColumn = 0);

            // Populate list according to chkHideNewer state
            UpdateListView(this, null);

            // Get current time zone info
            if (TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now))
            {
                lblTimeZone.Text = TimeZoneInfo.Local.DaylightName;
            }
            else
            {
                lblTimeZone.Text = TimeZoneInfo.Local.StandardName;
            }

            // Get used disk space
            lblDiskSpace.Text = shadowStorage.UsedSpaceString;

            // Only show chkOlder if there are older restore points
            int olderCount = 0;

            foreach (SystemRestoreItem sri in systemRestorePoints)
            {
                if (sri.CreationTime.ToLocalTime().CompareTo(DateTime.Now.Subtract(TimeSpan.FromDays(5))) < 0)
                {
                    olderCount++;
                }
            }

            if (olderCount > 0)
            {
                chkHideNewest.Visible = true;
            }
            else
            {
                chkHideNewest.Checked = false;
                UpdateListView(this, null);
            }

            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\SystemRestoreExplorer");
            
            if (rk != null)
            {
                updateCheckEnabled = Convert.ToBoolean(rk.GetValue("AutomaticUpdateCheckEnabled", true));
            }

            CustomizeMenu(updateCheckEnabled);

            // Get system drive (hardcode this as mout point root for now!)
            systemDrive = Environment.ExpandEnvironmentVariables("%SystemDrive%");
            systemDrive = systemDrive + @"\";

            // Init FileSystemWatcher
            fsw = new FileSystemWatcher(systemDrive);
            fsw.EnableRaisingEvents = true;

            // monitor for deleted mount points
            fsw.Deleted += new FileSystemEventHandler(fsw_Deleted);
        }

        private void CustomizeMenu(bool updateMenuChecked)
        {
            // Reset current system menu
            SystemMenu.ResetSystemMenu(this);

            // Add custom menuitems to system menu
            systemMenu = SystemMenu.FromForm(this);            
            //systemMenu.AppendSeparator();

            if (updateMenuChecked)
            {
                systemMenu.InsertMenu(0, ItemFlags.mfChecked, UpdateCheckMenuId, "Automatic Update Check Enabled");
                timerUpdateCheck.Enabled = true;
            }
            else
            {
                systemMenu.InsertMenu(0, ItemFlags.mfUnchecked, UpdateCheckMenuId, "Automatic Update Check Enabled");
            }

            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\SystemRestoreExplorer");

            if (rk != null)
            {
                rk.SetValue("AutomaticUpdateCheckEnabled", updateCheckEnabled, RegistryValueKind.DWord);
            }

            systemMenu.InsertMenu(0, AboutMenuId, "About");
            systemMenu.InsertSeparator(2);
        }

        private delegate void UpdateListViewDelegate(object sender, EventArgs e);

        protected override void WndProc(ref Message msg)
        {
            // Now we should catch the WM_SYSCOMMAND message and process it.
            // We override the WndProc to catch the WM_SYSCOMMAND message.
            // You don't have to look this message up in the MSDN; it is
            // defined in WindowMessages enumeration.
            // The WParam of the message contains the ID that was pressed.
            // It is the same value you have passed through InsertMenu()
            // or AppendMenu() member functions of my class.
            // Just check for them and do the proper action.
            if (msg.Msg == (int)WindowMessage.WMSysCommand)
            {
                switch (msg.WParam.ToInt32())
                {
                    case AboutMenuId:
                        AboutBox ab = new AboutBox();
                        ab.ShowDialog();
                        break;
                        
                    case UpdateCheckMenuId:
                        updateCheckEnabled = !updateCheckEnabled;
                        CustomizeMenu(updateCheckEnabled);
                        break;
                }
            }

            // Call base class function
            base.WndProc(ref msg);
        }

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            UpdateListView(sender, e);
        }

        private void lvRestorePoints_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;

                // Set the sort order to descending by default.
                lvRestorePoints.Sorting = SortOrder.Descending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvRestorePoints.Sorting == SortOrder.Ascending)
                {
                    lvRestorePoints.Sorting = SortOrder.Descending;
                }
                else
                {
                    lvRestorePoints.Sorting = SortOrder.Ascending;
                }
            }

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            lvRestorePoints.ListViewItemSorter = new ListViewItemComparer(e.Column, lvRestorePoints.Sorting);

            // Call the sort method to manually sort.
            lvRestorePoints.Sort();

            ShowSortGlyph(lastSortColumn, e.Column);
            lastSortColumn = e.Column;
        }

        // We should clear the sort glyph on the old column
        private void ShowSortGlyph(int lastSortedColumn, int columnToSort) 
        {
            // Get the handle of the ListView header
            IntPtr hHeader = Win32NativeMethods.SendMessage(lvRestorePoints.Handle, Win32NativeMethods.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero); 
            IntPtr newColumn = new IntPtr(columnToSort);
            IntPtr prevColumn = new IntPtr(lastSortedColumn);
            Win32NativeMethods.HDITEM hdi;
            IntPtr rtn;

            // If the last sorted column exists
            if (lastSortedColumn != -1 && lastSortedColumn != columnToSort) 
            {
                hdi = new Win32NativeMethods.HDITEM();
                hdi.mask = Win32NativeMethods.HDI_FORMAT;
                rtn = Win32NativeMethods.SendMessageItem(hHeader, Win32NativeMethods.HDM_GETITEM, prevColumn, ref hdi);
                hdi.fmt &= ~Win32NativeMethods.HDF_SORTDOWN & ~Win32NativeMethods.HDF_SORTUP;
                
                // Clear the sort glyph
                rtn = Win32NativeMethods.SendMessageItem(hHeader, Win32NativeMethods.HDM_SETITEM, prevColumn, ref hdi);
            }

            hdi = new Win32NativeMethods.HDITEM();
            hdi.mask = Win32NativeMethods.HDI_FORMAT;
            rtn = Win32NativeMethods.SendMessageItem(hHeader, Win32NativeMethods.HDM_GETITEM, newColumn, ref hdi);
            hdi.fmt |= lvRestorePoints.Sorting == SortOrder.Ascending ? Win32NativeMethods.HDF_SORTUP : Win32NativeMethods.HDF_SORTDOWN;

            // Send message to the column header to show the sort glyph
            rtn = Win32NativeMethods.SendMessageItem(hHeader, Win32NativeMethods.HDM_SETITEM, newColumn, ref hdi); 
            lastSortedColumn = columnToSort;
        }

        private void UpdateListView(object sender, EventArgs e)
        {
            if (this.lvRestorePoints.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                this.lvRestorePoints.Invoke(new UpdateListViewDelegate(this.UpdateListView), sender, e);
            }
            else
            {
                // This is the UI thread so perform the task.
                chkHideNewest_CheckedChanged(sender, e);
            }
        }

        private void chkHideNewest_CheckedChanged(object sender, EventArgs e)
        {
            lvRestorePoints.Items.Clear();

            if (chkHideNewest.Checked)
            {
                foreach (SystemRestoreItem sri in systemRestorePoints)
                {
                    if (sri.CreationTime.ToLocalTime().CompareTo(DateTime.Now.Subtract(TimeSpan.FromDays(5))) < 0)
                    {
                        ListViewItem lvi = new ListViewItem(sri.CreationTime.ToLocalTime().ToString());
                        lvi.SubItems.Add(sri.Description);
                        lvRestorePoints.Items.Add(lvi);
                    }
                }
            }
            else
            {
                foreach (SystemRestoreItem sri in systemRestorePoints)
                {
                    ListViewItem lvi = new ListViewItem(sri.CreationTime.ToLocalTime().ToString());
                    lvi.SubItems.Add(sri.Description);
                    lvRestorePoints.Items.Add(lvi);
                }
            }

            // Trim the headers to prevert horiz scorllbars
            lvRestorePoints.Columns[lvRestorePoints.Columns.Count - 1].Width = -2;

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            lvRestorePoints.ListViewItemSorter = new ListViewItemComparer(lastSortColumn, lvRestorePoints.Sorting);
            
            // Call the sort method to manually sort.
            lvRestorePoints.Sort();

            // Nothig is selected so disable buttons
            btnDelete.Enabled = false;
            btnMount.Enabled = false;
            btnUnmount.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lvRestorePoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRestorePoints.SelectedItems.Count > 0)
            {
                btnDelete.Enabled = true;

                try
                {
                    VssSnapshotProperties snapshot = GetVssSnapshotFromTime(DateTime.Parse(lvRestorePoints.Items[lvRestorePoints.SelectedIndices[0]].Text));

                    if (snapshot != null)
                    {
                        string[] path = snapshot.SnapshotDeviceObject.Split('\\');
                        string mountPoint = systemDrive + path[path.Length - 1];

                        if (Directory.Exists(mountPoint))
                        {
                            btnMount.Enabled = false;
                            btnUnmount.Enabled = true;

                            tsmiMount.Enabled = false;
                            tsmiUnmount.Enabled = true;
                        }
                        else
                        {
                            btnMount.Enabled = true;
                            btnUnmount.Enabled = false;

                            tsmiMount.Enabled = true;
                            tsmiUnmount.Enabled = false;
                        }
                    }
                }
                catch (Exception)
                {
                    btnMount.Enabled = true;
                    btnUnmount.Enabled = false;

                    tsmiMount.Enabled = true;
                    tsmiUnmount.Enabled = false;
                }
            }
        }

        private void btnMount_Click(object sender, EventArgs e)
        {
            VssSnapshotProperties snapshot = GetVssSnapshotFromTime(DateTime.Parse(lvRestorePoints.Items[lvRestorePoints.SelectedIndices[0]].Text));

            if (snapshot != null)
            {
                string[] path = snapshot.SnapshotDeviceObject.Split('\\');
                string mountPoint = systemDrive + path[path.Length - 1];

                // Create the hardlink
                bool symResult = VolumeNativeMethods.CreateSymbolicLink(mountPoint, snapshot.SnapshotDeviceObject + @"\", 1);

                if (symResult)
                {
                    // Start explorer at the mount point
                    ProcessStartInfo explorer = new System.Diagnostics.ProcessStartInfo();
                    explorer.FileName = "explorer.exe";
                    explorer.Arguments = mountPoint;
                    Process.Start(explorer);

                    btnMount.Enabled = false;
                    btnUnmount.Enabled = true;

                    tsmiMount.Enabled = false;
                    tsmiUnmount.Enabled = true;
                }
            }
        }

        private DateTime TruncateToHour(DateTime timeToTruncate)
        {
            return DateTime.Parse(timeToTruncate.ToShortDateString() + " " + timeToTruncate.Hour + ":00:00");
        }

        private DateTime TruncateToMinute(DateTime timeToTruncate)
        {
            return DateTime.Parse(timeToTruncate.ToShortDateString() + " " + timeToTruncate.Hour + ":" + timeToTruncate.Minute + ":00");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get selected restore point
            SystemRestoreItem selectedSystemRestoreItem = null;
            string time = lvRestorePoints.Items[lvRestorePoints.SelectedIndices[0]].Text;
            DateTime selectedCreationTime = DateTime.Parse(time);

            foreach (SystemRestoreItem sri in systemRestorePoints)
            {
                if (sri.CreationTime.ToLocalTime().CompareTo(selectedCreationTime) == 0)
                {
                    selectedSystemRestoreItem = sri;
                    break;
                }
            }

            if (selectedSystemRestoreItem.SequenceNumber > 0)
            {
                // Need to display are you sure dialog!
                string prompt = "Are you sure you want to remove system restore point?";
                TaskDialogResult tdr = TaskDialog.Show(this, selectedSystemRestoreItem.CreationTime.ToString() + "\n" + selectedSystemRestoreItem.Description, prompt, this.Text, TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning);

                // If the user is sure
                if (tdr == TaskDialogResult.Yes)
                {
                    // Remove the restore point
                    uint result = VolumeNativeMethods.SRRemoveRestorePoint(selectedSystemRestoreItem.SequenceNumber);

                    // If success
                    if (result == 0)
                    {
                        // Update internal data on system restore points and shadow storage
                        systemRestorePoints = new SystemRestorePoints();
                        shadowStorage = new ShadowStorage();

                        lblDiskSpace.Text = shadowStorage.UsedSpaceString;
                        UpdateListView(sender, e);
                        
                        // Only show chkOlder if there are older restore points
                        int olderCount = 0;

                        foreach (SystemRestoreItem sri in systemRestorePoints)
                        {
                            if (sri.CreationTime.ToLocalTime().CompareTo(DateTime.Now.Subtract(TimeSpan.FromDays(5))) < 0)
                            {
                                olderCount++;
                            }
                        }

                        if (olderCount > 0)
                        {
                            chkHideNewest.Visible = true;
                        }
                        else
                        {
                            chkHideNewest.Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error deleting restore point");
                    }
                }
            }
        }

        private void btnUnmount_Click(object sender, EventArgs e)
        {
            VssSnapshotProperties snapshot = GetVssSnapshotFromTime(DateTime.Parse(lvRestorePoints.Items[lvRestorePoints.SelectedIndices[0]].Text));

            if (snapshot != null)
            {
                string[] path = snapshot.SnapshotDeviceObject.Split('\\');
                string mountPoint = systemDrive + path[path.Length - 1];

                Directory.Delete(mountPoint);

                btnMount.Enabled = true;
                btnUnmount.Enabled = false;

                tsmiMount.Enabled = true;
                tsmiUnmount.Enabled = false;
            }
        }

        private VssSnapshotProperties GetVssSnapshotFromTime(DateTime selectedCreationTime)
        {
            Debug.WriteLine(string.Format("Selected creation time: {0}", selectedCreationTime.ToLongTimeString()));

            // Get selected restore point
            SystemRestoreItem selectedSystemRestoreItem = null;
            
            foreach (SystemRestoreItem sri in systemRestorePoints)
            {
                if (sri.CreationTime.ToLocalTime().CompareTo(selectedCreationTime) == 0)
                {
                    selectedSystemRestoreItem = sri;
                    break;
                }
            }

            // Enumerate snapshots using VSS API
            IVssImplementation vssImplmentation = VssUtils.LoadImplementation();
            ////VssBackupComponents vss = new VssBackupComponents();
            IVssBackupComponents vssBackup = vssImplmentation.CreateVssBackupComponents();

            vssBackup.InitializeForBackup(null);
            vssBackup.SetContext(VssSnapshotContext.All);

            List<VssSnapshotProperties> snapshotHourMatches = new List<VssSnapshotProperties>();
            List<VssSnapshotProperties> snapshotMinuteMatches = new List<VssSnapshotProperties>();

            foreach (VssSnapshotProperties snapshot in vssBackup.QuerySnapshots())
            {
                DateTime truncatedSelectedRestoreItemTime = TruncateToHour(selectedSystemRestoreItem.CreationTime.ToLocalTime());
                DateTime truncatedSnapshotTime = TruncateToHour(snapshot.CreationTimestamp);

                if (truncatedSelectedRestoreItemTime.CompareTo(truncatedSnapshotTime) == 0)
                {
                    snapshotHourMatches.Add(snapshot);
                }
            }

            if (snapshotHourMatches.Count == 1)
            {
                Debug.WriteLine(string.Format("Snapshot creation time: {0}", snapshotHourMatches[0].CreationTimestamp.ToLongTimeString()));
                return snapshotHourMatches[0];
            }
            else if (snapshotHourMatches.Count > 1)
            {
                foreach (VssSnapshotProperties snapshot in snapshotHourMatches)
                { 
                    DateTime truncatedSelectedRestoreItemTime = TruncateToMinute(selectedSystemRestoreItem.CreationTime.ToLocalTime());
                    DateTime truncatedSnapshotTime = TruncateToMinute(snapshot.CreationTimestamp);

                    if (truncatedSelectedRestoreItemTime.CompareTo(truncatedSnapshotTime) == 0)
                    {
                        snapshotMinuteMatches.Add(snapshot);
                    }
                }

                if (snapshotMinuteMatches.Count == 1)
                {
                    Debug.WriteLine(string.Format("Snapshot creation time: {0}", snapshotMinuteMatches[0].CreationTimestamp.ToLongTimeString()));
                    return snapshotMinuteMatches[0];
                }
                else if (snapshotMinuteMatches.Count > 1)
                {
                    Debug.WriteLine(string.Format("Found {0} potential matches", snapshotMinuteMatches.Count.ToString()));

                    // Find closest match
                    foreach (VssSnapshotProperties snapshot in snapshotMinuteMatches)
                    {
                        if(selectedSystemRestoreItem.CreationTime.ToLocalTime().CompareTo(snapshot.CreationTimestamp) < 0)
                        {
                            Debug.WriteLine(string.Format("Snapshot creation time: {0}", snapshot.CreationTimestamp.ToLongTimeString()));
                            return snapshot;
                        }
                    }                   
                }
            }

            return null;
        }

        private void timerUpdateCheck_Tick(object sender, EventArgs e)
        {
            timerUpdateCheck.Enabled = false;

            SelfUpdater su = new SelfUpdater(this);
            su.CheckForUpdate("http://nicbedford.co.uk/versions.xml", "SystemRestoreExplorer");
        }
    }
}
