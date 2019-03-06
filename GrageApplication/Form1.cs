using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GrageApplication
{
    public partial class Form1 : Form
    {
        string value;
        XmlDocument xd = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Controls.Clear();
            xd.Load("lang.xml");
            value = xd["age"].InnerText;
            if (int.Parse(value) == 0)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            }
            this.InitializeComponent();
        }

        private void addEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
        }

        private void arabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            value = "1";
            xd["age"].InnerText = value;
            this.InitializeComponent();
            xd.Save("lang.xml");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            value = "0";
            xd["age"].InnerText = value;
            this.InitializeComponent();
            xd.Save("lang.xml");
        }
    }
}
