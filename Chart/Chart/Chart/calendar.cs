using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chart
{
    public partial class Calendar : Form
    {
        HearthLogger form1 = new HearthLogger();
        public Calendar()
        {
            InitializeComponent();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.SetData = monthCalendar1.SelectionRange.Start.ToString("yyyy/MM/dd");
            this.Close();
        }
    }
}
