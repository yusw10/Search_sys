using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moda.Korean.TwitterKoreanProcessorCS;
using System.Data.SqlClient; //SQL SERVER LOCAL DB
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        

        #region Function
        //-----------------------< Function >------------------------//
        public bool IsContainKorean(string s) {
            //한글 확인 함수
            char[] charArr = s.ToCharArray();
            foreach (char c in charArr) {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter) {
                    return true;
                }
            }
            return false;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) //1브라우저 처음에 ㄷ
        {
            //브라우저가 표시완료하면 나오게 하는 함수
            
            //textBox1.Text = webBrowser1.Document.Url.ToString();
        }

        private void WebBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Button6_Click(sender, e);
            }
        } //  텍스트창에서 엔터키 눌렀을때 호출 함수

        private void CheckWeb(object sender, EventArgs e)
        {
            if (checkBox1.Checked & !checkBox2.Checked)// 히스토리없이 webBrowser1만 보여지고 있었다면
            {
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 0;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            }
            else if (checkBox1.Checked & checkBox2.Checked) // 히스토리가 보여지고 있었다면
            {
                checkBox2.Checked = false;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 0;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            }
            else
            { // 체크해제
                tableLayoutPanel1.ColumnCount = 1;
            }
        } // webBrowser2 호출

        private void CheckHisory(object sender, EventArgs e)
        {
            if (checkBox2.Checked & !checkBox1.Checked)
            { //webBrowser2 없이 webBrowser1만 보여지고 있었다면
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 1;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            }
            else if (checkBox2.Checked & checkBox1.Checked)
            {
                checkBox1.Checked = false;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 1;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            }
            else
            {
                tableLayoutPanel1.ColumnCount = 1;
            }

        } // History 호출


        //-----------------------< Function >------------------------//
        #endregion /Function

        #region Form
        //-----------------------< Form >-----------------------------//
        public Form1()
        {
            //Form1.Designer를 불러서 UI를 구성하게하는 필수적인 친구
            InitializeComponent();
            webBrowser2.Visible = true;
            tableLayoutPanel1.ColumnCount = 1;
            this.Text = "H browser";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_List(); //시작할 때 데이터베이스 연동
        }

        //-----------------------< /Form >-----------------------------//
        #endregion /Form

        #region Button
        //-----------------------< region button >------------------------//

        private void Button2_Click(object sender, EventArgs e) 
        {
            webBrowser1.GoBack();
        }// 뒤로가기

        private void Button3_Click(object sender, EventArgs e) 
        {
            webBrowser1.GoForward();
        }// 앞으로가기

        private void Button4_Click(object sender, EventArgs e) 
        {
            webBrowser1.Refresh();
        }// 새로고침

        private void Button5_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        } // 홈으로 가기

        private void Button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) // 문장검색 기능 키면 파싱해서 검색보내버리기
            {
                string searching_data = textBox1.Text; //게이
                Search_Add_Entry_to_Database(searching_data); // 한글 검색 내용을 Database에 저장
                string tmp1 = "https://www.google.com/search?&q=" + StemSample2(searching_data);
                string tmp2 = "https://www.google.com/search?&q=" + searching_data;
                webBrowser1.Navigate(tmp1);
                webBrowser2.Navigate(tmp2);
                
            }
            else // 문장검색이 아니라면
            {
                if (IsContainKorean(textBox1.Text))
                { // 한글을 친거면
                    string tmp = " https://www.google.com/search?q=" + textBox1.Text + "&& aqs = chrome..69i57j0j69i61j0j69i61l2.7224j0j4 & sourceid = chrome & ie = UTF - 8";
                    webBrowser1.Navigate(tmp);
                }
                else
                {
                    webBrowser1.Navigate(textBox1.Text);
                }
            }
        } // 체크박스 처리버튼

        private void Button1_Click(object sender, EventArgs e)
        {
            Add_Entry_to_Database();
        } // 추가하기

        private void Button8_Click(object sender, EventArgs e)
        {
            Update_Entry_in_Database();
        } // 변경 하기

        private void Button7_Click(object sender, EventArgs e)
        {
            Delete_Row_of_Database();
        } // 삭제하기



        //-----------------------< /region button >------------------------//
        #endregion /Button

        #region Parsing_Data
        //-----------------------< parsing data >-----------------------------//
        public string StemSample2(string input_Search_Data)
        {
            var tokens = TwitterKoreanProcessorCS.Tokenize(
                              TwitterKoreanProcessorCS.Normalize(input_Search_Data));
            var stemmedTokens = TwitterKoreanProcessorCS.Stem(tokens);

            List<string> list = new List<string>();
            List<string> second = new List<string>();
            List<string> third = new List<string>();
            List<string> forth = new List<string>();
            List<string> fifth = new List<string>();
            List<string> final = new List<string>();

            String sum = "";

            foreach (var stemmedToken in stemmedTokens)
            {
                if (stemmedToken.Pos.ToString().Contains("Noun") || stemmedToken.Pos.ToString() == "Alpha" || stemmedToken.Pos.ToString() == "Punctuation" || stemmedToken.Pos.ToString() == "Adverb" || stemmedToken.Pos.ToString() == "Number")
                {
                    list.Add(stemmedToken.Text);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Contains("제외"))
                {
                    second.Add(" -" + list[i - 1]);
                    second.RemoveAt(i - 1);
                }
                else if (list[i].Contains("혹은"))
                {
                    second.Remove(second.Last());
                    second.Add(list[i - 1] + " OR " + list[i + 1]);
                    list.RemoveAt(i + 1);
                }
                else if (list[i].Contains("~"))
                {
                    second.Remove(second.Last());
                    second.Add(list[i - 1] + ".." + list[i + 1]);
                    list.RemoveAt(i + 1);
                }
                else
                    second.Add(list[i]);
            }

            for (int i = 0; i < second.Count; i++)
            {
                if (second[i].Contains("형식"))
                {
                    if (second[i - 1].Contains("ppt"))
                    {
                        second[i] = "filetype:ppt";
                        third.Add(second[i]);
                        third.RemoveAt(i - 1);
                    }

                    else if (second[i - 1].Contains("pdf"))
                    {
                        second[i] = "filetype:pdf";
                        third.Add(second[i]);
                        third.RemoveAt(i - 1);
                    }

                    else if (second[i - 1].Contains("xls"))
                    {
                        second[i] = "filetype:xls";
                        third.Add(second[i]);
                        third.RemoveAt(i - 1);
                    }

                    else if (second[i - 1].Contains("doc"))
                    {
                        second[i] = "filetype:doc";
                        third.Add(second[i]);
                        third.RemoveAt(i - 1);
                    }
                }
                else
                    third.Add(second[i]);
            }

            for (int i = 0; i < third.Count; i++)
            {
                if (third[i] == "인스타그램")
                    forth.Add("site:www.instagram.com");
                else if (third[i] == "네이버")
                    forth.Add("site:www.naver.com");
                else if (third[i] == "페이스북")
                    forth.Add("site:www.facebook.com");
                else if (third[i] == "구글")
                    forth.Add("site:www.google.com");
                else if (third[i] == "유투브")
                    forth.Add("site:www.youtube.com");
                else
                    forth.Add(third[i]);
            }

            for (int i = 0; i < forth.Count; i++)
            {
                if (forth[i].Contains("해쉬태그"))
                {
                    fifth.Remove(fifth.Last());
                    fifth.Add(" #" + forth[i - 1]);

                }
                else
                    fifth.Add(forth[i]);
            }

            for (int i = 0; i < fifth.Count; i++)
            {
                if (i == 0)
                    final.Add(fifth[i] + " ");

                else if (fifth[i].Contains("-"))
                    final.Add(fifth[i] + " ");
                else if (fifth[i].Contains("#"))
                    final.Add(fifth[i] + " ");
                else
                    final.Add(" +" + fifth[i]);
            }

            sum = string.Join("", final);

            return sum;


        }




        //-----------------------< parsing data >-----------------------------//
        #endregion /Parsing_Data

        #region Database

        private void Load_List()
        {
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

        private void Add_Entry_to_Database()
        {
            //----------------< add_Entry_to_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            string sNew_Search = textBox2.Text;
            string sql_Text = "INSERT INTO tbl_Search ([SearchData]) VALUES(N'" + sNew_Search + "')";

            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            Load_List();
            //< /reload >
            //----------------< /add_Entry_to_Database>--------------//


        }

        private void Search_Add_Entry_to_Database(String s)
        {
            //----------------< Search_add_Entry_to_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            string sNew_Search = s;
            string sql_Text = "INSERT INTO tbl_Search ([SearchData]) VALUES(N'" + sNew_Search + "')";

            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            Load_List();
            //< /reload >
            //----------------< /Search_add_Entry_to_Database>--------------//
        }

        private void Delete_Row_of_Database()
        {
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
            Load_List();
            //< /reload >
            //----------------< /delete_Row_of_Database>--------------//
        }

        private void Update_Entry_in_Database()
        {
            //----------------< update_Entry_in_Database>--------------//
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            DataRowView row = listBox1.SelectedItem as DataRowView;
            string indexS = row["indexS"].ToString();
            string sql_Text = "UPDATE tbl_Search SET [Searchdata] = N'" + textBox2.Text + "' WHERE indexS = " + indexS;

            SqlCommand cmd_Command = new SqlCommand(sql_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();
            //-< Database >

            //< reload >
            Load_List();
            //< /reload >
            //----------------< /update_Entry_in_Database>--------------//
        }



        #endregion /Database

        
    }

}
