namespace WinFormsUI
{
	partial class DebugWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindow));
			this.lstbOpcodes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblIndexRegister = new System.Windows.Forms.Label();
			this.lblPc = new System.Windows.Forms.Label();
			this.lstbVRegisters = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lstbStack = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblOpcode = new System.Windows.Forms.Label();
			this.btInitialize = new System.Windows.Forms.Button();
			this.btCycle = new System.Windows.Forms.Button();
			this.btRun = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.pbGame = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbGame)).BeginInit();
			this.SuspendLayout();
			// 
			// lstbOpcodes
			// 
			resources.ApplyResources(this.lstbOpcodes, "lstbOpcodes");
			this.lstbOpcodes.FormattingEnabled = true;
			this.lstbOpcodes.Name = "lstbOpcodes";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// lblIndexRegister
			// 
			resources.ApplyResources(this.lblIndexRegister, "lblIndexRegister");
			this.lblIndexRegister.Name = "lblIndexRegister";
			// 
			// lblPc
			// 
			resources.ApplyResources(this.lblPc, "lblPc");
			this.lblPc.Name = "lblPc";
			// 
			// lstbVRegisters
			// 
			resources.ApplyResources(this.lstbVRegisters, "lstbVRegisters");
			this.lstbVRegisters.FormattingEnabled = true;
			this.lstbVRegisters.Name = "lstbVRegisters";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// lstbStack
			// 
			resources.ApplyResources(this.lstbStack, "lstbStack");
			this.lstbStack.FormattingEnabled = true;
			this.lstbStack.Name = "lstbStack";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// lblOpcode
			// 
			resources.ApplyResources(this.lblOpcode, "lblOpcode");
			this.lblOpcode.Name = "lblOpcode";
			// 
			// btInitialize
			// 
			resources.ApplyResources(this.btInitialize, "btInitialize");
			this.btInitialize.Name = "btInitialize";
			this.btInitialize.UseVisualStyleBackColor = true;
			this.btInitialize.Click += new System.EventHandler(this.btInitialize_Click);
			// 
			// btCycle
			// 
			resources.ApplyResources(this.btCycle, "btCycle");
			this.btCycle.Name = "btCycle";
			this.btCycle.UseVisualStyleBackColor = true;
			this.btCycle.Click += new System.EventHandler(this.btCycle_Click);
			// 
			// btRun
			// 
			resources.ApplyResources(this.btRun, "btRun");
			this.btRun.Name = "btRun";
			this.btRun.UseVisualStyleBackColor = true;
			this.btRun.Click += new System.EventHandler(this.btRun_Click);
			// 
			// btStop
			// 
			resources.ApplyResources(this.btStop, "btStop");
			this.btStop.Name = "btStop";
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// pbGame
			// 
			resources.ApplyResources(this.pbGame, "pbGame");
			this.pbGame.BackColor = System.Drawing.Color.Black;
			this.pbGame.Name = "pbGame";
			this.pbGame.TabStop = false;
			// 
			// DebugWindow
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pbGame);
			this.Controls.Add(this.btStop);
			this.Controls.Add(this.btRun);
			this.Controls.Add(this.btCycle);
			this.Controls.Add(this.btInitialize);
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
			this.Name = "DebugWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebugWindow_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DebugWindow_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.pbGame)).EndInit();
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
		private System.Windows.Forms.Button btInitialize;
		private System.Windows.Forms.Button btCycle;
		private System.Windows.Forms.Button btRun;
		private System.Windows.Forms.Button btStop;
		private System.Windows.Forms.PictureBox pbGame;
	}
}

