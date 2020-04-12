using System.Drawing;

namespace SevenRiversTD.Forms
{
    partial class LevelSelect
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LevelSelect));
			this.LevelPreview = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LevelPreview
			// 
			this.LevelPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LevelPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			this.LevelPreview.Location = new Point(80, 24);
			this.LevelPreview.Name = "LevelPreview";
			this.LevelPreview.Size = new Size(560, 432);
			this.LevelPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.LevelPreview.TabIndex = 0;
			this.LevelPreview.TabStop = false;
			this.LevelPreview.Click += LevelPreview_Click;
			// 
			// button1
			// 
			this.button1.Location = new Point(304, 488);
			this.button1.Name = "button1";
			this.button1.Size = new Size(104, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Custom Level...";
			this.button1.Click += button1_Click;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Font = new System.Drawing.Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.label1.Location = new Point(80, 456);
			this.label1.Name = "label1";
			this.label1.Size = new Size(560, 23);
			this.label1.TabIndex = 7;
			this.label1.Text = "Level 1: 20 Waves";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.ImageIndex = 1;
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new Point(32, 216);
			this.button2.Name = "button2";
			this.button2.Size = new Size(32, 40);
			this.button2.TabIndex = 8;
			this.button2.Click += button2_Click;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new Size(16, 23);
			this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream"));
			this.imageList1.TransparentColor = System.Drawing.Color.White;
			// 
			// button3
			// 
			this.button3.ImageIndex = 0;
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new Point(656, 216);
			this.button3.Name = "button3";
			this.button3.Size = new Size(32, 40);
			this.button3.TabIndex = 9;
			this.button3.Click += button3_Click;
			// 
			// LevelSelect
			// 
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(722, 520);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.LevelPreview);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "LevelSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Level Select";
			this.Load += LevelSelect_Load;
			this.ResumeLayout(false);

		}

		#endregion
	}
}
