using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Moda.Korean.TwitterKoreanProcessorCS;
using System.Data.SqlClient; //SQL SERVER LOCAL DB

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        bool http_complete = false;
        #region Function
        //-----------------------< Function >------------------------//
        public bool IsContainKorean(string s)
        {
            //한글 확인 함수
            char[] charArr = s.ToCharArray();
            foreach (char c in charArr)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    return true;
                }
            }
            return false;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) //1브라우저 처음에 ㄷ
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void WebBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(textBox2.Text != null) { 
                    this.Button6_Click(sender, e);
                }
            }
        } //  텍스트창에서 엔터키 눌렀을때 호출 함수

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string SearchType = comboBox1.SelectedItem.ToString();

                if (SearchType == "Google")
                {
                    Module_Google(textBox2.Text);
                }
                else if (SearchType == "Naver")
                {
                    Module_Naver(textBox2.Text);
                }
            }
        } // 히스토리창 검색에서 엔터 치면

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false & checkBox2.Checked == true & tabControl1.SelectedIndex == 0)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 0;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            }
            else if (checkBox1.Checked == true & checkBox2.Checked == false & tabControl1.SelectedIndex == 1)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 1;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            }
            else if (checkBox1.Checked == true & checkBox2.Checked == false & tabControl1.SelectedIndex == 0)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 0;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            }
            else if (checkBox1.Checked == true & checkBox2.Checked == false & tabControl1.SelectedIndex == 1)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                tableLayoutPanel1.ColumnCount = 2;
                tabControl1.SelectedIndex = 1;
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            }


        }

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

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SearchType = comboBox1.SelectedItem.ToString();
            if (SearchType == "Google")
            {
                Module_Google("");
            }
            else if (SearchType == "Naver") {
                Module_Naver("");
            }
        } //검색 히스토리 바뀌면 바로 적용 시키는 부분

        private string decide_day()
        {
            string SearchPeriod = "";
            string SearchType = comboBox1.SelectedItem.ToString();

            if (SearchType == "Google")
            {
                if (comboBox2.SelectedItem.ToString() == "지난 1일")
                {
                    SearchPeriod = "d";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1주")
                {
                    SearchPeriod = "w";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1개월")
                {
                    SearchPeriod = "m";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1년")
                {
                    SearchPeriod = "y";
                }

            }
            else if (SearchType == "Naver")
            {
                if (comboBox2.SelectedItem.ToString() == "지난 1일")
                {
                    SearchPeriod = "UnG7Owp0J1sssSdPdrZssssstrC-309562&nso=so%3Ar%2Cp%3A1d%2Ca%3Aall";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1주")
                {
                    SearchPeriod = "UnG6jlp0YidssCGqKPVssssssfR-387322&nso=so%3Ar%2Cp%3A1w%2Ca%3Aall";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1개월")
                {
                    SearchPeriod = "UnG6wwp0YiRssnfQhrlssssssE8-281674&nso=so%3Ar%2Cp%3A1m%2Ca%3Aall";
                }
                else if (comboBox2.SelectedItem.ToString() == "지난 1년")
                {
                    SearchPeriod = "UnG61wp0JywssuoTQgGssssstN8-044529&nso=so%3Ar%2Cp%3A1y%2Ca%3Aall";
                }
            }
            return SearchPeriod;
        } //검색기간 계산 후 반환
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

            // 검색엔진 설정 콤보박스
            string[] Searching_Engine_name = { "Google", "Naver" };
            comboBox1.Items.AddRange(Searching_Engine_name);
            comboBox1.DisplayMember = "Google";
            comboBox1.SelectedIndex = 0;

            //날짜 설정 콤보박스
            string[] Searching_day = { "모든 날짜", "지난 1일", "지난 1주", "지난 1개월", "지난 1년" };
            comboBox2.Items.AddRange(Searching_day);
            comboBox2.DisplayMember = "모든 날짜";
            comboBox2.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load_List(); //시작할 때 데이터베이스 연동

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
            string SearchType = comboBox1.SelectedItem.ToString();

            if (SearchType == "Google")
            {
                Module_Google(textBox1.Text);
            }
            else if (SearchType == "Naver") {
                Module_Naver(textBox1.Text);
            }
        } // 체크박스 처리버튼

        private void Module_Google(string data)
        {
            string SearchPeriod = decide_day();
            string searching_data;
            if (checkBox2.Checked) // 히스토리 검색
            {
                searching_data = "";

                if (data != "") { 
                    listBox1.Items.Add(data);
                }

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    searching_data += listBox1.Items[i];
                    searching_data += " ";
                }
                string tmp1 = "https://www.google.com/search?&q=" + StemSample2(searching_data) + "&tbs=qdr:" + SearchPeriod;
                webBrowser1.Navigate(tmp1);

            }
            else if (checkBox1.Checked) // 웹브라우저 두개 띄운 화면
            {
                searching_data = textBox1.Text;
               
                string tmp1 = "https://www.google.com/search?&q=" + StemSample2(searching_data) + "&tbs=qdr:" + SearchPeriod;
                string tmp2 = "https://www.google.com/search?&q=" + searching_data + "&tbs=qdr:" + SearchPeriod;
                webBrowser1.Navigate(tmp1);
                webBrowser2.Navigate(tmp2);
            }

            else // 비교도, 히스토리도 아니라면 > 그냥 브라우저라면
            {
                string tmp = "";

                if (IsContainKorean(data))
                { // 한글을 친거면
                    tmp = "https://www.google.com/search?q=" + data + "&& aqs = chrome..69i57j0j69i61j0j69i61l2.7224j0j4 & sourceid = chrome & ie = UTF - 8&tbs=qdr:" + SearchPeriod;
                    webBrowser1.Navigate(tmp);
                    //if (tableLayoutPanel1.ColumnCount == 2) { }
                }
                else
                {
                    tmp = "https://www.google.com/search?q=" + data + "&& aqs = chrome..69i57j0j69i61j0j69i61l2.7224j0j4 & sourceid = chrome & ie = UTF - 8&tbs=qdr:" + SearchPeriod;
                    webBrowser1.Navigate(tmp);
                }
            }
        }
        private void Module_Naver(string data)
        {
            string SearchPeriod = decide_day();
            string searching_data;

            if (checkBox2.Checked) // 히스토리 검색
            {
                searching_data = "";

                if (data != "")
                {
                    listBox1.Items.Add(data);
                }

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    searching_data += listBox1.Items[i];
                    searching_data += " ";
                }
                

                string tmp1 = "https://search.naver.com/search.naver?sm=top_hty&fbm=0&ie=utf8&query=" + HttpUtility.UrlEncode(StemSample1(searching_data)) + "&tqi=" + SearchPeriod;
                webBrowser1.Navigate(tmp1);

            }
            else if (checkBox1.Checked) // 웹브라우저 두개 띄운 화면
            {
                searching_data = data;
                
                string tmp1 = "https://search.naver.com/search.naver?sm=top_hty&fbm=0&ie=utf8&query=" + HttpUtility.UrlEncode(StemSample1(searching_data)) + "&tqi=" + SearchPeriod;
                string tmp2 = "https://search.naver.com/search.naver?sm=top_hty&fbm=0&ie=utf8&query=" + HttpUtility.UrlEncode(searching_data) + "&tqi=" + SearchPeriod;
                webBrowser1.Navigate(tmp1);
                webBrowser2.Navigate(tmp2);
            }
            else // 비교도, 히스토리도 아니라면 > 그냥 브라우저라면
            {
                string tmp = "";
                if (IsContainKorean(data))
                { // 한글을 친거면
                    tmp = "https://search.naver.com/search.naver?sm=tab_hty.top&where=nexearch&query=" + HttpUtility.UrlEncode(data) + "&tqi=" + SearchPeriod;
                    webBrowser1.Navigate(tmp);
                }
                else
                {
                    tmp = "https://search.naver.com/search.naver?sm=top_hty&fbm=0&ie=utf8&query=" + data + "&tqi=" + SearchPeriod;
                    webBrowser1.Navigate(tmp);
                }
            }
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if(textBox2.Text != "") {
                    listBox1.Items.Add(textBox2.Text);
                }
                string SearchType = comboBox1.SelectedItem.ToString();

                if (SearchType == "Google")
                {
                    Module_Google("");
                }
                else if (SearchType == "Naver")
                {
                    Module_Naver("");
                }
            }
            //Add_Entry_to_Database();
        } // 추가하기

        private void Button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                if(textBox2.Text!= "") { 
                    listBox1.Items[listBox1.SelectedIndex] = textBox2.Text;
                }
                Button6_Click(sender, e);
            }
            //Update_Entry_in_Database();
        } // 변경 하기

        private void Button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            Button6_Click(sender,e);
            //Delete_Row_of_Database();
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
            List<string> seq = new List<string>();
            List<string> plus = new List<string>();
            List<string> sub = new List<string>();
            List<string> output = new List<string>();
            List<string> input = new List<string>();
            List<string> or = new List<string>();

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
                    second.Add(list[i - 1] + "OR" + list[i + 1]);
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
                else if (third[i] == "유튜브")
                    forth.Add("site:www.youtube.com");
                else
                    forth.Add(third[i]);
            }

            for (int i = 0; i < forth.Count; i++)
            {
                if (forth[i] == "해시태그")
                {
                    fifth.RemoveAt(i - 1);
                    fifth.Add("%23" + forth[i - 1]);

                }
                else
                    fifth.Add(forth[i]);
            }

            for (int i = 0; i < fifth.Count; i++)
            {
                if (fifth[i].Contains("-"))
                    sub.Add(fifth[i]);
                else if (fifth[i].Contains("-"))
                    or.Add(fifth[i]);
                else if (fifth[i].Contains("site") || fifth[i].Contains("filetype"))
                    seq.Add(fifth[i]);
                else
                    plus.Add(fifth[i]);

            }

            output.AddRange(plus);
            output.AddRange(or);
            output.AddRange(sub);
            output.AddRange(seq);

            for (int i = 0; i < output.Count; i++)
            {
                if (i == 0)
                    input.Add(output[i]);
                else if (output[i].Contains("-"))
                    input.Add(output[i]);
                else if (fifth[i].Contains("#"))
                    input.Add(output[i]);
                else
                    input.Add("+" + output[i]);
            }

            sum = string.Join("", input);

            return sum;
        } //  구글 파싱

        public string StemSample1(string input_Search_Data)
        {
            var tokens = TwitterKoreanProcessorCS.Tokenize(
                              TwitterKoreanProcessorCS.Normalize(input_Search_Data));
            var stemmedTokens = TwitterKoreanProcessorCS.Stem(tokens);

            List<string> list = new List<string>();
            List<string> second = new List<string>();

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
                    second.Add(list[i - 1] + "|" + list[i + 1]);
                    list.RemoveAt(i + 1);
                }
                else
                    second.Add(list[i]);
            }
            sum = string.Join("", second);
            return sum;
        } // 네이버 파싱



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

        private string Output_Data()
        {
            string result = "";
            string cn_string = Properties.Settings.Default.SearchDataConnectionString;

            //-< Database >
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            string sql_Text = "SELECT Searchdata FROM tbl_Search";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql_Text, cn_connection);
            adapter.Fill(tbl);

            //MessageBox.Show(tbl.ToString());
            return result;
        }






        #endregion /Database

        
    }

}
