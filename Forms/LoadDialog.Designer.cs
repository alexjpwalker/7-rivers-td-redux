using System.Drawing;

namespace SevenRiversTD.Forms
{
    partial class LoadDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.LoadMeter = new System.Windows.Forms.ProgressBar();
			this.CancelDialogButton = new System.Windows.Forms.Button();
			this.Details = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// LoadMeter
			// 
			this.LoadMeter.Location = new Point(64, 64);
			this.LoadMeter.Name = "LoadMeter";
			this.LoadMeter.Size = new Size(176, 23);
			this.LoadMeter.TabIndex = 0;
			// 
			// CancelButton
			// 
			this.CancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelDialogButton.Enabled = false;
			this.CancelDialogButton.Location = new Point(104, 104);
			this.CancelDialogButton.Name = "CancelButton";
			this.CancelDialogButton.Size = new Size(88, 23);
			this.CancelDialogButton.TabIndex = 1;
			this.CancelDialogButton.Text = "Cancel";
			this.CancelDialogButton.Click += CancelButton_Click;
			// 
			// Details
			// 
			this.Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.Details.Location = new Point(64, 24);
			this.Details.Name = "Details";
			this.Details.ReadOnly = true;
			this.Details.Size = new Size(176, 22);
			this.Details.TabIndex = 2;
			this.Details.TabStop = false;
			this.Details.Text = "";
			this.Details.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// timer1
			// 
			this.timer1.Interval = 1;
			// 
			// LoadDialog
			// 
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(292, 144);
			this.Controls.Add(this.Details);
			this.Controls.Add(this.CancelDialogButton);
			this.Controls.Add(this.LoadMeter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoadDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loading, please wait...";
			this.ResumeLayout(false);

		}

		#endregion
	}
}
