﻿using System;
using System.Windows.Forms;

namespace DbMultiTool
{
    public partial class NSProject_Main : Form
    {
        public NSProject_Main()
        {
            InitializeComponent();
        }

        private void NSProject_Main_Load(object sender, EventArgs e)
        {

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testProxy = new MethodBaseProxy<AopTestClass>(1, 1).GetInstance();
            testProxy.GetOutPut();
        }
    }
}