using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vulcan.NET;
using System.Windows.Forms;

namespace Vulcan_Project
{
    public partial class Form1 : Form
    {
        #region variable
        IVulcanKeyboard kb = null;

        #endregion
        int speed = 0;
        int Red = 254;
        int Blue = 0;
        int Green = 0;
        bool stop = false;
        bool colorRandom = false;
        bool EvRand = false;
        Random r = new Random();


        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            stop = true;
            

            
            if (kb != null)
            {
                Thread.Sleep(203);
                kb.SetColor(255, 0, 0);
                kb.Update();
            }
            



            this.Close();
            
            
        }

        private void buttonAddKB_Click(object sender, EventArgs e)
        {
            var Keyboards = VulcanFinder.FindKeyboards();

            foreach (var Key in Keyboards)
            {
                kb = Key;

            }
            if (kb != null)
            {
                groupBox1.Visible = true;
                groupBox1.Enabled = true;
                labelKB.Visible = false;
            }

        }

        private void comboBoxLEDPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelFullColor.Visible = false;
            panelFullColor.Enabled = false;
            panelRainbowWaves.Visible = false;
            panelRainbowWaves.Enabled = false;
            panelRandom.Enabled = false;
            panelRandom.Visible = false;
            PannelRandomKeys.Visible = false;
            PannelRandomKeys.Enabled = false;

            if (comboBoxLEDPattern.SelectedIndex != 2)
            {
                if (backgroundWorkerRainbowColor.IsBusy == true)
                {
                    stop = true;
                    Thread.Sleep(203);
                    backgroundWorkerRainbowColor.CancelAsync();

                    stop = false;
                }
            }

            if (comboBoxLEDPattern.SelectedIndex != 5)
            {
                if (backgroundWorkerRainbowWave.IsBusy == true)
                {
                    stop = true;
                    Thread.Sleep(203);
                    backgroundWorkerRainbowWave.CancelAsync();

                    stop = false;
                }
            }

            if (comboBoxLEDPattern.SelectedIndex != 4)
            {
                if (backgroundWorkerRandomcolor.IsBusy == true)
                {
                    stop = true;
                    Thread.Sleep(203);
                    backgroundWorkerRandomcolor.CancelAsync();
                    stop = false;
                }
            }

            if (comboBoxLEDPattern.SelectedIndex != 3)
            {
                if (backgroundWorkerRandomKey.IsBusy == true)
                {
                    stop = true;
                    Thread.Sleep(203);
                    backgroundWorkerRandomKey.CancelAsync();
                    stop = false;
                }
            }

            switch (comboBoxLEDPattern.SelectedIndex)
            {

                case 0:
                    panelFullColor.Visible = true;
                    panelFullColor.Enabled = true;
                    groupBox1.Update();

                    break;

                case 2:
                    panelRainbowWaves.Visible = true;
                    panelRainbowWaves.Enabled = true;

                    Thread.Sleep(20);
                    if (backgroundWorkerRainbowColor.IsBusy == false)
                    {
                        backgroundWorkerRainbowColor.RunWorkerAsync();
                    }
                    break;

                case 5:

                    break;

                case 3:
                    panelRandom.Enabled = true;
                    panelRandom.Visible = true;
                    Thread.Sleep(20);
                    if (backgroundWorkerRandomKey.IsBusy == false)
                    {
                        backgroundWorkerRandomKey.RunWorkerAsync();
                    }

                    break;

                case 4:
                    PannelRandomKeys.Visible = true;
                    PannelRandomKeys.Enabled = true;
                    Thread.Sleep(20);
                    if (backgroundWorkerRandomcolor.IsBusy == false)
                    {
                        backgroundWorkerRandomcolor.RunWorkerAsync();
                    }
                    break;

                case 1:

                    Thread.Sleep(20);
                    if (backgroundWorkerRainbowWave.IsBusy == false)
                    {
                        backgroundWorkerRainbowWave.RunWorkerAsync();
                    }

                    break;
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Red = trackBarRed.Value; Green = trackBarGreen.Value; Blue = trackBarBlue.Value;
            kb.SetColor((byte)Red, (byte)Green, (byte)Blue);
            kb.Update();
        }

        private void trackBarRed_ValueChanged(object sender, EventArgs e)
        {
            labelColor.ForeColor = Color.FromArgb(trackBarRed.Value, trackBarGreen.Value, trackBarBlue.Value);
            labelColor.Update();
        }

