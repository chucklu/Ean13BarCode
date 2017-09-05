#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace Ean13Barcode2005
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main( )
		{
		    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		    Application.ThreadException += Application_ThreadException;

		    Application.EnableVisualStyles();
		    Application.SetCompatibleTextRenderingDefault(false);
		    Application.Run(new frmEan13());
		}

	    static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
	    {
	        MessageBox.Show(e.Exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	    }
    }
}