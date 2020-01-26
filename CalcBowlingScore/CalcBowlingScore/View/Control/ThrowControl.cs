using System;
using System.Linq;
using System.Windows.Forms;

namespace CalcBowlingScore
{
    public partial class ThrowControl : UserControl
    {
        public EventHandler<ThrowData> Thrown = null;

        public ThrowControl()
        {
            InitializeComponent();
        }

        public virtual void Initialize()
        {
            this.comboBox1stThrow.SelectedIndexChanged -= this.comboBox1stThrow_SelectedIndexChanged;
            this.initializeComboBoxItems(this.comboBox1stThrow);
            this.comboBox1stThrow.SelectedIndexChanged += this.comboBox1stThrow_SelectedIndexChanged;

            this.comboBox2ndThrow.SelectedIndexChanged -= this.comboBox2ndThrow_SelectedIndexChanged;
            this.initializeComboBoxItems(this.comboBox2ndThrow);
            this.comboBox2ndThrow.SelectedIndexChanged += this.comboBox2ndThrow_SelectedIndexChanged;
        }

        public void Finished()
        {
            this.Controls.OfType<ComboBox>().ToList().ForEach(e => e.Enabled = false);
        }

        protected void initializeComboBoxItems(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(Common.CreatePinTexts());
            comboBox.SelectedIndex = -1;
            comboBox.Enabled = true;
        }

        protected virtual void comboBox1stThrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var pin = int.Parse(comboBox.Text);
            this.SendThrownEvent(sender, new ThrowData(1, pin));

            this.comboBox2ndThrow.Items.Clear();
            if (pin == 10)
            {
                this.comboBox2ndThrow.Enabled = false;
            }
            else
            {
                this.comboBox2ndThrow.Enabled = true;
                for (var i = 0; i <= (10 - pin); i++)
                {
                    this.comboBox2ndThrow.Items.Add(i.ToString());
                }
            }
        }

        protected virtual void comboBox2ndThrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var pin = int.Parse(comboBox.Text);
            this.SendThrownEvent(sender, new ThrowData(2, pin));
        }

        protected void SendThrownEvent(object sender, ThrowData args)
        {
            if (this.Thrown != null)
            {
                this.Thrown(sender, args);
            }
        }
    }
}
