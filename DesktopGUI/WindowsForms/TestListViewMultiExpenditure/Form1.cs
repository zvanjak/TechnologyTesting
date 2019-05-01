using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestListViewMultiExpenditure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Ante");
            comboBox1.Items.Add("Bero");
            comboBox1.Items.Add("Cuki");
            comboBox1.Items.Add("Daki");
            comboBox1.Items.Add("Laki");
            comboBox1.Items.Add("Smrdo");
            comboBox1.Items.Add("Prdo");

        }
    }
}
