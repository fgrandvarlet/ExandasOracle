using System;
using System.Windows.Forms;

// cf. http://www.blackbeltcoder.com/Articles/winforms/implementing-a-waitcursor-class

namespace ExandasOracle.Forms
{
    public class WaitCursor : IDisposable
    {
        public WaitCursor()
        {
            IsWaitCursor = true;
        }

        public void Dispose()
        {
            IsWaitCursor = false;
        }

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
