using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrageApplication
{
    public partial class AddForm : Form
    {
        private byte[] img;
        private DataSet ds;
        private int Customer_ID;

        public AddForm()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void RefForm()
        {
            bt_save.Enabled = true;
            bt_edit.Enabled = false;


            txtArea.Text = txtType.Text = txtFixer.Text= txtCarTppe.Text= txtOwner.Text = txtProduction .Text= txtPhone .Text= txtNumber .Text= "";
            txtPayment.Text= txtRender.Text= txtTotal.Text = "0";

            using (ds = HelperSQL.GetDataSet("select_CarSelect", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void ValidDataSave()
        {
            if (ValidText(txtArea) && ValidText(txtType) && ValidText(txtTotal) && ValidText(txtPayment) && ValidText(txtRender) &&
                ValidText(txtFixer) && ValidText(txtCarTppe) && ValidText(txtProduction) && ValidText(txtPhone) && ValidText(txtNumber)&&
                ValidText(txtOwner)){
                SaveData();
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void SaveData()
        {
            HelperSQL.ExecutedNoneQuery("car_insert_newCar"
            , new SqlParameter("@Name",txtArea.Text)
            , new SqlParameter("@Type", txtType.Text)
            , new SqlParameter("@Money_Tot",double.Parse(txtTotal.Text))
            , new SqlParameter("@Money_Pay",double.Parse(txtPayment.Text))
            , new SqlParameter("@Money_Rend",double.Parse(txtRender.Text))
            , new SqlParameter("@Car_Owner",txtOwner.Text)
            , new SqlParameter("@Car_Fixer",txtFixer.Text)
            , new SqlParameter("@Car_Type",txtCarTppe.Text)
            , new SqlParameter("@Car_Production", txtProduction.Text)
            , new SqlParameter("@Phone_Num", txtPhone.Text)
            , new SqlParameter("@Car_Num", txtNumber.Text)
            , new SqlParameter("@Notes", txtNotes.Text)
            , new SqlParameter("@Car_Image", img)
            );
        }

        private bool ValidText(TextBox text)
        {
            if (text.Text == "")
                return false;
            else
                return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fop = new OpenFileDialog();
            fop.InitialDirectory = @"C:\";
            fop.Filter = "[JPG,JPEG]|*.jpg";
            if (fop.ShowDialog() == DialogResult.OK)
            {
                txtPhoto.Text = fop.FileName;
                FileStream FS = new FileStream(@fop.FileName, FileMode.Open, FileAccess.Read);
                img = new byte[FS.Length];
                FS.Read(img, 0, Convert.ToInt32(FS.Length));
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = HelperSQL.GetDataSet("select_CarSelect_search", "X",new SqlParameter("@text",textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = HelperSQL.GetDataReader("Customer_selectSearch_BYID", out con, new SqlParameter("@ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                   
                    txtArea.Text = dataReader["Name"].ToString();
                    txtType.Text = dataReader["Type"].ToString();
                    txtTotal.Text = dataReader["Money_Total"].ToString();
                    txtPayment.Text = dataReader["Money_Pay"].ToString();
                    txtRender.Text = dataReader["Money_Rend"].ToString();
                    txtOwner.Text = dataReader["Car_Owner"].ToString();
                    txtFixer.Text = dataReader["Car_Fixer"].ToString();
                    txtCarTppe.Text = dataReader["Car_Type"].ToString();
                    txtProduction.Text = dataReader["Car_Production"].ToString();
                    txtPhone.Text = dataReader["Phone_Num"].ToString();
                    txtNumber.Text = dataReader["Car_Num"].ToString();
                    txtNotes.Text = dataReader["Notes"].ToString();
                    

                    img = (byte[])dataReader["Car_Image"]; 
                    MemoryStream ms = new MemoryStream((byte[])dataReader["Car_Image"]);
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Refresh();
                }
            }
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }

        private void EditData()
        {
            HelperSQL.ExecutedNoneQuery("car_update_Car"
            , new SqlParameter("@Name", txtArea.Text)
            , new SqlParameter("@Type", txtType.Text)
            , new SqlParameter("@Money_Tot", double.Parse(txtTotal.Text))
            , new SqlParameter("@Money_Pay", double.Parse(txtPayment.Text))
            , new SqlParameter("@Money_Rend", double.Parse(txtRender.Text))
            , new SqlParameter("@Car_Owner", txtOwner.Text)
            , new SqlParameter("@Car_Fixer", txtFixer.Text)
            , new SqlParameter("@Car_Type", txtCarTppe.Text)
            , new SqlParameter("@Car_Production", txtProduction.Text)
            , new SqlParameter("@Phone_Num", txtPhone.Text)
            , new SqlParameter("@Car_Num", txtNumber.Text)
            , new SqlParameter("@Notes", txtNotes.Text)
            , new SqlParameter("@Car_Image", img)
            );
        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            if (ValidText(txtArea) && ValidText(txtType) && ValidText(txtTotal) && ValidText(txtPayment) && ValidText(txtRender) &&
               ValidText(txtFixer) && ValidText(txtCarTppe) && ValidText(txtProduction) && ValidText(txtPhone) && ValidText(txtNumber) &&
               ValidText(txtOwner))
            {
                EditData();
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedClass.Check_Message);
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtTotal, e);
        }

        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtPayment, e);
        }

        private void txtRender_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtRender, e);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedClass.KeyPress(txtPhone, e);
        }
    }
}