        private void trackBarGreen_ValueChanged(object sender, EventArgs e)
        {
            labelColor.ForeColor = Color.FromArgb(trackBarRed.Value, trackBarGreen.Value, trackBarBlue.Value);
            labelColor.Update();
        }

        private void trackBarBlue_ValueChanged(object sender, EventArgs e)
        {
            labelColor.ForeColor = Color.FromArgb(trackBarRed.Value, trackBarGreen.Value, trackBarBlue.Value);
            labelColor.Update();
        }

        private void trackBarSpeed_ValueChanged(object sender, EventArgs e)
        {
            speed = trackBarSpeed.Value;

        }

        private void backgroundWorkerRainbowWaves_DoWork(object sender, DoWorkEventArgs e)
        {
            if(Red == 0 && Blue == 0 && Green == 0) { Red = 254; }
            if (Red < Green && Red < Blue) { Red = 0; }
            if (Blue < Red && Blue < Green) { Blue = 0; }
            if (Green < Red && Green < Blue) { Green = 0; }



            while (stop == false)
            {
                if (Red > 0 && Green < 254 && Blue == 0)
                {
                    Red -= 1;
                    Green += 1;
                }

                if (Green > 0 && Blue < 254 && Red == 0)
                {
                    Green -= 1;
                    Blue += 1;
                }

                if (Blue > 0 && Red < 254 && Green == 0)
                {
                    Blue -= 1;
                    Red += 1;
                }

                kb.SetColor((byte)Red, (byte)Green, (byte)Blue);
                kb.Update();
                Thread.Sleep(201 - speed);

            }
        }

