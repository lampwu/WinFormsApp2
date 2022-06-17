using VisaComLib;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using CsvHelper;
using System.Globalization;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class usbSendAndRead
        {
            static ResourceManager? resourceManager;
            static FormattedIO488? ioObject;
            public static bool Write(string address, string command)
            {
                resourceManager = new ResourceManager();
                ioObject = new FormattedIO488();
                try
                {
                    ioObject.IO = (IMessage)resourceManager.Open(address, AccessMode.NO_LOCK, 0, "");
                    Thread.Sleep(20);
                    ioObject.WriteString(data: command, flushAndEND: true);

                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    try { ioObject.IO.Close(); }
                    catch { }
                    try { Marshal.ReleaseComObject(ioObject); }
                    catch { }
                    try { Marshal.ReleaseComObject(resourceManager); }
                    catch { }
                }

            }

            public static bool Read(string address, out string valueRead)
            {
                resourceManager = new ResourceManager();
                ioObject = new FormattedIO488();

                //string addr = $"GPIB::{address.ToString()}::INSTR";

                try
                {
                    ioObject.IO = (IMessage)resourceManager.Open(address, AccessMode.NO_LOCK, 0, "");
                    Thread.Sleep(20);
                    valueRead = ioObject.ReadString();

                    return true;
                }
                catch
                {
                    valueRead = "";
                    return false;
                }
                finally
                {
                    try { ioObject.IO.Close(); }
                    catch { }
                    try { Marshal.ReleaseComObject(ioObject); }
                    catch { }
                    try { Marshal.ReleaseComObject(resourceManager); }
                    catch { }
                }
            }
        }

        bool start_stop = false;
        private async void start_button_Click(object sender, EventArgs e)
        {
            powermeter_add.Enabled = false;
            record_name_textbox.Enabled = false;
            interval_textBox.Enabled = false;
            start_button.Enabled = false;

            start_stop = true;
            scan_state.BackColor = Color.Green;
            scan_state.Text = "running";
            var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS chargePC (recordname text, U text, I text,P text, record_datetime text)";
            cmd.ExecuteNonQuery();
            /*cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();*/


            string equment_add = powermeter_add.Text;
            string re_name = record_name_textbox.Text;
            //voltage and current range, mode, avg, update rate setting
            usbSendAndRead.Write(equment_add, ":INPUT:VOLTAGE:AUTO ON");
            usbSendAndRead.Write(equment_add, ":INPUT:current:AUTO ON");
            usbSendAndRead.Write(equment_add, ":INPUT:MODE RMS");
            usbSendAndRead.Write(equment_add, ":MEASure:AVERaging:STATe ON");
            usbSendAndRead.Write(equment_add, ":RATE 250MS");
            //display setting
            usbSendAndRead.Write(equment_add, ":DISPLAY:NORMAL:ITEM1 U,1");
            usbSendAndRead.Write(equment_add, ":DISPLAY:NORMAL:ITEM2 I,1");
            usbSendAndRead.Write(equment_add, ":DISPLAY:NORMAL:ITEM3 P,1");
            //measure value setting
            usbSendAndRead.Write(equment_add, ":NUMERIC:NORMAL:ITEM1 U,1");
            usbSendAndRead.Write(equment_add, ":NUMERIC:NORMAL:ITEM2 I,1");
            usbSendAndRead.Write(equment_add, ":NUMERIC:NORMAL:ITEM3 P,1");

            int milliseconds = short.Parse(interval_textBox.Text);
            
            await Task.Delay(milliseconds);

            while (start_stop == true)
            {
                string read_list;
                //fetch measure value
                usbSendAndRead.Write(equment_add, ":NUMERIC:NORMAL:VALUE?");
                usbSendAndRead.Read(equment_add, out read_list);
                string[] read_arr = read_list.Split(',');

                /*List<float> readList = new List<float>();

                readList.Add(float.Parse(read_arr[0]));
                readList.Add(float.Parse(read_arr[1]));
                readList.Add(float.Parse(read_arr[2]));*/
                //insertuip(re_name,read_arr[0], read_arr[1], read_arr[2]);
                DateTime just_now = DateTime.Now; // 12/20/2015 11:48:09 AM  
                cmd.CommandText = "INSERT INTO chargePC Values ('" + re_name + "', '" + read_arr[0] + "', '" + read_arr[1] + "', '" + read_arr[2] + "', '" + just_now + "');";
                cmd.ExecuteNonQuery();
                float vinf = float.Parse(read_arr[0]);
                float iinf = float.Parse(read_arr[1]);
                float pinf = float.Parse(read_arr[2]);

                vin_value_label.Text = vinf.ToString();
                iin_value_label.Text = iinf.ToString();
                pin_value_label.Text = pinf.ToString();

                await Task.Delay(milliseconds);
                //Thread.Sleep(milliseconds);

                //float.Parse(read_arr[1]);


            }
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();

        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            start_stop = false;
            scan_state.BackColor = Color.White;
            scan_state.Text = "stop";
            vin_value_label.Text = "NAN";
            iin_value_label.Text = "NAN";
            pin_value_label.Text = "NAN";
            start_button.Enabled = true;
            powermeter_add.Enabled = true;
            record_name_textbox.Enabled = true;
            interval_textBox.Enabled = true;

        }

        public class measurelist
        {
            public string? Vin { get; set; }
            public string? Iin { get; set; }
            public string? Pin { get; set; }

            public string? retime { get; set; }

        }

        //expert measure result
        private async void expert_button_Click(object sender, EventArgs e)
        {
            string target_directory = "";
            csv_file_name_textBox.Enabled = false;
            expert_button.Enabled = false;
            if (csv_file_name_textBox.Text == "") 
            {
                csv_file_name_textBox.Enabled = true;
                expert_button.Enabled = false;
                csv_file_name_textBox.Focus();
                MessageBox.Show("please input csv file name", "Message");
                return; 
            }
            
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    target_directory = fbd.SelectedPath;
                    target_directory = target_directory + "/" + csv_file_name_textBox.Text + ".csv";

                    //MessageBox.Show("dir: " + target_directory, "Message");
                }
            }


            var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            var resultList = new List<string>();


            cmd.CommandText = "select * from chargePC;";
            var reader = await cmd.ExecuteReaderAsync();
            var records = new List<measurelist>();

            while (await reader.ReadAsync())
            {
                //var name = reader.GetString(0);
                records.Add(new measurelist
                {
                    Vin = reader.GetString(0),
                    Iin = reader.GetString(1),
                    Pin = reader.GetString(2),
                    retime = reader.GetString(3)

                });
            }
            using (var writer = new StreamWriter(path: target_directory))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();
            records.Clear();
            MessageBox.Show("hello", "Message");
            csv_file_name_textBox.Enabled = true;
            expert_button.Enabled = true;
            return;
        }

        public class record_name_list
        {
            public string? name { get; set; }

        }

        private async void query_r_name_button_Click(object sender, EventArgs e)
        {
            var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            //var resultList = new List<string>();


            cmd.CommandText = "select DISTINCT recordname from chargePC;";
            var reader = await cmd.ExecuteReaderAsync();
            var records = new List<record_name_list>();

            while (await reader.ReadAsync())
            {
                //var name = reader.GetString(0);
                records.Add(new record_name_list
                {
                    name = reader.GetString(0)
                    
                });
            }
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();
            records.Clear();

        }
    }
}