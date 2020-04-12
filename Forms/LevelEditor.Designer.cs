using System.Drawing;
using System.Windows.Forms;

namespace SevenRiversTD.Forms
{
    partial class LevelEditor
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
		void InitializeComponent()
		{
			this.GameArea = new System.Windows.Forms.Panel();
			this.TilePanel = new System.Windows.Forms.Panel();
			this.SaveLevel = new System.Windows.Forms.SaveFileDialog();
			this.LoadLevel = new System.Windows.Forms.OpenFileDialog();
			this.AddPath = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MenuStrip();
			this.mFile = new System.Windows.Forms.ToolStripMenuItem();
			this.NewMap = new System.Windows.Forms.ToolStripMenuItem();
			this.mOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenMap = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenWaves = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveMap = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mAddPath = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.FillArea = new System.Windows.Forms.ToolStripMenuItem();
			this.WaveHeader = new System.Windows.Forms.Label();
			this.LoadWaves = new System.Windows.Forms.OpenFileDialog();
			this.Status = new System.Windows.Forms.StatusBar();
			this.Path = new System.Windows.Forms.NumericUpDown();
			this.t_path = new System.Windows.Forms.Label();
			this.PathHeader = new System.Windows.Forms.Label();
			this.SaveMapOnly = new System.Windows.Forms.ToolStripMenuItem();
			this.Path.BeginInit();
			this.SuspendLayout();
			// 
			// GameArea
			// 
			this.GameArea.BackColor = System.Drawing.Color.LightGreen;
			this.GameArea.Location = new Point(0, 0);
			this.GameArea.Name = "GameArea";
			this.GameArea.Size = new Size(736, 600);
			this.GameArea.TabIndex = 2;
			this.GameArea.MouseUp += GameArea_MouseUp;
			this.GameArea.Paint += GameArea_Paint;
			this.GameArea.MouseMove += GameArea_MouseMove;
			this.GameArea.MouseDown += GameArea_MouseDown;
			// 
			// TilePanel
			// 
			this.TilePanel.Location = new Point(768, 240);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.Size = new Size(97, 129);
			this.TilePanel.TabIndex = 3;
			this.TilePanel.Paint += TilePanel_Paint;
			this.TilePanel.MouseDown += TilePanel_MouseDown;
			// 
			// SaveLevel
			// 
			this.SaveLevel.Filter = "Level File (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// LoadLevel
			// 
			this.LoadLevel.Filter = "Level File (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// AddPath
			// 
			this.AddPath.Location = new Point(776, 392);
			this.AddPath.Name = "AddPath";
			this.AddPath.TabIndex = 4;
			this.AddPath.Text = "Add path";
			this.AddPath.Click += mAddPath_Click;
			// 
			// mainMenu1
			// 
			System.Windows.Forms.ToolStripMenuItem[] __mcTemp__1 = new System.Windows.Forms.ToolStripMenuItem[2];
			__mcTemp__1[0] = this.mFile;
			__mcTemp__1[1] = this.mEdit;
			this.mainMenu1.Items.AddRange(__mcTemp__1);
			// 
			// mFile
			//
			System.Windows.Forms.ToolStripMenuItem[] __mcTemp__2 = new System.Windows.Forms.ToolStripMenuItem[6];
			__mcTemp__2[0] = this.NewMap;
			__mcTemp__2[1] = this.mOpen;
			__mcTemp__2[2] = this.SaveMap;
			__mcTemp__2[3] = this.SaveMapOnly;
			__mcTemp__2[4] = this.SaveAs;
			__mcTemp__2[5] = this.Exit;
			this.mFile.DropDownItems.AddRange(__mcTemp__2);
			this.mFile.Text = "&File";
			// 
			// NewMap
			//
			this.NewMap.ShortcutKeys = Keys.Control | Keys.N;
			this.NewMap.Text = "&New";
			// 
			// mOpen
			//
			System.Windows.Forms.ToolStripMenuItem[] __mcTemp__3 = new System.Windows.Forms.ToolStripMenuItem[2];
			__mcTemp__3[0] = this.OpenMap;
			__mcTemp__3[1] = this.OpenWaves;
			this.mOpen.DropDownItems.AddRange(__mcTemp__3);
			this.mOpen.ShortcutKeys = Keys.Control | Keys.O;
			this.mOpen.Text = "&Open";
			// 
			// OpenMap
			//
			this.OpenMap.Text = "&Map...";
			// 
			// OpenWaves
			//
			this.OpenWaves.Text = "&Waves...";
			this.OpenWaves.Click += OpenWaves_Click;
			// 
			// SaveMap
			//
			this.SaveMap.ShortcutKeys = Keys.Control | Keys.S;
			this.SaveMap.Text = "&Save";
			this.SaveMap.Click += SaveMap_Click;
			// 
			// SaveAs
			//
			this.SaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.SaveAs.Text = "&Save As...";
			// 
			// Exit
			//
			this.Exit.ShortcutKeys = Keys.Alt | Keys.F4;
			this.Exit.Text = "E&xit";
			// 
			// mEdit
			//
			System.Windows.Forms.ToolStripMenuItem[] __mcTemp__4 = new System.Windows.Forms.ToolStripMenuItem[3];
			__mcTemp__4[0] = this.mAddPath;
			__mcTemp__4[1] = this.menuItem9;
			__mcTemp__4[2] = this.FillArea;
			this.mEdit.DropDownItems.AddRange(__mcTemp__4);
			this.mEdit.Text = "&Edit";
			// 
			// mAddPath
			//
			this.mAddPath.Text = "Add &Path";
			this.mAddPath.Click += mAddPath_Click;
			// 
			// menuItem9
			//
			this.menuItem9.Text = "-";
			// 
			// FillArea
			//
			this.FillArea.Text = "&Fill Area";
			// 
			// WaveHeader
			// 
			this.WaveHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.WaveHeader.Location = new Point(760, 48);
			this.WaveHeader.Name = "WaveHeader";
			this.WaveHeader.Size = new Size(112, 23);
			this.WaveHeader.TabIndex = 5;
			this.WaveHeader.Text = "Waves: 0";
			// 
			// LoadWaves
			// 
			this.LoadWaves.Filter = "WaveSequence File (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// Status
			// 
			this.Status.Location = new Point(0, 595);
			this.Status.Name = "Status";
			this.Status.Size = new Size(888, 22);
			this.Status.TabIndex = 6;
			this.Status.Text = "Ready";
			// 
			// Path
			// 
			this.Path.Location = new Point(808, 432);
			System.Int32[] __mcTemp__5 = new System.Int32[4];
			__mcTemp__5[0] = 9;
			__mcTemp__5[1] = 0;
			__mcTemp__5[2] = 0;
			__mcTemp__5[3] = 0;
			this.Path.Maximum = new System.Decimal(__mcTemp__5);
			this.Path.Name = "Path";
			this.Path.Size = new Size(40, 20);
			this.Path.TabIndex = 7;
			// 
			// t_path
			// 
			this.t_path.Location = new Point(768, 432);
			this.t_path.Name = "t_path";
			this.t_path.Size = new Size(40, 16);
			this.t_path.TabIndex = 8;
			this.t_path.Text = "Path";
			this.t_path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PathHeader
			// 
			this.PathHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.PathHeader.Location = new Point(760, 80);
			this.PathHeader.Name = "PathHeader";
			this.PathHeader.Size = new Size(112, 23);
			this.PathHeader.TabIndex = 9;
			this.PathHeader.Text = "Paths: 0";
			// 
			// SaveMapOnly
			//
			this.SaveMapOnly.Text = "Save &Map...";
			this.SaveMapOnly.Click += SaveMapOnly_Click;
			// 
			// LevelEditor
			// 
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(888, 617);
			this.Controls.Add(this.PathHeader);
			this.Controls.Add(this.t_path);
			this.Controls.Add(this.Path);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.WaveHeader);
			this.Controls.Add(this.AddPath);
			this.Controls.Add(this.TilePanel);
			this.Controls.Add(this.GameArea);
			//this.Menu = this.mainMenu1; // TODO: ?
			this.Name = "LevelEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "7 Rivers TD Level Editor";
			this.Load += LevelEditor_Load;
			this.Path.EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
