using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HackThe360Chrome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr;
            StreamWriter sw;
            FileStream fo;
            string rs="";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "") return;
            try {
                fo = new FileStream(openFileDialog1.FileName,FileMode.OpenOrCreate,FileAccess.ReadWrite);
                
                sr = new StreamReader(fo, Encoding.ASCII);
                sw = new StreamWriter(fo,Encoding.ASCII);
            }
            catch (IOException err)
            {
                MessageBox.Show(err.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                while(!sr.EndOfStream)
                {
                    Application.DoEvents();
                    rs = sr.ReadLine();
                    toolStripStatusLabel1.Text = "Searching:" + sr.BaseStream.Position.ToString() + "/" + sr.BaseStream.Length;
                    if (rs.IndexOf("haosou.com/s?q")!=-1)
                    {
                        sw.BaseStream.Position = sr.BaseStream.Position - rs.Length;
                        sw.Write(rs.Replace("haosou.com/s?q", "baidu.com/s?wd"));
                        sw.Flush();
                        fo.Flush();
                        sw.Close();
                        toolStripStatusLabel1.Text = "已完成修改!";
                        MessageBox.Show("Cracked!", "patch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false;
                        return;
                    }
                    
                }
                toolStripStatusLabel1.Text = "所选文件不对或该文件已被补丁!";
                MessageBox.Show("Failed!", "patch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (IOException err)
            {
                toolStripStatusLabel1.Text = err.Message;
                MessageBox.Show(err.Message,"错误" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sw.Close();
        }
    }
}
