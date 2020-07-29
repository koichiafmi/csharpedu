using MultiLanguage.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace MultiLanguage
{
    public partial class Form : System.Windows.Forms.Form
    {
        private LanguageSetting m_languageSetting = null;

        public Form()
        {
            InitializeComponent();
            this.createLanguageSetting();
            this.setCurrentLanguage();
        }

        private void createLanguageSetting()
        {
            this.m_languageSetting = new LanguageSetting()
            {
                ClassObject = this,
                Targets = new List<object>()
                {
                    this,

                    this.menuStrip,
                    this.fileToolStripMenuItem,
                    this.saveToolStripMenuItem,
                    this.exitToolStripMenuItem,

                    this.labelNumber,
                    this.labelName,

                    this.groupBox,
                    this.radioButtonJapanese,
                    this.radioButtonEnglish,

                    this.Column1,
                    this.Column2,
                    this.Column3,

                    this.button
                }
            };
        }

        private void setCurrentLanguage()
        {
            var language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            if (language == "ja")
            {
                this.radioButtonJapanese.Checked = true;
            }
            else
            {
                this.radioButtonEnglish.Checked = true;
            }
        }

        private void radioButtonJapanese_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                this.changeLanguage("ja");
            }
        }

        private void radioButtonEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                this.changeLanguage("en");
            }
        }

        private void changeLanguage(string language)
        {
            var culture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = culture;
            this.m_languageSetting.ChangeLanguage();
        }

        private void button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.Msg_Sample,
                            Resources.Msg_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
        }

        private void textBoxNumber_Validated(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show(Resources.Msg_TextBlank,
                                this.labelNumber.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else if (!int.TryParse(textBox.Text, out _))
            {
                MessageBox.Show(Resources.Msg_TextNotNumeric,
                                this.labelNumber.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void textBoxName_Validated(object sender, EventArgs e)
        {
            var textBox = (sender as TextBox);
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show(Resources.Msg_TextBlank,
                                this.labelName.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.ShowDialog(this);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
