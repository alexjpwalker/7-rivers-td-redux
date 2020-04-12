using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace SevenRiversTD.Forms
{
	public partial class LoadDialog : System.Windows.Forms.Form
	{
		public LoadDialog()
		{
			InitializeComponent();
		}

		public System.Windows.Forms.ProgressBar LoadMeter;
		public System.Windows.Forms.Button CancelDialogButton;
		public System.Windows.Forms.TextBox Details;
		public System.Windows.Forms.Timer timer1;

		private void CancelButton_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}

	};
}
