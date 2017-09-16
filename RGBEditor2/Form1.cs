using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;




namespace WindowsFormsApp1
{
    public partial class RGBEditor : Form
    {



        public RGBEditor()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("RGBIndex.txt") == false)
            {
                MessageBox.Show("Please place RGBIndex.txt in same folder as the RGB Editor!", "RGBIndex.txt not found!");
            }
            else
            {

            
            OpenFileDialog dialog = new OpenFileDialog();
            openFileDialog1.Filter = "N64 ROM Files (Z64,ROM,N64)|*.Z64;*.N64;*.ROM";
            openFileDialog1.FileName = "SM64 ROM";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)



                textBox1.Text = openFileDialog1.FileName;
            if (textBox1.TextLength > 0)
            {
                comboBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button2.Enabled = true;
                    this.comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(File.ReadAllLines("RGBIndex.txt"));
                for (int i = 1; i < comboBox1.Items.Count; i += 1)
                {
                    comboBox1.Items.RemoveAt(i);
                }
                for (int i = 1; i < comboBox1.Items.Count; i += 1)
                {
                    comboBox1.Items.RemoveAt(i);
                }
                for (int i = 1; i < comboBox1.Items.Count; i += 1)
                {
                    comboBox1.Items.RemoveAt(i);
                }



                comboBox1.SelectedIndex = 0;
                }

            }

                


            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox2.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox3.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox4.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (textBox1.TextLength > 0)
            {
                byte[] rome = File.ReadAllBytes(openFileDialog1.FileName);
                string line1 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 1).Take(1).First();
                string line2 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 3).Take(1).First();
                string line3 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 5).Take(1).First();
                if (line3 == "n")
                {
                    textBox8.Enabled = false;
                }
                else
                {
                    textBox8.Enabled = true;
                    UInt32 addrALPHA = UInt32.Parse(line3, styleAllowHex);
                    string aa = rome[addrALPHA].ToString("x");
                    //If one integer, place 0 in front
                    if (aa.Length < 2)
                    {
                        aa = "0" + (aa);
                    }
                    //write red RR into textbox
                    textBox8.Text = (aa);
                }
                if (line2 == "n")
                {
                        textBox5.Enabled = false;
                        textBox6.Enabled = false;
                        textBox7.Enabled = false;
                }
                else
                {
                        textBox5.Enabled = true;
                        textBox6.Enabled = true;
                        textBox7.Enabled = true;
                        UInt32 addrRGB2 = UInt32.Parse(line2, styleAllowHex);
                        string rr2 = rome[addrRGB2].ToString("x");
                        //If one integer, place 0 in front
                        if (rr2.Length < 2)
                        {
                            rr2 = "0" + (rr2);
                        }
                        //write red RR into textbox
                        textBox7.Text = (rr2);


                        //repeat for green GG
                        string gg2 = rome[addrRGB2 + 0x01].ToString("x");


                        if (gg2.Length < 2)
                        {
                            gg2 = "0" + (gg2);
                        }
                        textBox6.Text = (gg2);
                        //repeat for blue BB
                        string bb2 = rome[addrRGB2 + 0x02].ToString("x");


                        if (bb2.Length < 2)
                        {
                            bb2 = "0" + (bb2);
                        }
                        textBox5.Text = (bb2);
                    
                }
                //Picking up an address based off Combobox Selection
                
                UInt32 addrRGB = UInt32.Parse(line1, styleAllowHex);
                
                //Read bytes from selected combobox address
                string rr = rome[addrRGB].ToString("x");

                //If one integer, place 0 in front
                if (rr.Length < 2)
                {
                    rr = "0" + (rr);
                }
                    //write red RR into textbox
                    textBox2.Text = (rr);

                //repeat for green GG
                string gg = rome[addrRGB + 0x01].ToString("x");


                if (gg.Length < 2)
                {
                    gg = "0" + (gg);
                }
                    textBox3.Text = (gg);
                //repeat for blue BB
                string bb = rome[addrRGB + 0x02].ToString("x");


                if (bb.Length < 2)
                {
                    bb = "0" + (bb);
                }
                    textBox4.Text = (bb);


                
            }



        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        NumberStyles styleAllowHex = NumberStyles.AllowHexSpecifier;


        private void button2_Click(object sender, EventArgs e)
        {
            //This is where it actually writes bytes to the ROM.
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(openFileDialog1.FileName));
            {
                //Picking up an address based off Combobox Selection
                string line1 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 1).Take(1).First();
                //Picking up a y/n for double RGB
                string line2 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 2).Take(1).First();
                UInt32 addrRGB = UInt32.Parse(line1, styleAllowHex);
                //This is where the code reads from the textboxes, and uses the text as a hexadecimal format.
                //Begin Address
                bw.BaseStream.Position = addrRGB;
                //RR
                bw.Write(byte.Parse(textBox2.Text, styleAllowHex));
                bw.BaseStream.Position = addrRGB + 0x01;
                //GG
                bw.Write(byte.Parse(textBox3.Text, styleAllowHex));
                bw.BaseStream.Position = addrRGB + 0x02;
                //BB
                bw.Write(byte.Parse(textBox4.Text, styleAllowHex));
                //If alpha texbox is enabled..
                if (textBox8.Enabled == true)
                {
                    //begin the patch routine for alpha
                    string line5 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 5).Take(1).First();
                    UInt32 addrALPHA = UInt32.Parse(line5, styleAllowHex);
                    bw.BaseStream.Position = addrALPHA;
                    //AA
                    bw.Write(byte.Parse(textBox8.Text, styleAllowHex));

                }
                // if they chose y for double rgb
                if (line2 == "y") 
                {
                    //Begin Address
                    bw.BaseStream.Position = addrRGB + 0x04;
                    //RR 2
                    bw.Write(byte.Parse(textBox2.Text, styleAllowHex));
                    bw.BaseStream.Position = addrRGB + 0x05;
                    //GG 2
                    bw.Write(byte.Parse(textBox3.Text, styleAllowHex));
                    bw.BaseStream.Position = addrRGB + 0x06;
                    //BB 2
                    bw.Write(byte.Parse(textBox4.Text, styleAllowHex));
                }

                if (textBox5.Enabled == true)
                {

                
                //Picking up an address for SHADING based off Combobox Selection
                string line3 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 3).Take(1).First();
                //Picking up a y/n for double RGB
                string line4 = File.ReadLines("RGBIndex.txt").Skip((comboBox1.SelectedIndex * 8) + 4).Take(1).First();
                UInt32 addrRGB2 = UInt32.Parse(line3, styleAllowHex);
                //This is where the code reads from the textboxes, and uses the text as a hexadecimal format.
                //Begin Address
                bw.BaseStream.Position = addrRGB2;
                //Shade RR
                bw.Write(byte.Parse(textBox7.Text, styleAllowHex));
                bw.BaseStream.Position = addrRGB2 + 0x01;
                //Shade GG
                bw.Write(byte.Parse(textBox6.Text, styleAllowHex));
                bw.BaseStream.Position = addrRGB2 + 0x02;
                //Shade BB
                bw.Write(byte.Parse(textBox5.Text, styleAllowHex));

                // if they chose y for double rgb
                if (line4 == "y")
                {
                    //Begin Address
                    bw.BaseStream.Position = addrRGB2 + 0x04;
                    //Shade RR 2
                    bw.Write(byte.Parse(textBox7.Text, styleAllowHex));
                    bw.BaseStream.Position = addrRGB2 + 0x05;
                    //Shade GG 2
                    bw.Write(byte.Parse(textBox6.Text, styleAllowHex));
                    bw.BaseStream.Position = addrRGB2 + 0x06;
                    //Shade BB 2
                    bw.Write(byte.Parse(textBox5.Text, styleAllowHex));
                    }
                }

                bw.Close();

                //This is for the message dialog saying that it patched
                MessageBox.Show("Custom RGB has been Patched!", "Patched!");
                
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox7.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox6.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only 1 byte per textbox. (2 integers.)
            textBox5.MaxLength = 2;
            char c = e.KeyChar;
            //Here is a formula to only allow hexadecimal characters/integers
            if (c != '\b' && !((c <= 0x66 && c >= 0x61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39) || (c == 0x2c)))
            {
                e.Handled = true;

            }
        }
    }
}
