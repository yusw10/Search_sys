using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            //Form1.Designer를 불러서 UI를 구성하게하는 필수적인 친구
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) // 브라우저 처음에 ㄷ
        {
            this.Text = webBrowser1.DocumentText + "-브라우저 샘플입니다";
            toolStripTextBox1.Text = webBrowser1.Document.Url.ToString();
        }

        //뒤로가기
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
            MessageBox.Show("좆같노");
            //webBrowser1.Refresh();
        }

        //홈으로가기.
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        //go 버튼 누르면 그 url로 가기
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "테스트") {
                MessageBox.Show("앙 성공띠");
            }
            webBrowser1.Navigate(toolStripTextBox1.Text);
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //텍스트 창에서 엔터 누름 그거를 go버튼에 연동
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                this.toolStripButton5_Click(sender,e);
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
            
            
            try
            {
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();
                //파이선 프로그램 파일 실행.
                
                var source = engine.CreateScriptSourceFromFile("test.py");
                source.Execute(scope);

                var check= scope.GetVariable<Func<object, object>>("check_sen");
                //webBrowser1.Navigate(check("인스타그램에서 야식음식에서 간장을 제외하고 후라이드는 해쉬태그").ToString());
                MessageBox.Show(check("인스타그램에서 야식음식에서 간장을 제외하고 후라이드는 해쉬태그").ToString());

                /*
                //함수 실행 인자없는거
                var Test = scope.GetVariable<Func<object>>("Testhansuk");
                //MessageBox.Show(Test().ToString());
                //webBrowser1.Navigate(Test().ToString());

                //이번엔 인자 있는거
                var TestK = scope.GetVariable<Func<object,object>>("TestHong");
                MessageBox.Show(TestK("테스트").ToString());
                */
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.ShowDialog();
            

        
        }
    }
}
