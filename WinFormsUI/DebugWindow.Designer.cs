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
			this.lstbOpcodes.Enabled = false;
			this.lstbOpcodes.FormattingEnabled = true;
			this.lstbOpcodes.ItemHeight = 15;
			this.lstbOpcodes.Location = new System.Drawing.Point(12, 12);
			this.lstbOpcodes.Name = "lstbOpcodes";
			this.lstbOpcodes.Size = new System.Drawing.Size(247, 529);
			this.lstbOpcodes.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(265, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "IndexRegister";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(265, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "ProgramCounter";
			// 
			// lblIndexRegister
			// 
			this.lblIndexRegister.AutoSize = true;
			this.lblIndexRegister.Location = new System.Drawing.Point(367, 10);
			this.lblIndexRegister.Name = "lblIndexRegister";
			this.lblIndexRegister.Size = new System.Drawing.Size(69, 15);
			this.lblIndexRegister.TabIndex = 3;
			this.lblIndexRegister.Text = "Unintialized";
			// 
			// lblPc
			// 
			this.lblPc.AutoSize = true;
			this.lblPc.Location = new System.Drawing.Point(367, 25);
			this.lblPc.Name = "lblPc";
			this.lblPc.Size = new System.Drawing.Size(69, 15);
			this.lblPc.TabIndex = 3;
			this.lblPc.Text = "Unintialized";
			// 
			// lstbVRegisters
			// 
			this.lstbVRegisters.Enabled = false;
			this.lstbVRegisters.FormattingEnabled = true;
			this.lstbVRegisters.ItemHeight = 15;
			this.lstbVRegisters.Location = new System.Drawing.Point(265, 129);
			this.lstbVRegisters.Name = "lstbVRegisters";
			this.lstbVRegisters.Size = new System.Drawing.Size(164, 409);
			this.lstbVRegisters.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(265, 111);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "V registers";
			// 
			// lstbStack
			// 
			this.lstbStack.Enabled = false;
			this.lstbStack.FormattingEnabled = true;
			this.lstbStack.ItemHeight = 15;
			this.lstbStack.Location = new System.Drawing.Point(435, 129);
			this.lstbStack.Name = "lstbStack";
			this.lstbStack.Size = new System.Drawing.Size(164, 409);
			this.lstbStack.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(443, 111);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "Stack";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(265, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Opcode";
			// 
			// lblOpcode
			// 
			this.lblOpcode.AutoSize = true;
			this.lblOpcode.Location = new System.Drawing.Point(367, 40);
			this.lblOpcode.Name = "lblOpcode";
			this.lblOpcode.Size = new System.Drawing.Size(69, 15);
			this.lblOpcode.TabIndex = 3;
			this.lblOpcode.Text = "Unintialized";
			// 
			// btInitialize
			// 
			this.btInitialize.Location = new System.Drawing.Point(624, 10);
			this.btInitialize.Name = "btInitialize";
			this.btInitialize.Size = new System.Drawing.Size(75, 23);
			this.btInitialize.TabIndex = 5;
			this.btInitialize.Text = "Initialize";
			this.btInitialize.UseVisualStyleBackColor = true;
			this.btInitialize.Click += new System.EventHandler(this.btInitialize_Click);
			// 
			// btCycle
			// 
			this.btCycle.Location = new System.Drawing.Point(624, 43);
			this.btCycle.Name = "btCycle";
			this.btCycle.Size = new System.Drawing.Size(75, 23);
			this.btCycle.TabIndex = 5;
			this.btCycle.Text = "Cycle";
			this.btCycle.UseVisualStyleBackColor = true;
			this.btCycle.Click += new System.EventHandler(this.btCycle_Click);
			// 
			// btRun
			// 
			this.btRun.Location = new System.Drawing.Point(624, 72);
			this.btRun.Name = "btRun";
			this.btRun.Size = new System.Drawing.Size(75, 23);
			this.btRun.TabIndex = 5;
			this.btRun.Text = "Run";
			this.btRun.UseVisualStyleBackColor = true;
			this.btRun.Click += new System.EventHandler(this.btRun_Click);
			// 
			// btStop
			// 
			this.btStop.Location = new System.Drawing.Point(705, 72);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(75, 23);
			this.btStop.TabIndex = 6;
			this.btStop.Text = "Stop";
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// pbGame
			// 
			this.pbGame.Location = new System.Drawing.Point(642, 189);
			this.pbGame.Name = "pbGame";
			this.pbGame.Size = new System.Drawing.Size(128, 64);
			this.pbGame.TabIndex = 7;
			this.pbGame.TabStop = false;
			this.pbGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pbGame_Paint);
			// 
			// DebugWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(784, 561);
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
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Debug";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebugWindow_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DebugWindow_KeyPress);
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

