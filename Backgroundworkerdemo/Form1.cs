using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Backgroundworkerdemo
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 本实例演示如何在关闭窗体的时候取消Backgroundworker中正在运行的任务
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Completed");
            this.Close();//throw new NotImplementedException();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            //如果还有任务在处理，取消关闭窗口，在任务处理完毕后再关闭
            if (backgroundWorker1.IsBusy)
            {
                e.Cancel = true;
            }
            else
                base.OnClosing(e);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();   
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //检查是否已经发起取消操作
            while (!backgroundWorker1.CancellationPending)
            {
                Thread.Sleep(1000);
                Console.WriteLine(DateTime.Now);
            }
            //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
