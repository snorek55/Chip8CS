namespace WinFormsGUI
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
			this.components = new System.ComponentModel.Container();
			this.lstbOpcodes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblIndexRegister = new System.Windows.Forms.Label();
			this.bsInfo = new System.Windows.Forms.BindingSource(this.components);
			this.lblPc = new System.Windows.Forms.Label();
			this.lstbVRegisters = new System.Windows.Forms.ListBox();
			this.bsRegisters = new System.Windows.Forms.BindingSource(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.lstbStack = new System.Windows.Forms.ListBox();
			this.bsStackLevels = new System.Windows.Forms.BindingSource(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblOpcode = new System.Windows.Forms.Label();
			this.btCycle = new System.Windows.Forms.Button();
			this.btRun = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.pbGame = new System.Windows.Forms.PictureBox();
			this.btReset = new System.Windows.Forms.Button();
			this.btLoadRom = new System.Windows.Forms.Button();
			this.ofdLoadRom = new System.Windows.Forms.OpenFileDialog();
			this.lstbGameList = new System.Windows.Forms.ListBox();
			this.bsGamesList = new System.Windows.Forms.BindingSource(this.components);
			this.bsMainView = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.bsInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsRegisters)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsStackLevels)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbGame)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsGamesList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsMainView)).BeginInit();
			this.SuspendLayout();
			// 
			// lstbOpcodes
			// 
			this.lstbOpcodes.FormattingEnabled = true;
			this.lstbOpcodes.Location = new System.Drawing.Point(191, 10);
			this.lstbOpcodes.Name = "lstbOpcodes";
			this.lstbOpcodes.Size = new System.Drawing.Size(115, 459);
			this.lstbOpcodes.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(435, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "IndexRegister";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(435, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "ProgramCounter";
			// 
			// lblIndexRegister
			// 
			this.lblIndexRegister.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInfo, "IndexRegister", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.lblIndexRegister.Location = new System.Drawing.Point(523, 9);
			this.lblIndexRegister.Name = "lblIndexRegister";
			this.lblIndexRegister.Size = new System.Drawing.Size(61, 13);
			this.lblIndexRegister.TabIndex = 3;
			this.lblIndexRegister.Text = "Unintialized";
			// 
			// bsInfo
			// 
			this.bsInfo.DataSource = typeof(Core.ViewModels.DisassemblerInfoViewModel);
			// 
			// lblPc
			// 
			this.lblPc.AutoSize = true;
			this.lblPc.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInfo, "Pc", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.lblPc.Location = new System.Drawing.Point(523, 22);
			this.lblPc.Name = "lblPc";
			this.lblPc.Size = new System.Drawing.Size(61, 13);
			this.lblPc.TabIndex = 3;
			this.lblPc.Text = "Unintialized";
			// 
			// lstbVRegisters
			// 
			this.lstbVRegisters.DataSource = this.bsRegisters;
			this.lstbVRegisters.Enabled = false;
			this.lstbVRegisters.FormattingEnabled = true;
			this.lstbVRegisters.Location = new System.Drawing.Point(339, 96);
			this.lstbVRegisters.Name = "lstbVRegisters";
			this.lstbVRegisters.Size = new System.Drawing.Size(141, 355);
			this.lstbVRegisters.TabIndex = 4;
			// 
			// bsRegisters
			// 
			this.bsRegisters.DataMember = "VRegisters";
			this.bsRegisters.DataSource = this.bsInfo;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(336, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "V registers";
			// 
			// lstbStack
			// 
			this.lstbStack.DataSource = this.bsStackLevels;
			this.lstbStack.Enabled = false;
			this.lstbStack.FormattingEnabled = true;
			this.lstbStack.Location = new System.Drawing.Point(552, 96);
			this.lstbStack.Name = "lstbStack";
			this.lstbStack.Size = new System.Drawing.Size(141, 355);
			this.lstbStack.TabIndex = 4;
			// 
			// bsStackLevels
			// 
			this.bsStackLevels.DataMember = "StackLevels";
			this.bsStackLevels.DataSource = this.bsInfo;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(549, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Stack";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(435, 35);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Opcode";
			// 
			// lblOpcode
			// 
			this.lblOpcode.AutoSize = true;
			this.lblOpcode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInfo, "Opcode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.lblOpcode.Location = new System.Drawing.Point(523, 35);
			this.lblOpcode.Name = "lblOpcode";
			this.lblOpcode.Size = new System.Drawing.Size(61, 13);
			this.lblOpcode.TabIndex = 3;
			this.lblOpcode.Text = "Unintialized";
			// 
			// btCycle
			// 
			this.btCycle.Location = new System.Drawing.Point(747, 40);
			this.btCycle.Name = "btCycle";
			this.btCycle.Size = new System.Drawing.Size(64, 20);
			this.btCycle.TabIndex = 5;
			this.btCycle.Text = "Cycle";
			this.btCycle.UseVisualStyleBackColor = true;
			this.btCycle.Click += new System.EventHandler(this.btCycle_Click);
			// 
			// btRun
			// 
			this.btRun.Location = new System.Drawing.Point(747, 65);
			this.btRun.Name = "btRun";
			this.btRun.Size = new System.Drawing.Size(64, 20);
			this.btRun.TabIndex = 5;
			this.btRun.Text = "Run";
			this.btRun.UseVisualStyleBackColor = true;
			this.btRun.Click += new System.EventHandler(this.btRun_Click);
			// 
			// btStop
			// 
			this.btStop.Location = new System.Drawing.Point(817, 65);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(64, 20);
			this.btStop.TabIndex = 6;
			this.btStop.Text = "Stop";
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// pbGame
			// 
			this.pbGame.BackColor = System.Drawing.Color.Black;
			this.pbGame.Location = new System.Drawing.Point(800, 190);
			this.pbGame.Name = "pbGame";
			this.pbGame.Size = new System.Drawing.Size(219, 111);
			this.pbGame.TabIndex = 7;
			this.pbGame.TabStop = false;
			// 
			// btReset
			// 
			this.btReset.Location = new System.Drawing.Point(817, 40);
			this.btReset.Name = "btReset";
			this.btReset.Size = new System.Drawing.Size(64, 20);
			this.btReset.TabIndex = 9;
			this.btReset.Text = "Reset";
			this.btReset.UseVisualStyleBackColor = true;
			this.btReset.Click += new System.EventHandler(this.btReset_Click);
			// 
			// btLoadRom
			// 
			this.btLoadRom.Location = new System.Drawing.Point(29, 451);
			this.btLoadRom.Name = "btLoadRom";
			this.btLoadRom.Size = new System.Drawing.Size(63, 29);
			this.btLoadRom.TabIndex = 10;
			this.btLoadRom.Text = "Load Rom";
			this.btLoadRom.UseVisualStyleBackColor = true;
			this.btLoadRom.Click += new System.EventHandler(this.btLoadRom_Click);
			// 
			// ofdLoadRom
			// 
			this.ofdLoadRom.Title = "Load ROM...";
			// 
			// lstbGameList
			// 
			this.lstbGameList.DataSource = this.bsGamesList;
			this.lstbGameList.DisplayMember = "Name";
			this.lstbGameList.FormattingEnabled = true;
			this.lstbGameList.Location = new System.Drawing.Point(29, 10);
			this.lstbGameList.Name = "lstbGameList";
			this.lstbGameList.Size = new System.Drawing.Size(124, 420);
			this.lstbGameList.TabIndex = 11;
			// 
			// bsGamesList
			// 
			this.bsGamesList.DataMember = "Games";
			this.bsGamesList.DataSource = this.bsMainView;
			this.bsGamesList.CurrentChanged += new System.EventHandler(this.bsGamesList_CurrentChanged);
			// 
			// bsMainView
			// 
			this.bsMainView.DataSource = typeof(Core.ViewModels.MainViewModel);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1206, 511);
			this.Controls.Add(this.lstbGameList);
			this.Controls.Add(this.btLoadRom);
			this.Controls.Add(this.btReset);
			this.Controls.Add(this.pbGame);
			this.Controls.Add(this.btStop);
			this.Controls.Add(this.btRun);
			this.Controls.Add(this.btCycle);
			this.Controls.Add(this.lblOpcode);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lstbStack);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lstbVRegisters);
			this.Controls.Add(this.lblPc);
			this.Controls.Add(this.lblIndexRegister);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstbOpcodes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Debug";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebugWindow_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DebugWindow_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.bsInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsRegisters)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsStackLevels)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbGame)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsGamesList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsMainView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstbOpcodes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblIndexRegister;
		private System.Windows.Forms.Label lblPc;
		private System.Windows.Forms.ListBox lstbVRegisters;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lstbStack;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblOpcode;
		private System.Windows.Forms.Button btCycle;
		private System.Windows.Forms.Button btRun;
		private System.Windows.Forms.Button btStop;
		private System.Windows.Forms.PictureBox pbGame;
		private System.Windows.Forms.Button btReset;
		private System.Windows.Forms.Button btLoadRom;
		private System.Windows.Forms.OpenFileDialog ofdLoadRom;
		private System.Windows.Forms.ListBox lstbGameList;
		private System.Windows.Forms.BindingSource bsMainView;
		private System.Windows.Forms.BindingSource bsGamesList;
		private System.Windows.Forms.BindingSource bsInfo;
		private System.Windows.Forms.BindingSource bsRegisters;
		private System.Windows.Forms.BindingSource bsStackLevels;
	}
}

