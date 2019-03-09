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
    public partial class Home_Form : Form
    {
        string value;
        XmlDocument xd = new XmlDocument();
      

        public Home_Form()
        {
            InitializeComponent();
        }

        private void addEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
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

        private void arabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            value = "1";
            xd["age"].InnerText = value;
            this.InitializeComponent();
            xd.Save("lang.xml");
        }

        private void Home_Form_Load(object sender, EventArgs e)
        {
            timer1.Start();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
        }
    }
}
