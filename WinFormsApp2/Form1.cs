using VisaComLib;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

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
            start_stop = true;
            scan_state.BackColor = Color.Green;
            scan_state.Text = "running";
            var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=./database.db");
            sqlite.Open();
            var cmd = sqlite.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS chargePC (id int primary key, recordname text, U text, I text,P text, record_datetime text)";
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

            /*var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                //return 42;
            });
            t.Wait();*/
            //Thread.Sleep(milliseconds);
            //TestCorrect();
            await Task.Delay(500);

            while (start_stop == true)
            {
                string read_list;
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

                await Task.Delay(500);
                //Thread.Sleep(milliseconds);

                //float.Parse(read_arr[1]);


            }
            cmd.Dispose();
            sqlite.Close();
            sqlite.Dispose();

        }

        /*private static async Task TestCorrect()
        {
            await Task.Run(async () => //Task.Run automatically unwraps nested Task types!
            {
                //Console.WriteLine("Start");
                await Task.Delay(1000);
                //Console.WriteLine("Done");
            });
            //Console.WriteLine("All done");
        }*/

        private void Stop_button_Click(object sender, EventArgs e)
        {
            start_stop = false;
            scan_state.BackColor = Color.White;
            scan_state.Text = "stop";
            vin_value_label.Text = "NAN";
            iin_value_label.Text = "NAN";
            pin_value_label.Text = "NAN";
        }
    }
}