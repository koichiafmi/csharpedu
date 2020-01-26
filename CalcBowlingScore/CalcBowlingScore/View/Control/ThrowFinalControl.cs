using System;
using System.Windows.Forms;

namespace CalcBowlingScore
{
    public partial class ThrowFinalControl : ThrowControl
    {
        public ThrowFinalControl() : base()
        {
            InitializeComponent();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.comboBox3rdThrow.SelectedIndexChanged -= this.comboBox3rdThrow_SelectedIndexChanged;
            this.initializeComboBoxItems(this.comboBox3rdThrow);
            this.comboBox3rdThrow.SelectedIndexChanged += this.comboBox3rdThrow_SelectedIndexChanged;
        }

        protected override void comboBox1stThrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var pin = int.Parse(comboBox.Text);
            this.SendThrownEvent(sender, new ThrowData(1, pin));

            this.comboBox2ndThrow.Items.Clear();
            if (pin == 10)
            {
                for (var i = 0; i <= 10; i++)
                {
                    this.comboBox2ndThrow.Items.Add(i.ToString());
                }
            }
            else
            {
                for (var i = 0; i <= (10 - pin); i++)
                {
                    this.comboBox2ndThrow.Items.Add(i.ToString());
                }
            }
        }

        protected override void comboBox2ndThrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var pin2 = int.Parse(comboBox.Text);
            this.SendThrownEvent(sender, new ThrowData(2, pin2));

            this.comboBox3rdThrow.Items.Clear();

            var pin1 = int.Parse(this.comboBox1stThrow.Text);
            if ((pin1 + pin2) < 10)
            {
                this.comboBox3rdThrow.Enabled = false;
                return;
            }
            else if ((pin2 == 10) || ((pin1 + pin2) == 10))
            {
                this.comboBox3rdThrow.Enabled = true;
                for (var i = 0; i <= 10; i++)
                {
                    this.comboBox3rdThrow.Items.Add(i.ToString());
                }
            }
            else
            {
                this.comboBox3rdThrow.Enabled = true;
                for (var i = 0; i <= (10 - pin2); i++)
                {
                    this.comboBox3rdThrow.Items.Add(i.ToString());
                }
            }
        }
        
        private void comboBox3rdThrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (sender as ComboBox);
            var pin = int.Parse(comboBox.Text);
            this.SendThrownEvent(sender, new ThrowData(3, pin));
        }
    }
}
