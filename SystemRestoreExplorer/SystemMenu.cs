using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SystemRestoreExplorer
{   
    // Values taken from MSDN.
    public enum ItemFlags
    { 
        // The item ...
        mfUnchecked = 0x00000000,    // ... is not checked
        mfString = 0x00000000,    // ... contains a string as label
        mfDisabled = 0x00000002,    // ... is disabled
        mfGrayed = 0x00000001,    // ... is grayed
        mfChecked = 0x00000008,    // ... is checked
        mfPopup = 0x00000010,    // ... Is a popup menu. Pass the

        // Menu handle of the popup
        // Menu into the ID parameter
        mfBarBreak = 0x00000020,    // ... is a bar break
        mfBreak = 0x00000040,    // ... is a break
        mfByPosition = 0x00000400,    // ... is identified by the position
        mfByCommand = 0x00000000,    // ... is identified by its ID
        mfSeparator = 0x00000800     // ... is a seperator (String and
        
        // ID parameters are ignored
    }

    public enum WindowMessage
    {
        WMSysCommand = 0x0112
    }

    /// <summary>
    /// A class that helps to manipulate the system menu
    /// of a passed form.
    ///
    /// Written by Florian "nohero" Stinglmayr
    /// </summary>
    public class SystemMenu
    {
        // Handle to the System Menu
        private IntPtr hSystemMenu = IntPtr.Zero;

        public SystemMenu()
        {
        }

        // Retrieves a new object from a Form object
        public static SystemMenu FromForm(Form form)
        {
            SystemMenu systemMenu = new SystemMenu();

            systemMenu.hSystemMenu = apiGetSystemMenu(form.Handle, 0);
            if (systemMenu.hSystemMenu == IntPtr.Zero)
            { // Throw an exception on failure
                throw new NoSystemMenuException();
            }

            return systemMenu;
        }

        // Reset's the window menu to it's default
        public static void ResetSystemMenu(Form form)
        {
            apiGetSystemMenu(form.Handle, 1);
        }

        // Checks if an ID for a new system menu item is OK or not
        public static bool VerifyItemID(int id)
        {
            return (bool)(id < 0xF000 && id > 0);
        }

        // Insert a separator at the given position index starting at zero.
        public bool InsertSeparator(int pos)
        {
            return InsertMenu(pos, ItemFlags.mfSeparator | ItemFlags.mfByPosition, 0, string.Empty);
        }

        // Simplified InsertMenu(), that assumes that Pos is a relative
        // position index starting at zero
        public bool InsertMenu(int pos, int id, string item)
        {
            return InsertMenu(pos, ItemFlags.mfByPosition | ItemFlags.mfString, id, item);
        }

        // Insert a menu at the given position. The value of the position
        // depends on the value of Flags. See the article for a detailed
        // description.
        public bool InsertMenu(int pos, ItemFlags flags, int id, string item)
        {
            flags |= ItemFlags.mfByPosition;
            return apiInsertMenu(hSystemMenu, pos, (int)flags, id, item) == 0;
        }

        // Appends a seperator
        public bool AppendSeparator()
        {
            return AppendMenu(0, string.Empty, ItemFlags.mfSeparator);
        }

        // This uses the ItemFlags.mfString as default value
        public bool AppendMenu(int id, string item)
        {
            return AppendMenu(id, item, ItemFlags.mfString);
        }

        // Superseded function.
        public bool AppendMenu(int id, string item, ItemFlags flags)
        {
            return apiAppendMenu(hSystemMenu, (int)flags, id, item) == 0;
        }

        // I havn't found any other solution than using plain old
        // WinAPI to get what I want.
        // If you need further information on these functions, their
        // parameters, and their meanings, you should look them up in
        // the MSDN.

        // All parameters in the [DllImport] should be self explanatory.
        // NOTICE: Use never stdcall as a calling convention, since Winapi
        // is used.
        // If the underlying structure changes, your program might cause
        // errors that are hard to find.

        // First, we need the GetSystemMenu() function.
        // This function does not have an Unicode counterpart
        [DllImport("USER32", EntryPoint = "GetSystemMenu", SetLastError = true,
                   CharSet = CharSet.Unicode, ExactSpelling = true,
                   CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr apiGetSystemMenu(IntPtr hWnd, int bReset);

        // And we need the AppendMenu() function. Since .NET uses Unicode,
        // we pick the unicode solution.
        [DllImport("USER32", EntryPoint = "AppendMenuW", SetLastError = true,
                   CharSet = CharSet.Unicode, ExactSpelling = true,
                   CallingConvention = CallingConvention.Winapi)]
        private static extern int apiAppendMenu(IntPtr hMenu, int flags, int newID, string item);

        // And we also may need the InsertMenu() function.
        [DllImport("USER32", EntryPoint = "InsertMenuW", SetLastError = true,
                   CharSet = CharSet.Unicode, ExactSpelling = true,
                   CallingConvention = CallingConvention.Winapi)]
        private static extern int apiInsertMenu(IntPtr hMenu, int position, int flags, int newId, string item);
    }
}