        private void backgroundWorkerRandomKey_DoWork(object sender, DoWorkEventArgs e)
        {
            while (stop == false)
            {
                if (colorRandom == true && EvRand == true)
                {
                    Red = r.Next(0, 254);
                    Blue = r.Next(0, 254);
                    Green = r.Next(0, 254);

                }


                var k = kb.Keys;
                foreach (var l in k)
                {
                    if (stop == true) { break; }
                    if (colorRandom == true && EvRand == false)
                    {
                        Red = r.Next(0, 254);
                        Blue = r.Next(0, 254);
                        Green = r.Next(0, 254);
                    }

                    kb.SetKeyColor(l, (byte)Red, (byte)Green, (byte)Blue);
                    kb.Update();
                    Thread.Sleep(201 - speed);
                }
                foreach (var l in k)
                {
                    if (stop == true) { break; }
                    kb.SetKeyColor(l, 0, 0, 0);
                    kb.Update();
                    Thread.Sleep(201 - speed);
                }


            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            speed = trackBar1.Value;
        }

        private void checkBoxRand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRand.Checked == true) {
                colorRandom = true;
                checkBoxOnnlyOne.Enabled = true;
                panelColor.Enabled = false;
                panelColor.Visible = false;
            }
            if (checkBoxRand.Checked == false) {
                colorRandom = false;
                checkBoxOnnlyOne.Checked = false;
                checkBoxOnnlyOne.Enabled = false;
                panelColor.Enabled = true;
                panelColor.Visible = true;
            }
        }

        private void checkBoxOnnlyOne_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnnlyOne.Checked == true) { EvRand = true; }
            if (checkBoxOnnlyOne.Checked == false) { EvRand = false; }
        }

        private void trackBarRValue_ValueChanged(object sender, EventArgs e)
        {
            Red = trackBarRValue.Value;
            labelC.ForeColor = Color.FromArgb(trackBarRValue.Value, trackBarGvalue.Value, trackBarBvalue.Value);
        }

        private void trackBarGvalue_ValueChanged(object sender, EventArgs e)
        {
            Green = trackBarGvalue.Value;
            labelC.ForeColor = Color.FromArgb(trackBarRValue.Value, trackBarGvalue.Value, trackBarBvalue.Value);
        }

        private void trackBarBvalue_ValueChanged(object sender, EventArgs e)
        {
            Blue = trackBarBvalue.Value;
            labelC.ForeColor = Color.FromArgb(trackBarRValue.Value, trackBarGvalue.Value, trackBarBvalue.Value);
        }

        private void trackBarSpeedKey_ValueChanged(object sender, EventArgs e)
        {
            speed = trackBarSpeedKey.Value;
        }

        private void backgroundWorkerRandomcolor_DoWork(object sender, DoWorkEventArgs e)
        {
            var AK = kb.Keys;
            int it = 0;
            bool LEDON = true;
            while (stop != true)
            {
                while (stop != true && LEDON == true)
                {

                    int keyselected = r.Next(0, AK.Count());

                    if (checkBoxR.Checked == true)
                    {
                        this.panel2.Invoke((MethodInvoker)delegate
                        {
                            this.panel2.Visible = false;
                            this.panel2.Enabled = false;


                        });
                        Red = r.Next(0, 254);
                        Green = r.Next(0, 254);
                        Blue = r.Next(0, 254);

                    }
                    if (checkBoxR.Checked == false)
                    {
                        this.panel2.Invoke((MethodInvoker)delegate
                        {
                            this.panel2.Visible = true;
                            this.panel2.Enabled = true;

                        });


                    }


                    this.trackBarColorBlue.Invoke((MethodInvoker)delegate
                    {
                        this.trackBarColorBlue.Value = Blue;
                    });
                    this.trackBarColorGreen.Invoke((MethodInvoker)delegate
                    {
                        this.trackBarColorGreen.Value = Green;
                    });
                    this.trackBarColorRED.Invoke((MethodInvoker)delegate
                    {
                        this.trackBarColorRED.Value = Red;
                    });







                    kb.SetKeyColor(AK.ElementAt(keyselected), (byte)Red, (byte)Green, (byte)Blue);

                    kb.Update();
                    Thread.Sleep(201 - speed);
                    it += 1;
                    if (it == AK.Count())
                    {
                        LEDON = false;
                        it = 0;
                    }



                }
                while (stop != true && LEDON == false)
                {
                    int keyselected = r.Next(0, AK.Count());

                    if (checkBoxR.Checked == true)
                    {
                        this.panel2.Invoke((MethodInvoker)delegate
                        {
                            this.panel2.Visible = false;
                            this.panel2.Enabled = false;

                        });

                    }
                    if (checkBoxR.Checked == false)
                    {
                        this.panel2.Invoke((MethodInvoker)delegate
                        {
                            this.panel2.Visible = true;
                            this.panel2.Enabled = true;

                        });


                    }

                    kb.SetKeyColor(AK.ElementAt(keyselected), 0, 0, 0);
                    kb.Update();
                    Thread.Sleep(201 - speed);
                    it += 1;
                    if (it == AK.Count())
                    {
                        LEDON = true;
                        it = 0;
                    }

                }
            }
        }

        private void trackBarColorRED_ValueChanged(object sender, EventArgs e)
        {
            Red = trackBarColorRED.Value;
        }

        private void trackBarColorGreen_ValueChanged(object sender, EventArgs e)
        {
            Green = trackBarColorGreen.Value;
        }

        private void trackBarColorBlue_ValueChanged(object sender, EventArgs e)
        {
            Blue = trackBarColorBlue.Value;
        }

        private void backgroundWorkerMatrix_DoWork(object sender, DoWorkEventArgs e)
        {
            var AK = kb.Keys;
            Red = 255;
            Green = 0;
            Blue = 0;
            int ite = 0;
            int loop = 0;

            while (stop != true)
            {
                if (loop == 0) { Red = 254 - ite; Green = 0 + ite; }
                if (loop == 1) { Green = 254 - ite; Blue = 0 + ite; }
                if (loop == 2) { Blue = 254 - ite; Red = 0 + ite; }

                for (int j = 0; j < AK.Count(); j++)
                {
                    kb.SetKeyColor(AK.ElementAt(j), (byte)Red, (byte)Green, (byte)Blue);
                    if (Red > 0 && Green < 254 && Blue == 0)
                    {
                        Red -= 2;
                        Green += 2;
                    }

                    if (Green > 0 && Blue < 254 && Red == 0)
                    {
                        Green -= 2;
                        Blue += 2;
                    }

                    if (Blue > 0 && Red < 254 && Green == 0)
                    {
                        Blue -= 2;
                        Red += 2;
                    }


                }

                kb.Update();
                ite += 1;
                Thread.Sleep(0);
                if (ite == 254) { loop += 1; ite = 0; }
                if (loop > 2) loop = 0;
                Red = 0; Blue = 0; Green = 0;


            }










        }
    }
}
