namespace WinFormsApp2
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
            this.scan_state = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.record_name_lab = new System.Windows.Forms.Label();
            this.Stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.powermeter_add = new System.Windows.Forms.TextBox();
            this.record_name_textbox = new System.Windows.Forms.TextBox();
            this.interval_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scan_state
            // 
            this.scan_state.AutoSize = true;
            this.scan_state.Location = new System.Drawing.Point(491, 116);
            this.scan_state.Name = "scan_state";
            this.scan_state.Size = new System.Drawing.Size(42, 20);
            this.scan_state.TabIndex = 0;
            this.scan_state.Text = "stop";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "wt310 address";
            // 
            // record_name_lab
            // 
            this.record_name_lab.AutoSize = true;
            this.record_name_lab.Location = new System.Drawing.Point(12, 89);
            this.record_name_lab.Name = "record_name_lab";
            this.record_name_lab.Size = new System.Drawing.Size(102, 20);
            this.record_name_lab.TabIndex = 3;
            this.record_name_lab.Text = "record name";
            // 
            // Stop_button
            // 
            this.Stop_button.Location = new System.Drawing.Point(184, 239);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(94, 29);
            this.Stop_button.TabIndex = 4;
            this.Stop_button.Text = "stop";
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(32, 239);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(94, 29);
            this.start_button.TabIndex = 5;
            this.start_button.Text = "Start";
            this.start_button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // powermeter_add
            // 
            this.powermeter_add.Location = new System.Drawing.Point(12, 41);
            this.powermeter_add.Name = "powermeter_add";
            this.powermeter_add.Size = new System.Drawing.Size(532, 27);
            this.powermeter_add.TabIndex = 6;
            // 
            // record_name_textbox
            // 
            this.record_name_textbox.Location = new System.Drawing.Point(12, 130);
            this.record_name_textbox.Name = "record_name_textbox";
            this.record_name_textbox.Size = new System.Drawing.Size(210, 27);
            this.record_name_textbox.TabIndex = 7;
            // 
            // interval_textBox
            // 
            this.interval_textBox.Location = new System.Drawing.Point(12, 195);
            this.interval_textBox.Name = "interval_textBox";
            this.interval_textBox.Size = new System.Drawing.Size(125, 27);
            this.interval_textBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "interval";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.interval_textBox);
            this.Controls.Add(this.record_name_textbox);
            this.Controls.Add(this.powermeter_add);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.Stop_button);
            this.Controls.Add(this.record_name_lab);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scan_state);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label scan_state;
        private Label label3;
        private Label record_name_lab;
        private Button Stop_button;
        private Button start_button;
        private TextBox powermeter_add;
        private TextBox record_name_textbox;
        private TextBox interval_textBox;
        private Label label1;
    }
}