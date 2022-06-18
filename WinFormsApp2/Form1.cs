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
using System.Diagnostics;
using System.Data;

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
                //https://stackoverflow.com/questions/12939501/insert-into-c-sharp-with-sqlcommand
                cmd.CommandText = "INSERT INTO chargePC Values (@param1,@param2,@param3,@param4,@param5);";
                DateTime just_now = DateTime.Now; // 12/20/2015 11:48:09 AM  
                cmd.Parameters.Add("@param1", DbType.String).Value = re_name;
                cmd.Parameters.Add("@param2", DbType.String).Value = read_arr[0];
                cmd.Parameters.Add("@param3", DbType.String).Value = read_arr[1];
                cmd.Parameters.Add("@param4",DbType.String).Value = read_arr[2];
                cmd.Parameters.Add("@param4", DbType.DateTime).Value = just_now;
                cmd.CommandType = CommandType.Text;


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
                }
            }


            var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            var resultList = new List<string>();
            if (record_name_list.CheckedItems.Count < 1)
            {
                MessageBox.Show("No record name selected", "Message");

                return;
            };

            if (record_name_list.Items.Count == 0)
            {
                MessageBox.Show("Free record data", "Message");
                return;
            }

            //SQLiteParameter rq = new SQLiteParameter();
            //cmd.Parameters.Add(rq);
            //rq.Value = record_name_list.SelectedItem;
            //cmd.Parameters.Add(new SQLiteParameter("@renamequery", record_name_list.SelectedItem));
            var records = new List<measurelist>();
            List<string> res = new List<string>();
            foreach (var item in record_name_list.CheckedItems)
            {
                res.Add((string)item);
                Debug.WriteLine(item.ToString());
            }
            //string res = string.Join(",", record_name_list.CheckedItems);
            //Debug.WriteLine(res.ToString());
            //string combinedString = string.Join(",", res.ToArray());
            //Debug.WriteLine(combinedString);

            //cmd.CommandText = "select * from chargePC where recordname in (@renamequery);";

            //cmd.Parameters.Add("@renamequery", DbType.String);
            //cmd.Parameters["@renamequery"].Value = combinedString;
            //cmd.Parameters.AddWithValue("@renamequery", combinedString);
            //https://stackoverflow.com/questions/2377506/pass-array-parameter-in-sqlcommand
            var parameters = new string[record_name_list.CheckedItems.Count];

            for (int i = 0; i < record_name_list.CheckedItems.Count; i++)
            {
                parameters[i] = string.Format("'{0}'", res[i]);
                cmd.Parameters.AddWithValue(parameters[i], res[i]);
            }

            cmd.CommandText = string.Format("select * from chargePC where recordname in ({0});", string.Join(", ", parameters));
            //cmd.CommandText = "select * from chargePC where recordname in ('adsadasd','hhhj');";
            Debug.WriteLine(cmd.CommandText);

            var reader = await cmd.ExecuteReaderAsync();

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
            records.Clear();
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();
            MessageBox.Show("export complete", "Message");
            csv_file_name_textBox.Enabled = true;
            expert_button.Enabled = true;
            return;
        }

        /*public class recordname
        {
            public string? name { get; set; }

        }*/

        //query record name
        private async void query_r_name_button_Click(object sender, EventArgs e)
        {
            record_name_list.Items.Clear();
            var sqlite = new SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            cmd.CommandText = "select DISTINCT recordname from chargePC;";
            var reader = await cmd.ExecuteReaderAsync();
            List<string> recordname = new List<string>();

            while (await reader.ReadAsync())
            {
                recordname.Add(reader.GetString(0));
            }
            //Debug.WriteLine(AuthorList);
            //Debug.WriteLine(AuthorList.Count);
            //Debug.WriteLine("hello");
            record_name_list.Items.AddRange(recordname.ToArray());
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();
            recordname.Clear();
        }
    }
}