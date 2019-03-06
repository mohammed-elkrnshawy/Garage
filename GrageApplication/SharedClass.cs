using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrageApplication
{
    class SharedClass
    {
        public static string Check_Message = "من فضلك راجع البيانات";
        public static string Add_Message = "تمت الاضافة";
        public static string Edit_Message = "تمت التعديل";
        public static string Delete_Message = "تمت الحذف";
        public static string Successful_Message = "تمت بنجاح";
        public static string No_Message = "لا يوجد ملاحظات";
        public static string Exsisting_Message = "هذا الصنف موجود مسبقا";


        public static void KeyPress(TextBox textBox, KeyPressEventArgs e)
        {
            if (textBox.Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public static void Leave(TextBox textBox)
        {
            textBox.Text = String.Format("{0:0.00}", (double.Parse(textBox.Text)));
        }

        public static void Change(TextBox textBox)
        {
            if (textBox.Text == ".")
                textBox.Text = "0.";
            if (textBox.Text == "")
                textBox.Text = "0";
        }

    }
}
