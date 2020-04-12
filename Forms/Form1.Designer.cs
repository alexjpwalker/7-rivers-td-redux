using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SevenRiversTD.Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new Container();
			this.StartGame = new Label();
			this.ControlPanel = new Panel();
			this.GameArea = new Panel();
			this.StartEditor = new Label();
			this.GameLoop = new Timer(this.components);
			this.InfoPanel = new Panel();
			this.ThisWave = new Label();
			this.t_cur = new Label();
			this.NextWave = new Label();
			this.t_next = new Label();
			this.SpeedLabel = new Label();
			this.t_sp = new Label();
			this.GoldBox = new Label();
			this.t_g = new Label();
			this.t_hp = new Label();
			this.HealthBox = new Label();
			this.LoadLevel = new OpenFileDialog();
			this.LoadWaves = new OpenFileDialog();
			this.GameArea.SuspendLayout();
			this.InfoPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// StartGame
			// 
			this.StartGame.BackColor = System.Drawing.Color.OliveDrab;
			this.StartGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StartGame.Location = new System.Drawing.Point(328, 280);
			this.StartGame.Name = "StartGame";
			this.StartGame.Size = new System.Drawing.Size(80, 23);
			this.StartGame.TabIndex = 0;
			this.StartGame.Text = "Start game";
			this.StartGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.StartGame.Click += StartGame_Click;
			this.StartGame.MouseEnter += StartGame_MouseEnter;
			this.StartGame.MouseLeave += StartGame_MouseLeave;
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DimGray;
			this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ControlPanel.Location = new System.Drawing.Point(736, 64);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(192, 656);
			this.ControlPanel.TabIndex = 1;
			this.ControlPanel.MouseUp += ControlPanel_MouseUp;
			this.ControlPanel.Paint += ControlPanel_Paint;
			this.ControlPanel.MouseMove += ControlPanel_MouseMove;
			this.ControlPanel.MouseLeave += ControlPanel_MouseLeave;
			this.ControlPanel.MouseDown += ControlPanel_MouseDown;
			// 
			// GameArea
			// 
			this.GameArea.Controls.Add(this.StartEditor);
			this.GameArea.Controls.Add(this.StartGame);
			this.GameArea.Location = new System.Drawing.Point(0, 64);
			this.GameArea.Name = "GameArea";
			this.GameArea.Size = new System.Drawing.Size(736, 600);
			this.GameArea.TabIndex = 2;
			this.GameArea.MouseUp += GameArea_MouseUp;
			this.GameArea.MouseMove += GameArea_MouseMove;
			this.GameArea.MouseLeave += GameArea_MouseLeave;
			this.GameArea.MouseDown += GameArea_MouseDown;
			// 
			// StartEditor
			// 
			this.StartEditor.BackColor = System.Drawing.Color.SeaGreen;
			this.StartEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StartEditor.Location = new System.Drawing.Point(328, 320);
			this.StartEditor.Name = "StartEditor";
			this.StartEditor.Size = new System.Drawing.Size(80, 23);
			this.StartEditor.TabIndex = 1;
			this.StartEditor.Text = "Level editor...";
			this.StartEditor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.StartEditor.Click += StartEditor_Click;
			// 
			// GameLoop
			// 
			this.GameLoop.Interval = 40;
			this.GameLoop.Tick += GameLoop_Tick;
			// 
			// InfoPanel
			// 
			this.InfoPanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.InfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.InfoPanel.Controls.Add(this.ThisWave);
			this.InfoPanel.Controls.Add(this.t_cur);
			this.InfoPanel.Controls.Add(this.NextWave);
			this.InfoPanel.Controls.Add(this.t_next);
			this.InfoPanel.Controls.Add(this.SpeedLabel);
			this.InfoPanel.Controls.Add(this.t_sp);
			this.InfoPanel.Controls.Add(this.GoldBox);
			this.InfoPanel.Controls.Add(this.t_g);
			this.InfoPanel.Controls.Add(this.t_hp);
			this.InfoPanel.Controls.Add(this.HealthBox);
			this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.InfoPanel.Location = new System.Drawing.Point(0, 0);
			this.InfoPanel.Name = "InfoPanel";
			this.InfoPanel.Size = new System.Drawing.Size(920, 64);
			this.InfoPanel.TabIndex = 3;
			// 
			// ThisWave
			// 
			this.ThisWave.BackColor = System.Drawing.Color.LightGray;
			this.ThisWave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ThisWave.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.ThisWave.Location = new System.Drawing.Point(488, 8);
			this.ThisWave.Name = "ThisWave";
			this.ThisWave.Size = new System.Drawing.Size(160, 24);
			this.ThisWave.TabIndex = 9;
			this.ThisWave.Text = "None";
			this.ThisWave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ThisWave.Click += NextEnemy;
			// 
			// t_cur
			// 
			this.t_cur.BackColor = System.Drawing.Color.LightGray;
			this.t_cur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.t_cur.Font = new Font("Copperplate Gothic Bold", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.t_cur.Location = new System.Drawing.Point(352, 8);
			this.t_cur.Name = "t_cur";
			this.t_cur.Size = new System.Drawing.Size(136, 24);
			this.t_cur.TabIndex = 8;
			this.t_cur.Text = "Current (0)";
			this.t_cur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.t_cur.Click += NextEnemy;
			// 
			// NextWave
			// 
			this.NextWave.BackColor = System.Drawing.Color.LightGray;
			this.NextWave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.NextWave.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.NextWave.Location = new System.Drawing.Point(488, 32);
			this.NextWave.Name = "NextWave";
			this.NextWave.Size = new System.Drawing.Size(160, 24);
			this.NextWave.TabIndex = 7;
			this.NextWave.Text = "Normal";
			this.NextWave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.NextWave.Click += NextEnemy;
			// 
			// t_next
			// 
			this.t_next.BackColor = System.Drawing.Color.LightGray;
			this.t_next.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.t_next.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.t_next.Location = new System.Drawing.Point(352, 32);
			this.t_next.Name = "t_next";
			this.t_next.Size = new System.Drawing.Size(136, 24);
			this.t_next.TabIndex = 6;
			this.t_next.Text = "Next Wave:";
			this.t_next.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.t_next.Click += NextEnemy;
			// 
			// SpeedLabel
			// 
			this.SpeedLabel.BackColor = System.Drawing.Color.DarkBlue;
			this.SpeedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SpeedLabel.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.SpeedLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.SpeedLabel.Location = new System.Drawing.Point(776, 16);
			this.SpeedLabel.Name = "SpeedLabel";
			this.SpeedLabel.Size = new System.Drawing.Size(104, 24);
			this.SpeedLabel.TabIndex = 5;
			this.SpeedLabel.Text = "Medium";
			this.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SpeedLabel.MouseDown += SpeedLabel_MouseDown;
			// 
			// t_sp
			// 
			this.t_sp.BackColor = System.Drawing.Color.DarkBlue;
			this.t_sp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.t_sp.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.t_sp.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.t_sp.Location = new System.Drawing.Point(688, 16);
			this.t_sp.Name = "t_sp";
			this.t_sp.Size = new System.Drawing.Size(88, 24);
			this.t_sp.TabIndex = 4;
			this.t_sp.Text = "Speed:";
			this.t_sp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GoldBox
			// 
			this.GoldBox.BackColor = System.Drawing.Color.Gold;
			this.GoldBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.GoldBox.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.GoldBox.Location = new System.Drawing.Point(232, 16);
			this.GoldBox.Name = "GoldBox";
			this.GoldBox.Size = new System.Drawing.Size(88, 24);
			this.GoldBox.TabIndex = 3;
			this.GoldBox.Text = "50";
			this.GoldBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// t_g
			// 
			this.t_g.BackColor = System.Drawing.Color.Gold;
			this.t_g.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.t_g.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.t_g.Location = new System.Drawing.Point(160, 16);
			this.t_g.Name = "t_g";
			this.t_g.Size = new System.Drawing.Size(72, 24);
			this.t_g.TabIndex = 2;
			this.t_g.Text = "Gold:";
			this.t_g.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// t_hp
			// 
			this.t_hp.BackColor = System.Drawing.Color.LimeGreen;
			this.t_hp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.t_hp.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.t_hp.Location = new System.Drawing.Point(40, 16);
			this.t_hp.Name = "t_hp";
			this.t_hp.Size = new System.Drawing.Size(48, 24);
			this.t_hp.TabIndex = 1;
			this.t_hp.Text = "HP:";
			this.t_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// HealthBox
			// 
			this.HealthBox.BackColor = System.Drawing.Color.LimeGreen;
			this.HealthBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.HealthBox.Font = new Font("Copperplate Gothic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0);
			this.HealthBox.Location = new System.Drawing.Point(88, 16);
			this.HealthBox.Name = "HealthBox";
			this.HealthBox.Size = new System.Drawing.Size(48, 24);
			this.HealthBox.TabIndex = 0;
			this.HealthBox.Text = "20";
			this.HealthBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LoadLevel
			// 
			this.LoadLevel.Filter = "Level File (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// LoadWaves
			// 
			this.LoadWaves.Filter = "WaveSequence File (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightGreen;
			this.ClientSize = new System.Drawing.Size(920, 664);
			this.Controls.Add(this.InfoPanel);
			this.Controls.Add(this.GameArea);
			this.Controls.Add(this.ControlPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "7 Rivers Tower Defence";
			this.KeyDown += Form1_KeyDown;
			this.Closing += Form1_Closing;
			this.GameArea.ResumeLayout(false);
			this.InfoPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
	}
}

