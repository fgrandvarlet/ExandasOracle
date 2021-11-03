using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

// cf. http://www.blackbeltcoder.com/Articles/winforms/implementing-a-waitcursor-class

namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class WaitCursor : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public WaitCursor()
        {
            IsWaitCursor = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            IsWaitCursor = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsWaitCursor
        {
            get
            {
                return Application.UseWaitCursor;
            }
            set
            {
                if (Application.UseWaitCursor != value)
                {
                    Application.UseWaitCursor = value;
                    Cursor.Current = value ? Cursors.WaitCursor : Cursors.Default;
                }
            }
        }

    }
}
