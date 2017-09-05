using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Ean13Barcode2005
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	partial class frmEan13 : Form
	{
		private Ean13 ean13;

		public frmEan13( )
		{
			InitializeComponent( );

		    cboScale.DropDownStyle = ComboBoxStyle.DropDownList;
            cboScale.SelectedIndexChanged += cboScale_SelectedIndexChanged;
            cboScale.SelectedIndex = 2;

		    butPrint.Enabled = false;
		    StartPosition = FormStartPosition.CenterScreen;

#if DEBUG
		    Test();
#endif
        }

        private void Test()
	    {
	        ean13 = new Ean13("9780201734843");
	        ean13.Scale = (float)Convert.ToDecimal(cboScale.Items[cboScale.SelectedIndex]);

	        txtCountryCode.Text = ean13.CountryCode;
	        txtManufacturerCode.Text = ean13.ManufacturerCode;
	        txtProductCode.Text = ean13.ProductCode;
	        txtChecksumDigit.Text = ean13.ChecksumDigit;

	        Bitmap bmp = ean13.CreateBitmap();
	        this.picBarcode.Image = bmp;
	    }

        private void CreateEan13( )
		{
			ean13 = new Ean13( );
			ean13.CountryCode = txtCountryCode.Text;
			ean13.ManufacturerCode = txtManufacturerCode.Text;
			ean13.ProductCode = txtProductCode.Text;
			if( txtChecksumDigit.Text.Length > 0 )
				ean13.ChecksumDigit = txtChecksumDigit.Text;
		}

		private void butDraw_Click(object sender, EventArgs e)
		{
			Graphics g = picBarcode.CreateGraphics( );

			g.FillRectangle( new SolidBrush( SystemColors.Control ),
				new Rectangle( 0, 0, picBarcode.Width, picBarcode.Height ) );

			CreateEan13( );
			ean13.Scale = (float)Convert.ToDecimal( cboScale.Items [cboScale.SelectedIndex] );
			ean13.DrawEan13Barcode( g, new Point( 0, 0 ) );
			txtChecksumDigit.Text = ean13.ChecksumDigit;
			g.Dispose( );
		}

		private void butPrint_Click(object sender, EventArgs e)
		{
			PrintDocument pd = new PrintDocument( );
			pd.PrintPage += pd_PrintPage;
			pd.Print( );
		}

		private void pd_PrintPage( object sender, PrintPageEventArgs ev )
		{
			CreateEan13( );
			ean13.Scale = ( float )Convert.ToDecimal( cboScale.Items [cboScale.SelectedIndex] );
			ean13.DrawEan13Barcode( ev.Graphics, new Point( 0, 0 ) );
			txtChecksumDigit.Text = ean13.ChecksumDigit;

			// Add Code here to print other information.
			ev.Graphics.Dispose( );
		}

		private void butCreateBitmap_Click(object sender, EventArgs e)
        {
            CreateEan13();
            ean13.Scale = (float)Convert.ToDecimal(cboScale.Items[cboScale.SelectedIndex]);

            Bitmap bmp = ean13.CreateBitmap();
            picBarcode.Image = bmp;
        }
        
        private void cboScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ean13 == null)
            {
                return;
            }
            ean13.Scale = (float)Convert.ToDecimal(cboScale.Items[cboScale.SelectedIndex]);
            picBarcode.Image = ean13.CreateBitmap();
        }
	}
}

