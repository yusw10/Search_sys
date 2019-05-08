using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //SQL SERVER LOCAL DB
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        #region Form
        //-----------------------<region Form>------------//
        public Form2()
        {
            InitializeComponent();
           
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            load_List();
        }
        //-----------------------</region Form>------------//
        #endregion /Form

        #region Buttons
        //-----------------------<region button>------------//
        private void button1_Click(object sender, EventArgs e) //삭제 버튼
        {
            delete_Row_of_Database();
        }

        private void button2_Click(object sender, EventArgs e) // 변경 버튼 
        {
            update_Entry_in_Database();
        }

        private void button3_Click(object sender, EventArgs e) // 추가하기 버튼
        {
            add_Entry_to_Database();
        }


        //-----------------------<region button>------------//
        #endregion

        #region Methods
        //-----------------------<region method>------------//
        private void load_List() {
            //----------------< load_list>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            string sql_Text = "SELECT * FROM tbl_Search";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql_Text, cn_connection);
            adapter.Fill(tbl);
            //-< Database >


            //< show >
            listBox1.DisplayMember = "SearchData";
            listBox1.ValueMember = "indexS";

            listBox1.DataSource = tbl;
            //< /show >
            //----------------< /load_list>--------------//
        }

        private void add_Entry_to_Database() {
            //----------------< add_Entry_to_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            string sNew_Search = textBox1.Text;
            string sql_Text = "INSERT INTO tbl_Search ([SearchData]) VALUES('" + sNew_Search + "')";

            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            load_List();
            //< /reload >
            //----------------< /add_Entry_to_Database>--------------//


        }

        private void delete_Row_of_Database() {
            //----------------< delete_Row_of_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            DataRowView row = listBox1.SelectedItem as DataRowView;
            string indexS = row["indexS"].ToString();
            string sql_Text = "DELETE FROM tbl_Search WHERE(indexS = " + indexS + ")";
            
            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            load_List();
            //< /reload >
            //----------------< /delete_Row_of_Database>--------------//
        }

        private void update_Entry_in_Database() {
            //----------------< update_Entry_in_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            DataRowView row = listBox1.SelectedItem as DataRowView;
            string indexS = row["indexS"].ToString();
            string sql_Text = "UPDATE tbl_Search SET [Searchdata] = '" + textBox1.Text + "' WHERE indexS = " + indexS;
    
            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            load_List();
            //< /reload >
            //----------------< /update_Entry_in_Database>--------------//
        }
        //-----------------------</region method>------------//
        #endregion


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = listBox1.SelectedItem as DataRowView;
            textBox1.Text = row["Searchdata"].ToString();
        }

     
    }
}
