using System;
using System.Windows.Forms;

namespace SystemRestoreExplorer
{
    public class SystemRestoreItem
    {
        private string description;
        private uint restorePointType;
        private uint eventType;
        private uint sequenceNumber;
        private string creationTime;

        public SystemRestoreItem(string description, uint restorePointType, uint eventType, uint sequenceNumber, string creationTime)
        {
            switch (restorePointType)
            {
                case 0:
                case 10:
                    this.description = "Install: " + description;
                    break;
                case 1:
                    this.description = "Uninstall: " + description;
                    break;
                case 6:
                    this.description = "Restore: " + description;
                    break;
                case 7:
                case 11:
                    this.description = "System: " + description;
                    break;
                case 12:
                    this.description = "Modified: " + description;
                    break;
                case 13:
                    this.description = "Cancelled: " + description;
                    break;
                case 14:
                    this.description = "Backup: " + description;
                    break;
                case 16:
                    this.description = "Manual: " + description;
                    break;
                default:
                    ////string buffer = "RestorePointType: "+restorePointType.ToString() + ", EventType: " + eventType.ToString() +", SequenceNumber: " + sequenceNumber.ToString();
                    ////MessageBox.Show(buffer);
                    this.description = description;
                    break;
            }

            this.restorePointType = restorePointType;
            this.eventType = eventType;
            this.sequenceNumber = sequenceNumber;
            this.creationTime = creationTime;
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                // Convert SystemRestore CreationTime string into a DateTime object
                int year = Convert.ToInt32(new string(creationTime.ToCharArray(0, 4)));
                int month = Convert.ToInt32(new string(creationTime.ToCharArray(4, 2)));
                int day = Convert.ToInt32(new string(creationTime.ToCharArray(6, 2)));
                int hour = Convert.ToInt32(new string(creationTime.ToCharArray(8, 2)));
                int minute = Convert.ToInt32(new string(creationTime.ToCharArray(10, 2)));
                int second = Convert.ToInt32(new string(creationTime.ToCharArray(12, 2)));

                return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            }
        }

        public uint SequenceNumber
        {
            get
            {
                return sequenceNumber;
            }
        }
    }
}
