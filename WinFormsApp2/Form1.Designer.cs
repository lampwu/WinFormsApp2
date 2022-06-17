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
            this.pin_value_label = new System.Windows.Forms.Label();
            this.iin_value_label = new System.Windows.Forms.Label();
            this.vin_value_label = new System.Windows.Forms.Label();
            this.pin_label = new System.Windows.Forms.Label();
            this.iin_label = new System.Windows.Forms.Label();
            this.vin_label = new System.Windows.Forms.Label();
            this.expert_button = new System.Windows.Forms.Button();
            this.csv_file_name_textBox = new System.Windows.Forms.TextBox();
            this.csv_file_name_label = new System.Windows.Forms.Label();
            this.record_name_list = new System.Windows.Forms.CheckedListBox();
            this.query_r_name_button = new System.Windows.Forms.Button();
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
            this.start_button.Text = "start";
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
            // pin_value_label
            // 
            this.pin_value_label.AutoSize = true;
            this.pin_value_label.Location = new System.Drawing.Point(202, 355);
            this.pin_value_label.Name = "pin_value_label";
            this.pin_value_label.Size = new System.Drawing.Size(44, 20);
            this.pin_value_label.TabIndex = 10;
            this.pin_value_label.Text = "NAN";
            // 
            // iin_value_label
            // 
            this.iin_value_label.AutoSize = true;
            this.iin_value_label.Location = new System.Drawing.Point(112, 355);
            this.iin_value_label.Name = "iin_value_label";
            this.iin_value_label.Size = new System.Drawing.Size(44, 20);
            this.iin_value_label.TabIndex = 11;
            this.iin_value_label.Text = "NAN";
            // 
            // vin_value_label
            // 
            this.vin_value_label.AutoSize = true;
            this.vin_value_label.Location = new System.Drawing.Point(32, 355);
            this.vin_value_label.Name = "vin_value_label";
            this.vin_value_label.Size = new System.Drawing.Size(44, 20);
            this.vin_value_label.TabIndex = 12;
            this.vin_value_label.Text = "NAN";
            // 
            // pin_label
            // 
            this.pin_label.AutoSize = true;
            this.pin_label.Location = new System.Drawing.Point(202, 312);
            this.pin_label.Name = "pin_label";
            this.pin_label.Size = new System.Drawing.Size(31, 20);
            this.pin_label.TabIndex = 13;
            this.pin_label.Text = "Pin";
            // 
            // iin_label
            // 
            this.iin_label.AutoSize = true;
            this.iin_label.Location = new System.Drawing.Point(112, 312);
            this.iin_label.Name = "iin_label";
            this.iin_label.Size = new System.Drawing.Size(26, 20);
            this.iin_label.TabIndex = 14;
            this.iin_label.Text = "Iin";
            // 
            // vin_label
            // 
            this.vin_label.AutoSize = true;
            this.vin_label.Location = new System.Drawing.Point(32, 312);
            this.vin_label.Name = "vin_label";
            this.vin_label.Size = new System.Drawing.Size(32, 20);
            this.vin_label.TabIndex = 15;
            this.vin_label.Text = "Vin";
            // 
            // expert_button
            // 
            this.expert_button.Location = new System.Drawing.Point(395, 239);
            this.expert_button.Name = "expert_button";
            this.expert_button.Size = new System.Drawing.Size(96, 63);
            this.expert_button.TabIndex = 16;
            this.expert_button.Text = "expert data";
            this.expert_button.UseVisualStyleBackColor = true;
            this.expert_button.Click += new System.EventHandler(this.expert_button_Click);
            // 
            // csv_file_name_textBox
            // 
            this.csv_file_name_textBox.Location = new System.Drawing.Point(395, 195);
            this.csv_file_name_textBox.Name = "csv_file_name_textBox";
            this.csv_file_name_textBox.Size = new System.Drawing.Size(224, 27);
            this.csv_file_name_textBox.TabIndex = 17;
            // 
            // csv_file_name_label
            // 
            this.csv_file_name_label.AutoSize = true;
            this.csv_file_name_label.Location = new System.Drawing.Point(389, 172);
            this.csv_file_name_label.Name = "csv_file_name_label";
            this.csv_file_name_label.Size = new System.Drawing.Size(102, 20);
            this.csv_file_name_label.TabIndex = 18;
            this.csv_file_name_label.Text = "csv file name";
            // 
            // record_name_list
            // 
            this.record_name_list.FormattingEnabled = true;
            this.record_name_list.Location = new System.Drawing.Point(625, 27);
            this.record_name_list.Name = "record_name_list";
            this.record_name_list.Size = new System.Drawing.Size(150, 334);
            this.record_name_list.TabIndex = 19;
            // 
            // query_r_name_button
            // 
            this.query_r_name_button.Location = new System.Drawing.Point(625, 367);
            this.query_r_name_button.Name = "query_r_name_button";
            this.query_r_name_button.Size = new System.Drawing.Size(150, 48);
            this.query_r_name_button.TabIndex = 20;
            this.query_r_name_button.Text = "query record names";
            this.query_r_name_button.UseVisualStyleBackColor = true;
            this.query_r_name_button.Click += new System.EventHandler(this.query_r_name_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.query_r_name_button);
            this.Controls.Add(this.record_name_list);
            this.Controls.Add(this.csv_file_name_label);
            this.Controls.Add(this.csv_file_name_textBox);
            this.Controls.Add(this.expert_button);
            this.Controls.Add(this.vin_label);
            this.Controls.Add(this.iin_label);
            this.Controls.Add(this.pin_label);
            this.Controls.Add(this.vin_value_label);
            this.Controls.Add(this.iin_value_label);
            this.Controls.Add(this.pin_value_label);
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
        private Label pin_value_label;
        private Label iin_value_label;
        private Label vin_value_label;
        private Label pin_label;
        private Label iin_label;
        private Label vin_label;
        private Button expert_button;
        private TextBox csv_file_name_textBox;
        private Label csv_file_name_label;
        private CheckedListBox record_name_list;
        private Button query_r_name_button;
    }
}