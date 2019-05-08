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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static Boolean SenetenceSearch = false;
        #region Function
        //-----------------------< Function >------------//
        public bool isContainKorean(string s) {
            //한글 확인 함수
            char[] charArr = s.ToCharArray();
            foreach (char c in charArr) {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter) {
                    return true;
                }
            }
            return false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) // 브라우저 처음에 ㄷ
        {
            //브라우저가 표시완료하면 나오게 하는 함수
            this.Text = webBrowser1.DocumentText + "-브라우저 샘플입니다";
            toolStripTextBox1.Text = webBrowser1.Document.Url.ToString();
        }
        //-----------------------< Function >------------//
        #endregion /Function

        #region Form
        //-----------------------< Form >-----------------//
        public Form1()
        {
            //Form1.Designer를 불러서 UI를 구성하게하는 필수적인 친구
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //-----------------------< Form >-----------------//
        #endregion /Form

      
        #region toolbar
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        //앞으로가기
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        //새로고침
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            webBrowser1.Refresh();
        }

        //홈으로가기.
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        //go 버튼 누르면 그 url로 가기
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) // 문장검색 기능 키면 파싱해서 검색보내버리기
            {
                string tmp = "https://www.google.com/search?&q=" + StemSample2(toolStripTextBox1.Text);
                webBrowser1.Navigate(tmp);
                //MessageBox.Show(StemSample2(toolStripTextBox1.Text));

            }
            else { // 문장검색이 아니라면
                if (isContainKorean(toolStripTextBox1.Text))
                { // 한글을 친거면
                    string tmp = " https://www.google.com/search?q=" + toolStripTextBox1.Text + "&& aqs = chrome..69i57j0j69i61j0j69i61l2.7224j0j4 & sourceid = chrome & ie = UTF - 8";
                    webBrowser1.Navigate(tmp);
                }
                else
                {

                    webBrowser1.Navigate(toolStripTextBox1.Text);
                }
            }
        }
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.toolStripButton5_Click(sender, e);
            }
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string tmp = " 테스트용 버튼";
            MessageBox.Show(tmp);
        }
        #endregion /toolbar

        #region Button
        //-----------------------< region button >------------//
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.ShowDialog();      
        }

        //-----------------------< /region button >------------//
        #endregion /Button

        #region Parsing_Data
        //-----------------------< parsing data >-----------------//
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
        //-----------------------< parsing data >-----------------//
        #endregion /Parsing_Data

        
    }
}
