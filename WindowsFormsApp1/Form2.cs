using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public test memberType;
        public enum test {
            link = 0,
            regular,
            associate,
            daypass
        }

        public Form2()
        {
            InitializeComponent();
           
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            /*
            listBox1.Items.Add("VIP");
            listBox1.Items.Add("정회원");
            listBox1.Items.Add("준회원");
            listBox1.Items.Add("테스트");
            listBox1.SelectedIndex = 1;
            */
            listBox1.SelectionMode = SelectionMode.MultiExtended;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            memberType = (test)listBox1.SelectedIndex;
        }

        public static int i = 0;
        private void button1_Click(object sender, EventArgs e) //삭제 버튼
        {
            listBox1.BeginUpdate();
            if (i < 4) {
                listBox1.Items.Add("테스트" + i);
                i++;
            }
            listBox1.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e) // 변경 버튼 // 일단 임시로 전부 합치는거 버튼으로 사용해봄
        {
            string result = "";
            int j = 0;
            while(j < 3) { 
                listBox1.SetSelected(i, true);
                result += listBox1.SelectedItems.ToString();

            }
            MessageBox.Show(result);
        }
    }
}
