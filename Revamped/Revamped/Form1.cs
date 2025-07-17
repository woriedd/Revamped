using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XDevkit;
using JRPC_Client;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;
using SupItsTom.API;

namespace Revamped
{
    public partial class Form1 : Form
    {
        IXboxConsole Console;
        private readonly Serenity serenity = new Serenity();
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            pathToBlackOps2.Text = Properties.Settings.Default.bo2Path;
            /*this.serenity.GetFilesOfTheDay(Serenity.FileType.S6);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.S5);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.S4);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.X6);*/
            this.serenity.GetFilesOfTheDay(Serenity.FileType.V2);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.XV2);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.PTB);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.XPTB);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.RGG);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.RGGX);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.KNIFE);
            this.serenity.GetFilesOfTheDay(Serenity.FileType.DSR);
            //this.serenity.GetFilesOfTheDay(Serenity.FileType.X6);
        }
        private void connect_Click(object sender, EventArgs e)
        {
            Connection();
            /*if (Console.Connect(out Console))
            {
                JRPC.XNotify(this.Console, "Revamped Tool:\nConnected");
            }
            else
            {
                MessageBox.Show("Failed to connect");
            }*/
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool flag = this.pathToBlackOps2.Text != null && this.bo2Drive.SelectedIndex != -1;
            if (flag)
            {
                Properties.Settings.Default.bo2Path = this.pathToBlackOps2.Text;
                Properties.Settings.Default.bo2Drive = this.bo2Drive.SelectedIndex.ToString();
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag3 = this.pathToBlackOps2.Text != null && this.bo2Drive.SelectedIndex != -1;
            if (flag3)
            {
                Properties.Settings.Default.bo2Path = this.pathToBlackOps2.Text;
                Properties.Settings.Default.bo2Drive = this.bo2Drive.SelectedIndex.ToString();
                bool flag4 = this.pathToBlackOps2.Text.Contains("default_mp.xex") || this.pathToBlackOps2.Text.Contains("Default_mp.xex") || this.pathToBlackOps2.Text.Contains("DEFUALT_MP.xex");
                if (flag4)
                {
                    string text = Path.Combine(new string[]
                    {
                        this.bo2Drive.Text + this.pathToBlackOps2.Text
                    });
                    string mediaDirectory = text.Replace("\\default_mp.xex", "");
                    Console.Reboot(text, mediaDirectory, null, XboxRebootFlags.Title);
                }
                else
                {
                    bool flag5 = !this.pathToBlackOps2.Text.Contains("default_mp.xex") || !this.pathToBlackOps2.Text.Contains("Default_mp.xex") || !this.pathToBlackOps2.Text.Contains("DEFUALT_MP.xex");
                    if (flag5)
                    {
                        string mediaDirectory2 = Path.Combine(new string[]
                        {
                            this.bo2Drive.Text + this.pathToBlackOps2.Text
                        });
                        string text2 = Path.Combine(new string[]
                        {
                            this.bo2Drive.Text + this.pathToBlackOps2.Text + "\\default_mp.xex"
                        });
                        int num = text2.LastIndexOf("\\\\", StringComparison.Ordinal);
                        text2 = ((num > -1) ? text2.Substring(0, num) : text2);
                        Console.Reboot(text2 + "\\default_mp.xex", mediaDirectory2, null, XboxRebootFlags.Title);
                    }
                    else
                    {
                        MessageBox.Show("Make sure to put your path like this 'Games\\Black Ops 2' or '\\Black Ops 2\\default_mp.xex.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Make sure to include a Black Ops 2 Path before clicking the button.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Type in your black ops 2 game drive like this 'Games\\Black Ops 2\\' and launch. It will save your path in the text box on load.\nTHIS IS CASE SENSETIVE!!");
        }

        private void Reboot_Click(object sender, EventArgs e)
        {
            this.Console.Reboot(null, null, null, XboxRebootFlags.Cold);
            MessageBox.Show("Console rebooted\nPlease Reconnect");
        }

        private void screenshot_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Screenshots")) { Directory.CreateDirectory("Screenshots"); }
            Console.ScreenShot("Screenshots\\Temp0.bmp");
            Image capture = Image.FromFile("Screenshots\\Temp0.bmp");
            capture.Save("Screenshots\\Screenshot_" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd_hh-mm-ss") + ".png", ImageFormat.Png);
            Clipboard.SetImage(capture);
            capture.Dispose();
            File.Delete("Screenshots\\Temp0.bmp");
            Console.XNotify("Screenshot Captured");
        }

        private void shutdown_Click(object sender, EventArgs e)
        {
            this.Console.ShutDownConsole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B57, 03);
            Console.WriteByte(0x82FF9CFF, 03);
            Console.WriteByte(0x82FF9DD3, 03);
            Console.WriteByte(0x82FF9EA7, 03);
            Console.WriteByte(0x82FF9F7B, 03);
            Console.WriteByte(Console.ReadUInt32(0x82FF9B5C), Properties.Resources.dark_matter_glow); //cyborg glow
            Console.WriteByte(Console.ReadUInt32(0x82FF9D04), Properties.Resources.dark_matter_reveal); //cyborg reveal
            Console.WriteByte(Console.ReadUInt32(0x82FF9DD8), Properties.Resources.dark_matter_rbg); //cyborg rgb
            Console.WriteByte(Console.ReadUInt32(0x82FF9EAC), Properties.Resources.dark_matter_col); //cyborg col
            Console.WriteByte(Console.ReadUInt32(0x82FF9F80), Properties.Resources.dark_matter_heat); //cyborg heat
            this.Console.XNotify("Dark Matter Camo Loaded!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B57, 03);
            Console.WriteByte(0x82FF9CFF, 03);
            Console.WriteByte(0x82FF9DD3, 03);
            Console.WriteByte(0x82FF9EA7, 03);
            Console.WriteByte(0x82FF9F7B, 03);
            Console.WriteByte(Console.ReadUInt32(0x82FF9B5C), Properties.Resources.rick_and_morty_glow); //cyborg glow
            Console.WriteByte(Console.ReadUInt32(0x82FF9D04), Properties.Resources.rick_and_morty_reveal); //cyborg reveal
            Console.WriteByte(Console.ReadUInt32(0x82FF9DD8), Properties.Resources.rick_and_morty_rgb); //cyborg rgb
            Console.WriteByte(Console.ReadUInt32(0x82FF9EAC), Properties.Resources.rick_and_morty_col); //cyborg col
            Console.WriteByte(Console.ReadUInt32(0x82FF9F80), Properties.Resources.rick_and_morty_heat); //cyborg heat
            this.Console.XNotify("Rick And Morty Camo Loaded!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B57, 03);
            Console.WriteByte(0x82FF9CFF, 03);
            Console.WriteByte(0x82FF9DD3, 03);
            Console.WriteByte(0x82FF9EA7, 03);
            Console.WriteByte(0x82FF9F7B, 03);
            Console.WriteByte(Console.ReadUInt32(0x82FF9B5C), Properties.Resources.bubblegum_glow); //cyborg glow
            Console.WriteByte(Console.ReadUInt32(0x82FF9D04), Properties.Resources.bubblegum_reveal); //cyborg reveal
            Console.WriteByte(Console.ReadUInt32(0x82FF9DD8), Properties.Resources.bubblegum_rgb); //cyborg rgb
            Console.WriteByte(Console.ReadUInt32(0x82FF9EAC), Properties.Resources.bubblegum_col); //cyborg col
            Console.WriteByte(Console.ReadUInt32(0x82FF9F80), Properties.Resources.bubblegum_heat); //cyborg heat
            this.Console.XNotify("Bubblegum Camo Loaded!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B57, 03);
            Console.WriteByte(0x82FF9CFF, 03);
            Console.WriteByte(0x82FF9DD3, 03);
            Console.WriteByte(0x82FF9EA7, 03);
            Console.WriteByte(0x82FF9F7B, 03);
            Console.WriteByte(Console.ReadUInt32(0x82FF9B5C), Properties.Resources.let_it_rip_glow); //cyborg glow
            Console.WriteByte(Console.ReadUInt32(0x82FF9D04), Properties.Resources.let_it_rip_reveal); //cyborg reveal
            Console.WriteByte(Console.ReadUInt32(0x82FF9DD8), Properties.Resources.let_it_rip_rgb); //cyborg rgb
            Console.WriteByte(Console.ReadUInt32(0x82FF9EAC), Properties.Resources.let_it_rip_col); //cyborg col
            Console.WriteByte(Console.ReadUInt32(0x82FF9F80), Properties.Resources.let_it_rip_heat); //cyborg heat
            this.Console.XNotify("Let It Rip Camo Loaded!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B57, 03);
            Console.WriteByte(0x82FF9CFF, 03);
            Console.WriteByte(0x82FF9DD3, 03);
            Console.WriteByte(0x82FF9EA7, 03);
            Console.WriteByte(0x82FF9F7B, 03);
            Console.WriteByte(Console.ReadUInt32(0x82FF9B5C), Properties.Resources.manipulation_glow); //cyborg glow
            Console.WriteByte(Console.ReadUInt32(0x82FF9D04), Properties.Resources.manipulation_reveal); //cyborg reveal
            Console.WriteByte(Console.ReadUInt32(0x82FF9DD8), Properties.Resources.manipulation_rgb); //cyborg rgb
            Console.WriteByte(Console.ReadUInt32(0x82FF9EAC), Properties.Resources.manipulation_col); //cyborg col
            Console.WriteByte(Console.ReadUInt32(0x82FF9F80), Properties.Resources.manipulation_heat); //cyborg heat
            this.Console.XNotify("Manipulation Camo Loaded!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ALL OF THESE CAMOS REPLACE CYBORG\nMake sure to not move around too much in game while the camo is injecting. Once done enjoy!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.class1.Checked)
            {
                Console.WriteByte(0x84353CFE, 0x90);
                Console.WriteByte(0x84353CFF, 0x05);//TOP
                Console.WriteByte(0x84353D0C, 0xC0);
                Console.WriteByte(0x84353D0D, 0x02);//BOTTOM
            }
            if (this.class2.Checked)
            {
                Console.WriteByte(0x84353D33, 0x58);
                //Console.WriteByte(0x84353CFF, 0x05);//TOP
                Console.WriteByte(0x84353D41, 0x2C);
                //Console.WriteByte(0x84353D5E, 0x16);//BOTTOM
            }
            if (this.class3.Checked)
            {
                Console.WriteByte(0x84353D67, 0x80);
                Console.WriteByte(0x84353D68, 0x05);//TOP
                Console.WriteByte(0x84353D75, 0xC0);
                Console.WriteByte(0x84353D76, 0x02);//BOTTOM
            }
            if (this.class4.Checked)
            {
                Console.WriteByte(0x84353D9C, 0x58);
                //Console.WriteByte(0x84353CFF, 0x05);//TOP
                Console.WriteByte(0x84353DAA, 0x2C);
                //Console.WriteByte(0x84353D5E, 0x16);//BOTTOM
            }
            if (this.class5.Checked)
            {
                Console.WriteByte(0x84353DD0, 0x80);
                Console.WriteByte(0x84353DD1, 0x05);//TOP
                Console.WriteByte(0x84353DDE, 0xC0);
                Console.WriteByte(0x84353DDF, 0x02);//BOTTOM
            }
            this.Console.XNotify("Double DSR Class Set");
            MessageBox.Show("Make sure to set overkill as a perk and a attachment on the second gun to stick!");
            Console.CallVoid(0x824015E0, 0, "updategamerprofile;uploadstats;disconnect");
        }

        private void LoadSpoofer()
        {
            this.Console.SendFile("BO2RemoteRecovery.xex", "HDD:\\BO2RemoteRecovery.xex");
            this.Console.CallVoid("xboxkrnl.exe", 409, new object[]
            {
                "HDD:\\BO2RemoteRecovery.xex",
                8,
                0,
                0
            });
        }

        private void Spoof_Click(object sender, EventArgs e)
        {
            this.LoadSpoofer();
            this.Console.WriteByte(2175795328U, 1);
            this.Console.WriteString(2175795329U, this.textEdit26.Text);
            this.Console.SetMemory(2175795345U, Form1.hexString(this.textEdit27.Text));
            this.Console.XNotify("GT: " + this.textEdit26.Text + "\nXUID: " + this.textEdit27.Text);
            this.Console.CallVoid(0x824015E0, 0, "cancelDemonWareConnect");
            this.Console.CallVoid(0x824015E0, 0, "cancelDemonWareConnect");
            this.Console.WriteString(0x841E1B30, this.textEdit26.Text);
            this.Console.SetMemory(0x841E1B50, Form1.hexString(this.textEdit27.Text));
            this.Console.WriteString(0x841E1B58, this.textEdit27.Text);
            string mediaDirectory2 = Path.Combine(new string[]
            {
                this.bo2Drive.Text + Properties.Settings.Default.bo2Path
            });
            string text2 = Path.Combine(new string[]
            {
                this.bo2Drive.Text + Properties.Settings.Default.bo2Path + "\\default_mp.xex"
            });
            int num = text2.LastIndexOf("\\\\", StringComparison.Ordinal);
            text2 = ((num > -1) ? text2.Substring(0, num) : text2);
            Console.Reboot(text2 + "\\default_mp.xex", mediaDirectory2, null, XboxRebootFlags.Title);
        }

        public static byte[] hexString(string hex)
        {
            return (from x in Enumerable.Range(0, hex.Length)
                    where x % 2 == 0
                    select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
        }

        string XamUserGetGamertag()
        {
            return Encoding.ASCII.GetString(Console.GetMemory(0x81AA28FC, 30)).Replace("\0", string.Empty);
        }

        private void UnSpoof()
        {
            this.Console.WriteByte(2175795328U, 1);
            this.Console.WriteString(2175795329U, this.XamUserGetGamertag());
            this.Console.SetMemory(2175795345U, Form1.hexString($"000{Console.ReadUInt64(0x81AA291C).ToString("X")}"));
            this.Console.XNotify("GT: " + this.XamUserGetGamertag() + "\nXUID: " + $"000{Console.ReadUInt64(0x81AA291C).ToString("X")}");
            string mediaDirectory2 = Path.Combine(new string[]
            {
                this.bo2Drive.Text + Properties.Settings.Default.bo2Path
            });
            string text2 = Path.Combine(new string[]
            {
                this.bo2Drive.Text + Properties.Settings.Default.bo2Path + "\\default_mp.xex"
            });
            int num = text2.LastIndexOf("\\\\", StringComparison.Ordinal);
            text2 = ((num > -1) ? text2.Substring(0, num) : text2);
            Console.Reboot(text2 + "\\default_mp.xex", mediaDirectory2, null, XboxRebootFlags.Title);
        }

        private void Unspoof_Click(object sender, EventArgs e)
        {
            this.UnSpoof();
        }

        private void inject_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                new Thread((ThreadStart)(() =>
                {
                    this.serenity.GetFilesOfTheDay(Serenity.FileType.V2);
                    if (MessageBox.Show("PLEASE READ!!!!! \n\nIf you are infecting, please click reset parse tree after injecting the mod and continue infecting like normal. \nThis is a better method of infecting and causes less issues\n\nCLICK YES TO CONTINUE", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    this.UnloadPlugin("MatrixMods.xex");
                    Thread.CurrentThread.IsBackground = true;
                    JRPC.SetMemory(this.Console, 1076887552U, System.IO.File.ReadAllBytes(Path.GetTempPath() + "/Revamped.V2"));
                    JRPC.XNotify(this.Console, "Project Revamped V2\nLoaded!");
                    JRPC.WriteUInt32(this.Console, 2199829632U, 1076887552U);
                    //this.SerenityV6InfectModLabel.Text = "Loaded";
                    this.infect.Enabled = true;
                })).Start();
                this.infect.Enabled = true;
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void LoadPlugin(string plugin) => JRPC.CallVoid(this.Console, "xboxkrnl.exe", 409, new object[4]
        {
               (object) plugin,
               (object) 8,
               (object) 0,
               (object) 0
        });

        private void UnloadPlugin(string plugin)
        {
            uint num = JRPC.Call<uint>(this.Console, "xam.xex", 1102, new object[1]
            {
              (object) plugin
            });
            if (num <= 0U)
                return;
            JRPC.WriteInt16(this.Console, num + 64U, (short)1);
            JRPC.CallVoid(this.Console, "xboxkrnl.exe", 417, new object[1]
            {
              (object) num
            });
        }

        void Connection()
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                if (Console.Connect(out Console))
                {
                    if (XamUserGetGamertag() == "")
                    {
                        this.Console.XNotify("Connected\nWelcome");
                    }
                    else
                    {
                        this.Console.XNotify($"Connected\nWelcome {XamUserGetGamertag()}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }


        private void infect_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.serenity.GetFilesOfTheDay(Serenity.FileType.XV2);
                if (MessageBox.Show("For this to work properly, everyone has to be in spectator/codcaster besides you.\n\nThis will take about 5 mins to complete, are you ready??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.XV2", "HDD:\\Cache\\XRV2.xex");
                this.LoadPlugin("HDD:\\Cache\\XRV2.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            Console.WriteByte(0x82FF9B3F, 0x90);
            //Thread.Sleep(1000);
            //Console.WriteByte(0x82FF9C13, 54); //REVERTING
            Console.WriteByte(0x82FF9F63, 0x50);
            Console.WriteByte(0x82FF9DC5, 0x07);
            Console.WriteByte(0x82FF9DBA, 0x19);
            Console.WriteByte(0x82FF9DBB, 0x18);
            Console.WriteByte(0x82FF9DC5, 0x07);
            Console.WriteByte(0x82FF9CE5, 0x09);
            Console.WriteByte(0x82FF9CE6, 0x20);
            Console.WriteByte(0x82FF9F6D, 0x07);
            Console.WriteByte(0x82FF9F61, 0x04);
            Console.WriteByte(0x82FF9EA6, 0x07);
            Console.WriteByte(0x82FF9E8D, 0x04);
            Console.WriteByte(0x82FF9E8F, 0x89);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks to SupItsTom for the API :)");
        }

        private void injectvanilla_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                new Thread((ThreadStart)(() =>
                {
                    this.serenity.GetFilesOfTheDay(Serenity.FileType.S5);
                    if (MessageBox.Show("PLEASE READ!!!!! \n\nIf you are infecting, please click reset parse tree after injecting the mod and continue infecting like normal. \nThis is a better method of infecting and causes less issues\n\nCLICK YES TO CONTINUE", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    this.UnloadPlugin("MatrixMods.xex");
                    Thread.CurrentThread.IsBackground = true;
                    JRPC.SetMemory(this.Console, 1076887552U, System.IO.File.ReadAllBytes(Path.GetTempPath() + "/Revamped.Vanilla"));
                    JRPC.XNotify(this.Console, "Project Revamped Vanilla\nLoaded!");
                    JRPC.WriteUInt32(this.Console, 2199829632U, 1076887552U);
                    //this.SerenityV6InfectModLabel.Text = "Loaded";
                    this.infectv.Enabled = true;
                })).Start();
                this.infectv.Enabled = true;
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void infectv_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.serenity.GetFilesOfTheDay(Serenity.FileType.S4);
                if (MessageBox.Show("For this to work properly, everyone has to be in spectator/codcaster besides you.\n\nThis will take about 5 mins to complete, are you ready??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.VanillaX", "HDD:\\Cache\\RV.xex");
                this.LoadPlugin("HDD:\\Cache\\RV.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void resetparse()
        {
            Console.WriteByte(0x831EBC80, 0xA5);
            Console.WriteByte(0x831EBC81, 0xBA);
            Console.WriteByte(0x831EBC82, 0xF4);
            Console.WriteByte(0x831EBC83, 0xA0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.resetparse();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.resetparse();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.resetparse();
        }

        private void injectsnd_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                new Thread((ThreadStart)(() =>
                {
                    this.serenity.GetFilesOfTheDay(Serenity.FileType.PTB);
                    if (MessageBox.Show("PLEASE READ!!!!! \n\nIf you are infecting, please click reset parse tree after injecting the mod and continue infecting like normal. \nThis is a better method of infecting and causes less issues\n\nCLICK YES TO CONTINUE", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    this.UnloadPlugin("MatrixMods.xex");
                    Thread.CurrentThread.IsBackground = true;
                    JRPC.SetMemory(this.Console, 1076887552U, System.IO.File.ReadAllBytes(Path.GetTempPath() + "/Revamped.SND"));
                    JRPC.XNotify(this.Console, "Project Revamped SND\nLoaded!");
                    JRPC.WriteUInt32(this.Console, 2199829632U, 1076887552U);
                    //this.SerenityV6InfectModLabel.Text = "Loaded";
                    this.infectsnd.Enabled = true;
                })).Start();
                this.infectsnd.Enabled = true;
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void infectsnd_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.serenity.GetFilesOfTheDay(Serenity.FileType.XPTB);
                if (MessageBox.Show("For this to work properly, everyone has to be in spectator/codcaster besides you.\n\nThis will take about 5 mins to complete, are you ready??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.SNDX", "HDD:\\Cache\\RSND.xex");
                this.LoadPlugin("HDD:\\Cache\\RSND.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void Cbuf_AddText(int localClientNum, string buffer) => Console.CallVoid(0x824015E0, localClientNum, buffer);

        private void button12_Click(object sender, EventArgs e)
        {
            this.Cbuf_AddText(0, "disconnect");
        }

        private void injectgg_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                new Thread((ThreadStart)(() =>
                {
                    this.serenity.GetFilesOfTheDay(Serenity.FileType.RGG);
                    if (MessageBox.Show("PLEASE READ!!!!! \n\nIf you are infecting, please click reset parse tree after injecting the mod and continue infecting like normal. \nThis is a better method of infecting and causes less issues\n\nCLICK YES TO CONTINUE", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    this.UnloadPlugin("MatrixMods.xex");
                    Thread.CurrentThread.IsBackground = true;
                    JRPC.SetMemory(this.Console, 1076887552U, System.IO.File.ReadAllBytes(Path.GetTempPath() + "/Revamped.GG"));
                    JRPC.XNotify(this.Console, "Project Revamped GunGame\nLoaded!");
                    JRPC.WriteUInt32(this.Console, 2199829632U, 1076887552U);
                    //this.SerenityV6InfectModLabel.Text = "Loaded";
                    this.infectgg.Enabled = true;
                })).Start();
                this.infectgg.Enabled = true;
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void infectgg_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.serenity.GetFilesOfTheDay(Serenity.FileType.RGGX);
                if (MessageBox.Show("For this to work properly, everyone has to be in spectator/codcaster besides you.\n\nThis will take about 5 mins to complete, are you ready??", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.GGX", "HDD:\\Cache\\RGG.xex");
                this.LoadPlugin("HDD:\\Cache\\RGG.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.resetparse();
        }

        private void knife_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.Console.WriteByte(0x82E8F110, new byte[] { 0x02, 0x01, 0x00, 0x88, 0x00, 0xE4, 0x01, 0x35, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x22, 0x00, 0x00, 0x20, 0x06, 0x00, 0x40, 0x02, 0x48, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x03, 0xF6, 0x00, 0x00, 0x00, 0x00, 0x41, 0xF0, 0x00, 0x00, 0x3F, 0x5B, 0x6D, 0xB7, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0x70, 0x53, 0xCE, 0xAA, 0x70, 0x54, 0x70, 0xAA, 0x70, 0x56, 0x72, 0xAA, 0x70, 0x57, 0x84, 0xAA, 0x70, 0x5B, 0x14, 0xAA, 0x70, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0x70, 0x41, 0x08, 0x00, 0x00, 0x00, 0x00, 0xAA, 0x70, 0x53, 0xAE, 0x02, 0x01, 0x00, 0x88, 0x00, 0xE4, 0x01, 0x35, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x22, 0x00, 0x00, 0x20, 0x06, 0x00, 0x40, 0x02, 0x48, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x03, 0xF6, 0x00, 0x00, 0x00, 0x00, 0x41, 0xF0, 0x00, 0x00, 0x3F, 0x5B, 0x6D, 0xB7, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                this.serenity.GetFilesOfTheDay(Serenity.FileType.KNIFE);
                if (MessageBox.Show("Knife Lunge Infect, This will only show for people who are infected with it.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.knife", "HDD:\\Cache\\knife.xex");
                this.LoadPlugin("HDD:\\Cache\\knife.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }

        private void dsrxbal_Click(object sender, EventArgs e)
        {
            if (new WebClient().DownloadString("https://rentry.co/himtd/raw") == "true")
            {
                this.Console.WriteByte(0x82E76D80, new byte[] { 0x06, 0x88, 0x00, 0x50, 0x00, 0xE7, 0x02, 0x40, 0x00, 0x00, 0x00, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x42, 0x00, 0x00, 0x01, 0x0A, 0x01, 0x37, 0x08, 0x4A, 0x05, 0x01, 0x00, 0x00, 0x00, 0x00, 0x10, 0x7A, 0x00, 0x00, 0x00, 0x00, 0x42, 0xF0, 0x00, 0x00, 0x3E, 0xEC, 0x4E, 0xC5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA9, 0xD1, 0x55, 0x66, 0xA9, 0xD1, 0x56, 0x24, 0xA9, 0xD1, 0x5C, 0xAC, 0xA9, 0xD1, 0x5D, 0x4C, 0xA9, 0xD1, 0x60, 0xE8, 0xA9, 0xD1, 0x81, 0xDC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA9, 0xD1, 0x55, 0xFC, 0x00, 0x00, 0x00, 0x00, 0xA9, 0xD1, 0x84, 0x1C, 0x04, 0x52, 0x00, 0x4E, 0x00, 0xE7, 0x01, 0xA7, 0x00, 0x00, 0x00, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x40, 0x00, 0x00, 0x01, 0x09, 0x02, 0x37, 0x08, 0x4A, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x71, 0x00, 0x00, 0x00, 0x00, 0x41, 0xF0, 0x00, 0x00, 0x3F, 0x32, 0x9A, 0xCA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                this.serenity.GetFilesOfTheDay(Serenity.FileType.DSR);
                if (MessageBox.Show("DSR Pullout Animation To Ballista Infect, This will only show for people who are infected with it.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.UnloadPlugin("knife.xex");
                this.UnloadPlugin("dsr.xex");
                this.UnloadPlugin("RGG.xex");
                this.UnloadPlugin("RSND.xex");
                this.UnloadPlugin("RV.xex");
                this.UnloadPlugin("XRV2.xex");
                this.Console.SendFile(Path.GetTempPath() + "/Revamped.dsr", "HDD:\\Cache\\dsr.xex");
                this.LoadPlugin("HDD:\\Cache\\dsr.xex");
                JRPC.XNotify(this.Console, "Infection started!");
            }
            else
            {
                MessageBox.Show("Project Revamped Services Are Temporarily Disabled.");
            }
        }
    }
}
