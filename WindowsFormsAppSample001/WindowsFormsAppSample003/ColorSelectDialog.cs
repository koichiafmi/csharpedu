using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppSample003
{
    public partial class ColorSelectDialog : Form
    {
        private Color setComboBox1SelectColor;
        private Color setComboBox2SelectColor;
        private Color setBackColor;
        private Color setForeColor;

        public ColorSelectDialog()
        {
            InitializeComponent();
            this.Set_Color_Items(1);
            this.Set_Color_Items(2);
            this.Set_Color_Items(3);
            this.Set_Color_Items(4);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択されたリストのKeyを取得する
            DialogCmbObjectColor obj = (DialogCmbObjectColor)comboBox1.SelectedItem;
            Color setColor = obj.Key;
            this.textBox1.BackColor = setColor;
            setComboBox1SelectColor = setColor;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択されたリストのKeyを取得する
            DialogCmbObjectColor obj = (DialogCmbObjectColor)comboBox2.SelectedItem;
            Color setColor = obj.Key;
            this.textBox2.BackColor = setColor;
            setComboBox2SelectColor = setColor;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択されたリストのKeyを取得する
            DialogCmbObjectColor obj = (DialogCmbObjectColor)comboBox3.SelectedItem;
            Color setColor = obj.Key;
            this.textBox3.BackColor = setColor;
            setBackColor = setColor;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択されたリストのKeyを取得する
            DialogCmbObjectColor obj = (DialogCmbObjectColor)comboBox4.SelectedItem;
            Color setColor = obj.Key;
            this.textBox4.BackColor = setColor;
            setForeColor = setColor;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.label2.Visible = true;
                this.textBox2.Visible = true;
                this.comboBox2.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
                this.textBox2.Visible = false;
                this.comboBox2.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                this.label3.Visible = true;
                this.textBox3.Visible = true;
                this.comboBox3.Visible = true;
            }
            else
            {
                this.label3.Visible = false;
                this.textBox3.Visible = false;
                this.comboBox3.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.label4.Visible = true;
                this.textBox4.Visible = true;
                this.comboBox4.Visible = true;
            }
            else
            {
                this.label4.Visible = false;
                this.textBox4.Visible = false;
                this.comboBox4.Visible = false;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewObject.dataGridObj.setAllRowBackgroundColor(setComboBox1SelectColor);
            DataGridViewObject.dataGridObj.setOddRowBackgroundColor(setComboBox2SelectColor);
            DataGridViewObject.dataGridObj.setSelectionBackColor(setBackColor);
            DataGridViewObject.dataGridObj.setSelectionForeColor(setForeColor);
            //自分自身のフォームを閉じる
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //自分自身のフォームを閉じる
            DataGridViewObject.dataGridObj.setAllRowBackgroundColor(Color.White);
            DataGridViewObject.dataGridObj.setOddRowBackgroundColor(Color.White);
            DataGridViewObject.dataGridObj.setSelectionBackColor(Color.Green);
            DataGridViewObject.dataGridObj.setSelectionForeColor(Color.Red);
            this.Close();

        }

        private void Set_Color_Items(int comboBoxNo)
        {
            DialogCmbObjectColor objAliceBlue = new DialogCmbObjectColor(Color.AliceBlue, "AliceBlue");
            DialogCmbObjectColor objAntiqueWhite = new DialogCmbObjectColor(Color.AntiqueWhite, "AntiqueWhite");
            DialogCmbObjectColor objAqua = new DialogCmbObjectColor(Color.Aqua, "Aqua");
            DialogCmbObjectColor objAquamarine = new DialogCmbObjectColor(Color.Aquamarine, "Aquamarine");
            DialogCmbObjectColor objAzure = new DialogCmbObjectColor(Color.Azure, "Azure");
            DialogCmbObjectColor objBeige = new DialogCmbObjectColor(Color.Beige, "Beige");
            DialogCmbObjectColor objBisque = new DialogCmbObjectColor(Color.Bisque, "Bisque");
            DialogCmbObjectColor objBlack = new DialogCmbObjectColor(Color.Black, "Black");
            DialogCmbObjectColor objBlanchedAlmond = new DialogCmbObjectColor(Color.BlanchedAlmond, "BlanchedAlmond");
            DialogCmbObjectColor objBlue = new DialogCmbObjectColor(Color.Blue, "Blue");
            DialogCmbObjectColor objBlueViolet = new DialogCmbObjectColor(Color.BlueViolet, "BlueViolet");
            DialogCmbObjectColor objBrown = new DialogCmbObjectColor(Color.Brown, "Brown");
            DialogCmbObjectColor objBurlyWood = new DialogCmbObjectColor(Color.BurlyWood, "BurlyWood");
            DialogCmbObjectColor objCadetBlue = new DialogCmbObjectColor(Color.CadetBlue, "CadetBlue");
            DialogCmbObjectColor objChartreuse = new DialogCmbObjectColor(Color.Chartreuse, "Chartreuse");
            DialogCmbObjectColor objChocolate = new DialogCmbObjectColor(Color.Chocolate, "Chocolate");
            DialogCmbObjectColor objCoral = new DialogCmbObjectColor(Color.Coral, "Coral");
            DialogCmbObjectColor objCornflowerBlue = new DialogCmbObjectColor(Color.CornflowerBlue, "CornflowerBlue");
            DialogCmbObjectColor objCornsilk = new DialogCmbObjectColor(Color.Cornsilk, "Cornsilk");
            DialogCmbObjectColor objCrimson = new DialogCmbObjectColor(Color.Crimson, "Crimson");
            DialogCmbObjectColor objCyan = new DialogCmbObjectColor(Color.Cyan, "Cyan");
            DialogCmbObjectColor objDarkBlue = new DialogCmbObjectColor(Color.DarkBlue, "DarkBlue");
            DialogCmbObjectColor objDarkCyan = new DialogCmbObjectColor(Color.DarkCyan, "DarkCyan");
            DialogCmbObjectColor objDarkGoldenrod = new DialogCmbObjectColor(Color.DarkGoldenrod, "DarkGoldenrod");
            DialogCmbObjectColor objDarkGray = new DialogCmbObjectColor(Color.DarkGray, "DarkGray");
            DialogCmbObjectColor objDarkGreen = new DialogCmbObjectColor(Color.DarkGreen, "DarkGreen");
            DialogCmbObjectColor objDarkKhaki = new DialogCmbObjectColor(Color.DarkKhaki, "DarkKhaki");
            DialogCmbObjectColor objDarkMagenta = new DialogCmbObjectColor(Color.DarkMagenta, "DarkMagenta");
            DialogCmbObjectColor objDarkOliveGreen = new DialogCmbObjectColor(Color.DarkOliveGreen, "DarkOliveGreen");
            DialogCmbObjectColor objDarkOrange = new DialogCmbObjectColor(Color.DarkOrange, "DarkOrange");
            DialogCmbObjectColor objDarkOrchid = new DialogCmbObjectColor(Color.DarkOrchid, "DarkOrchid");
            DialogCmbObjectColor objDarkRed = new DialogCmbObjectColor(Color.DarkRed, "DarkRed");
            DialogCmbObjectColor objDarkSalmon = new DialogCmbObjectColor(Color.DarkSalmon, "DarkSalmon");
            DialogCmbObjectColor objDarkSeaGreen = new DialogCmbObjectColor(Color.DarkSeaGreen, "DarkSeaGreen");
            DialogCmbObjectColor objDarkSlateBlue = new DialogCmbObjectColor(Color.DarkSlateBlue, "DarkSlateBlue");
            DialogCmbObjectColor objDarkSlateGray = new DialogCmbObjectColor(Color.DarkSlateGray, "DarkSlateGray");
            DialogCmbObjectColor objDarkTurquoise = new DialogCmbObjectColor(Color.DarkTurquoise, "DarkTurquoise");
            DialogCmbObjectColor objDarkViolet = new DialogCmbObjectColor(Color.DarkViolet, "DarkViolet");
            DialogCmbObjectColor objDeepPink = new DialogCmbObjectColor(Color.DeepPink, "DeepPink");
            DialogCmbObjectColor objDeepSkyBlue = new DialogCmbObjectColor(Color.DeepSkyBlue, "DeepSkyBlue");
            DialogCmbObjectColor objDimGray = new DialogCmbObjectColor(Color.DimGray, "DimGray");
            DialogCmbObjectColor objDodgerBlue = new DialogCmbObjectColor(Color.DodgerBlue, "DodgerBlue");
            DialogCmbObjectColor objFirebrick = new DialogCmbObjectColor(Color.Firebrick, "Firebrick");
            DialogCmbObjectColor objFloralWhite = new DialogCmbObjectColor(Color.FloralWhite, "FloralWhite");
            DialogCmbObjectColor objForestGreen = new DialogCmbObjectColor(Color.ForestGreen, "ForestGreen");
            DialogCmbObjectColor objFuchsia = new DialogCmbObjectColor(Color.Fuchsia, "Fuchsia");
            DialogCmbObjectColor objGainsboro = new DialogCmbObjectColor(Color.Gainsboro, "Gainsboro");
            DialogCmbObjectColor objGhostWhite = new DialogCmbObjectColor(Color.GhostWhite, "GhostWhite");
            DialogCmbObjectColor objGold = new DialogCmbObjectColor(Color.Gold, "Gold");
            DialogCmbObjectColor objGoldenrod = new DialogCmbObjectColor(Color.Goldenrod, "Goldenrod");
            DialogCmbObjectColor objGray = new DialogCmbObjectColor(Color.Gray, "Gray");
            DialogCmbObjectColor objGreen = new DialogCmbObjectColor(Color.Green, "Green");
            DialogCmbObjectColor objGreenYellow = new DialogCmbObjectColor(Color.GreenYellow, "GreenYellow");
            DialogCmbObjectColor objHoneydew = new DialogCmbObjectColor(Color.Honeydew, "Honeydew");
            DialogCmbObjectColor objHotPink = new DialogCmbObjectColor(Color.HotPink, "HotPink");
            DialogCmbObjectColor objIndianRed = new DialogCmbObjectColor(Color.IndianRed, "IndianRed");
            DialogCmbObjectColor objIndigo = new DialogCmbObjectColor(Color.Indigo, "Indigo");
            DialogCmbObjectColor objIvory = new DialogCmbObjectColor(Color.Ivory, "Ivory");
            DialogCmbObjectColor objKhaki = new DialogCmbObjectColor(Color.Khaki, "Khaki");
            DialogCmbObjectColor objLavender = new DialogCmbObjectColor(Color.Lavender, "Lavender");
            DialogCmbObjectColor objLavenderBlush = new DialogCmbObjectColor(Color.LavenderBlush, "LavenderBlush");
            DialogCmbObjectColor objLawnGreen = new DialogCmbObjectColor(Color.LawnGreen, "LawnGreen");
            DialogCmbObjectColor objLemonChiffon = new DialogCmbObjectColor(Color.LemonChiffon, "LemonChiffon");
            DialogCmbObjectColor objLightBlue = new DialogCmbObjectColor(Color.LightBlue, "LightBlue");
            DialogCmbObjectColor objLightCoral = new DialogCmbObjectColor(Color.LightCoral, "LightCoral");
            DialogCmbObjectColor objLightCyan = new DialogCmbObjectColor(Color.LightCyan, "LightCyan");
            DialogCmbObjectColor objLightGoldenrodYellow = new DialogCmbObjectColor(Color.LightGoldenrodYellow, "LightGoldenrodYellow");
            DialogCmbObjectColor objLightGray = new DialogCmbObjectColor(Color.LightGray, "LightGray");
            DialogCmbObjectColor objLightGreen = new DialogCmbObjectColor(Color.LightGreen, "LightGreen");
            DialogCmbObjectColor objLightPink = new DialogCmbObjectColor(Color.LightPink, "LightPink");
            DialogCmbObjectColor objLightSalmon = new DialogCmbObjectColor(Color.LightSalmon, "LightSalmon");
            DialogCmbObjectColor objLightSeaGreen = new DialogCmbObjectColor(Color.LightSeaGreen, "LightSeaGreen");
            DialogCmbObjectColor objLightSkyBlue = new DialogCmbObjectColor(Color.LightSkyBlue, "LightSkyBlue");
            DialogCmbObjectColor objLightSlateGray = new DialogCmbObjectColor(Color.LightSlateGray, "LightSlateGray");
            DialogCmbObjectColor objLightSteelBlue = new DialogCmbObjectColor(Color.LightSteelBlue, "LightSteelBlue");
            DialogCmbObjectColor objLightYellow = new DialogCmbObjectColor(Color.LightYellow, "LightYellow");
            DialogCmbObjectColor objLime = new DialogCmbObjectColor(Color.Lime, "Lime");
            DialogCmbObjectColor objLimeGreen = new DialogCmbObjectColor(Color.LimeGreen, "LimeGreen");
            DialogCmbObjectColor objLinen = new DialogCmbObjectColor(Color.Linen, "Linen");
            DialogCmbObjectColor objMagenta = new DialogCmbObjectColor(Color.Magenta, "Magenta");
            DialogCmbObjectColor objMaroon = new DialogCmbObjectColor(Color.Maroon, "Maroon");
            DialogCmbObjectColor objMediumAquamarine = new DialogCmbObjectColor(Color.MediumAquamarine, "MediumAquamarine");
            DialogCmbObjectColor objMediumBlue = new DialogCmbObjectColor(Color.MediumBlue, "MediumBlue");
            DialogCmbObjectColor objMediumOrchid = new DialogCmbObjectColor(Color.MediumOrchid, "MediumOrchid");
            DialogCmbObjectColor objMediumPurple = new DialogCmbObjectColor(Color.MediumPurple, "MediumPurple");
            DialogCmbObjectColor objMediumSeaGreen = new DialogCmbObjectColor(Color.MediumSeaGreen, "MediumSeaGreen");
            DialogCmbObjectColor objMediumSlateBlue = new DialogCmbObjectColor(Color.MediumSlateBlue, "MediumSlateBlue");
            DialogCmbObjectColor objMediumSpringGreen = new DialogCmbObjectColor(Color.MediumSpringGreen, "MediumSpringGreen");
            DialogCmbObjectColor objMediumTurquoise = new DialogCmbObjectColor(Color.MediumTurquoise, "MediumTurquoise");
            DialogCmbObjectColor objMediumVioletRed = new DialogCmbObjectColor(Color.MediumVioletRed, "MediumVioletRed");
            DialogCmbObjectColor objMidnightBlue = new DialogCmbObjectColor(Color.MidnightBlue, "MidnightBlue");
            DialogCmbObjectColor objMintCream = new DialogCmbObjectColor(Color.MintCream, "MintCream");
            DialogCmbObjectColor objMistyRose = new DialogCmbObjectColor(Color.MistyRose, "MistyRose");
            DialogCmbObjectColor objMoccasin = new DialogCmbObjectColor(Color.Moccasin, "Moccasin");
            DialogCmbObjectColor objNavajoWhite = new DialogCmbObjectColor(Color.NavajoWhite, "NavajoWhite");
            DialogCmbObjectColor objNavy = new DialogCmbObjectColor(Color.Navy, "Navy");
            DialogCmbObjectColor objOldLace = new DialogCmbObjectColor(Color.OldLace, "OldLace");
            DialogCmbObjectColor objOlive = new DialogCmbObjectColor(Color.Olive, "Olive");
            DialogCmbObjectColor objOliveDrab = new DialogCmbObjectColor(Color.OliveDrab, "OliveDrab");
            DialogCmbObjectColor objOrange = new DialogCmbObjectColor(Color.Orange, "Orange");
            DialogCmbObjectColor objOrangeRed = new DialogCmbObjectColor(Color.OrangeRed, "OrangeRed");
            DialogCmbObjectColor objOrchid = new DialogCmbObjectColor(Color.Orchid, "Orchid");
            DialogCmbObjectColor objPaleGoldenrod = new DialogCmbObjectColor(Color.PaleGoldenrod, "PaleGoldenrod");
            DialogCmbObjectColor objPaleGreen = new DialogCmbObjectColor(Color.PaleGreen, "PaleGreen");
            DialogCmbObjectColor objPaleTurquoise = new DialogCmbObjectColor(Color.PaleTurquoise, "PaleTurquoise");
            DialogCmbObjectColor objPaleVioletRed = new DialogCmbObjectColor(Color.PaleVioletRed, "PaleVioletRed");
            DialogCmbObjectColor objPapayaWhip = new DialogCmbObjectColor(Color.PapayaWhip, "PapayaWhip");
            DialogCmbObjectColor objPeachPuff = new DialogCmbObjectColor(Color.PeachPuff, "PeachPuff");
            DialogCmbObjectColor objPeru = new DialogCmbObjectColor(Color.Peru, "Peru");
            DialogCmbObjectColor objPink = new DialogCmbObjectColor(Color.Pink, "Pink");
            DialogCmbObjectColor objPlum = new DialogCmbObjectColor(Color.Plum, "Plum");
            DialogCmbObjectColor objPowderBlue = new DialogCmbObjectColor(Color.PowderBlue, "PowderBlue");
            DialogCmbObjectColor objPurple = new DialogCmbObjectColor(Color.Purple, "Purple");
            DialogCmbObjectColor objRed = new DialogCmbObjectColor(Color.Red, "Red");
            DialogCmbObjectColor objRosyBrown = new DialogCmbObjectColor(Color.RosyBrown, "RosyBrown");
            DialogCmbObjectColor objRoyalBlue = new DialogCmbObjectColor(Color.RoyalBlue, "RoyalBlue");
            DialogCmbObjectColor objSaddleBrown = new DialogCmbObjectColor(Color.SaddleBrown, "SaddleBrown");
            DialogCmbObjectColor objSalmon = new DialogCmbObjectColor(Color.Salmon, "Salmon");
            DialogCmbObjectColor objSandyBrown = new DialogCmbObjectColor(Color.SandyBrown, "SandyBrown");
            DialogCmbObjectColor objSeaGreen = new DialogCmbObjectColor(Color.SeaGreen, "SeaGreen");
            DialogCmbObjectColor objSeaShell = new DialogCmbObjectColor(Color.SeaShell, "SeaShell");
            DialogCmbObjectColor objSienna = new DialogCmbObjectColor(Color.Sienna, "Sienna");
            DialogCmbObjectColor objSilver = new DialogCmbObjectColor(Color.Silver, "Silver");
            DialogCmbObjectColor objSkyBlue = new DialogCmbObjectColor(Color.SkyBlue, "SkyBlue");
            DialogCmbObjectColor objSlateBlue = new DialogCmbObjectColor(Color.SlateBlue, "SlateBlue");
            DialogCmbObjectColor objSlateGray = new DialogCmbObjectColor(Color.SlateGray, "SlateGray");
            DialogCmbObjectColor objSnow = new DialogCmbObjectColor(Color.Snow, "Snow");
            DialogCmbObjectColor objSpringGreen = new DialogCmbObjectColor(Color.SpringGreen, "SpringGreen");
            DialogCmbObjectColor objSteelBlue = new DialogCmbObjectColor(Color.SteelBlue, "SteelBlue");
            DialogCmbObjectColor objTan = new DialogCmbObjectColor(Color.Tan, "Tan");
            DialogCmbObjectColor objTeal = new DialogCmbObjectColor(Color.Teal, "Teal");
            DialogCmbObjectColor objThistle = new DialogCmbObjectColor(Color.Thistle, "Thistle");
            DialogCmbObjectColor objTomato = new DialogCmbObjectColor(Color.Tomato, "Tomato");
            DialogCmbObjectColor objTurquoise = new DialogCmbObjectColor(Color.Turquoise, "Turquoise");
            DialogCmbObjectColor objViolet = new DialogCmbObjectColor(Color.Violet, "Violet");
            DialogCmbObjectColor objWheat = new DialogCmbObjectColor(Color.Wheat, "Wheat");
            DialogCmbObjectColor objWhite = new DialogCmbObjectColor(Color.White, "White");
            DialogCmbObjectColor objWhiteSmoke = new DialogCmbObjectColor(Color.WhiteSmoke, "WhiteSmoke");
            DialogCmbObjectColor objYellow = new DialogCmbObjectColor(Color.Yellow, "Yellow");
            DialogCmbObjectColor objYellowGreen = new DialogCmbObjectColor(Color.YellowGreen, "YellowGreen");

            ComboBox comboBox = null;
            if (1 == comboBoxNo)
            {
                comboBox = comboBox1;
            }

            if (2 == comboBoxNo)
            {
                comboBox = comboBox2;
            }

            if (3 == comboBoxNo)
            {
                comboBox = comboBox3;
            }

            if (4 == comboBoxNo)
            {
                comboBox = comboBox4;
            }

            comboBox.Items.Add(objAliceBlue);
            comboBox.Items.Add(objAntiqueWhite);
            comboBox.Items.Add(objAqua);
            comboBox.Items.Add(objAquamarine);
            comboBox.Items.Add(objAzure);
            comboBox.Items.Add(objBeige);
            comboBox.Items.Add(objBisque);
            comboBox.Items.Add(objBlack);
            comboBox.Items.Add(objBlanchedAlmond);
            comboBox.Items.Add(objBlue);
            comboBox.Items.Add(objBlueViolet);
            comboBox.Items.Add(objBrown);
            comboBox.Items.Add(objBurlyWood);
            comboBox.Items.Add(objCadetBlue);
            comboBox.Items.Add(objChartreuse);
            comboBox.Items.Add(objChocolate);
            comboBox.Items.Add(objCoral);
            comboBox.Items.Add(objCornflowerBlue);
            comboBox.Items.Add(objCornsilk);
            comboBox.Items.Add(objCrimson);
            comboBox.Items.Add(objCyan);
            comboBox.Items.Add(objDarkBlue);
            comboBox.Items.Add(objDarkCyan);
            comboBox.Items.Add(objDarkGoldenrod);
            comboBox.Items.Add(objDarkGray);
            comboBox.Items.Add(objDarkGreen);
            comboBox.Items.Add(objDarkKhaki);
            comboBox.Items.Add(objDarkMagenta);
            comboBox.Items.Add(objDarkOliveGreen);
            comboBox.Items.Add(objDarkOrange);
            comboBox.Items.Add(objDarkOrchid);
            comboBox.Items.Add(objDarkRed);
            comboBox.Items.Add(objDarkSalmon);
            comboBox.Items.Add(objDarkSeaGreen);
            comboBox.Items.Add(objDarkSlateBlue);
            comboBox.Items.Add(objDarkSlateGray);
            comboBox.Items.Add(objDarkTurquoise);
            comboBox.Items.Add(objDarkViolet);
            comboBox.Items.Add(objDeepPink);
            comboBox.Items.Add(objDeepSkyBlue);
            comboBox.Items.Add(objDimGray);
            comboBox.Items.Add(objDodgerBlue);
            comboBox.Items.Add(objFirebrick);
            comboBox.Items.Add(objFloralWhite);
            comboBox.Items.Add(objForestGreen);
            comboBox.Items.Add(objFuchsia);
            comboBox.Items.Add(objGainsboro);
            comboBox.Items.Add(objGhostWhite);
            comboBox.Items.Add(objGold);
            comboBox.Items.Add(objGoldenrod);
            comboBox.Items.Add(objGray);
            comboBox.Items.Add(objGreen);
            comboBox.Items.Add(objGreenYellow);
            comboBox.Items.Add(objHoneydew);
            comboBox.Items.Add(objHotPink);
            comboBox.Items.Add(objIndianRed);
            comboBox.Items.Add(objIndigo);
            comboBox.Items.Add(objIvory);
            comboBox.Items.Add(objKhaki);
            comboBox.Items.Add(objLavender);
            comboBox.Items.Add(objLavenderBlush);
            comboBox.Items.Add(objLawnGreen);
            comboBox.Items.Add(objLemonChiffon);
            comboBox.Items.Add(objLightBlue);
            comboBox.Items.Add(objLightCoral);
            comboBox.Items.Add(objLightCyan);
            comboBox.Items.Add(objLightGoldenrodYellow);
            comboBox.Items.Add(objLightGray);
            comboBox.Items.Add(objLightGreen);
            comboBox.Items.Add(objLightPink);
            comboBox.Items.Add(objLightSalmon);
            comboBox.Items.Add(objLightSeaGreen);
            comboBox.Items.Add(objLightSkyBlue);
            comboBox.Items.Add(objLightSlateGray);
            comboBox.Items.Add(objLightSteelBlue);
            comboBox.Items.Add(objLightYellow);
            comboBox.Items.Add(objLime);
            comboBox.Items.Add(objLimeGreen);
            comboBox.Items.Add(objLinen);
            comboBox.Items.Add(objMagenta);
            comboBox.Items.Add(objMaroon);
            comboBox.Items.Add(objMediumAquamarine);
            comboBox.Items.Add(objMediumBlue);
            comboBox.Items.Add(objMediumOrchid);
            comboBox.Items.Add(objMediumPurple);
            comboBox.Items.Add(objMediumSeaGreen);
            comboBox.Items.Add(objMediumSlateBlue);
            comboBox.Items.Add(objMediumSpringGreen);
            comboBox.Items.Add(objMediumTurquoise);
            comboBox.Items.Add(objMediumVioletRed);
            comboBox.Items.Add(objMidnightBlue);
            comboBox.Items.Add(objMintCream);
            comboBox.Items.Add(objMistyRose);
            comboBox.Items.Add(objMoccasin);
            comboBox.Items.Add(objNavajoWhite);
            comboBox.Items.Add(objNavy);
            comboBox.Items.Add(objOldLace);
            comboBox.Items.Add(objOlive);
            comboBox.Items.Add(objOliveDrab);
            comboBox.Items.Add(objOrange);
            comboBox.Items.Add(objOrangeRed);
            comboBox.Items.Add(objOrchid);
            comboBox.Items.Add(objPaleGoldenrod);
            comboBox.Items.Add(objPaleGreen);
            comboBox.Items.Add(objPaleTurquoise);
            comboBox.Items.Add(objPaleVioletRed);
            comboBox.Items.Add(objPapayaWhip);
            comboBox.Items.Add(objPeachPuff);
            comboBox.Items.Add(objPeru);
            comboBox.Items.Add(objPink);
            comboBox.Items.Add(objPlum);
            comboBox.Items.Add(objPowderBlue);
            comboBox.Items.Add(objPurple);
            comboBox.Items.Add(objRed);
            comboBox.Items.Add(objRosyBrown);
            comboBox.Items.Add(objRoyalBlue);
            comboBox.Items.Add(objSaddleBrown);
            comboBox.Items.Add(objSalmon);
            comboBox.Items.Add(objSandyBrown);
            comboBox.Items.Add(objSeaGreen);
            comboBox.Items.Add(objSeaShell);
            comboBox.Items.Add(objSienna);
            comboBox.Items.Add(objSilver);
            comboBox.Items.Add(objSkyBlue);
            comboBox.Items.Add(objSlateBlue);
            comboBox.Items.Add(objSlateGray);
            comboBox.Items.Add(objSnow);
            comboBox.Items.Add(objSpringGreen);
            comboBox.Items.Add(objSteelBlue);
            comboBox.Items.Add(objTan);
            comboBox.Items.Add(objTeal);
            comboBox.Items.Add(objThistle);
            comboBox.Items.Add(objTomato);
            comboBox.Items.Add(objTurquoise);
            comboBox.Items.Add(objViolet);
            comboBox.Items.Add(objWheat);
            comboBox.Items.Add(objWhite);
            comboBox.Items.Add(objWhiteSmoke);
            comboBox.Items.Add(objYellow);
            comboBox.Items.Add(objYellowGreen);
        }

    }
    // データクラス
    class DialogCmbObjectColor
    {
        public Color Key { get; set; }
        public string Value { get; set; }

        public DialogCmbObjectColor(Color Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
