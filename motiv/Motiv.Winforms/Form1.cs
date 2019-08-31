using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motiv.Data;
using Motiv.Data.Model;
using Newtonsoft.Json;

namespace Motiv.Winforms
{
    public partial class Form1 : Form
    {
        IAuthData _authData { get; set; }
        BackgroundWorker backgroundWorker;
        
        int oneTime;
        int twoTime;
        IBallance ballance;

        public Form1()
        {           
            oneTime = new Random().Next(150, 2000);
            twoTime = new Random().Next(360, 2000);
            
            InitializeComponent();
            _authData = new AuthData();
            ballance = new Ballance();
            ballance.Change += Ballance_Change;



        }       

        void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled)
                (sender as Button).BackColor = Color.Red;
            else if (e.Error != null)
                MessageBox.Show(e.Error.ToString());


            if (e.Result is Subscription)
            {
                var result = e.Result as Subscription;
               

                var dic = new List<KeyValuePair<string, string>>
                {                   
                    new KeyValuePair<string, string>("конец подписки", result.End.Value.ToString("dd.MM.yyyy")),
                    new KeyValuePair<string, string>("осталось времени", result.TimeLeft),
                    new KeyValuePair<string, string>("осталось трафика", result.FreeTraffic),
                    new KeyValuePair<string, string>("норма потребления", result.Median),
                    new KeyValuePair<string, string>("баланс ", result.Ballance),
                    new KeyValuePair<string, string>("тариф",result.Tariff),
                };

                if (result.Options != null)
                {
                    dic.Add(new KeyValuePair<string, string>(_authData.Option.ToLower(), result.Options[0].Title));
                }
                dataGridView1.DataSource = dic;          
              
            }
        }



        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {        
         
            try
            {
                if (e.Cancel)
                {
                    return;
                }              
                e.Result = ballance.GetBallanse(_authData,oneTime,twoTime);
            } catch(Exception error)
            {               
                MessageBox.Show(error.Message);
                Ballance_Change(0);

            } finally
            {
                Invoke(new Action(() =>
                {
                    refreshButton.Enabled = true;
                    refreshButton.BackColor = Color.White;
                }));
            }
        }

        private void Ballance_Change(int state = 0)
        {
            this.Invoke(new Action(() =>
            {
                progressBar.Value = state;
            }));          

        }

        void refreshButton_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            backgroundWorker.RunWorkerAsync();
            (sender as Button).Enabled = false;
            (sender as Button).BackColor = Color.Yellow;
            progressBar.Visible = true;
        }




        void ShowErrorAndCloseForm(string errorMessage)
        {
            var messageResult = MessageBox.Show(errorMessage, "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (messageResult == DialogResult.OK)
            {
                Close();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            if (!System.IO.File.Exists("application.json"))
            {

                ShowErrorAndCloseForm("файл конфигурации не найден");
            }
            var task = Task<bool>.Factory.StartNew(() => Ballance.CheckPing());
            task.Wait();
            if (!task.Result)
            {
                task.Dispose();
                ShowErrorAndCloseForm("проверьте подключение к интернету");
            }
            else
            {
                refreshButton.Visible = true;
                var configuration = System.IO.File.ReadAllText("application.json");
                _authData = JsonConvert.DeserializeObject<AuthData>(configuration);

                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorker.WorkerSupportsCancellation = true;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {            
            var columns = dataGridView1.Columns; ;
            for(var i=0;i<columns.Count;i++)
            {
                columns[i].Width = this.Width / columns.Count;
            }
           
        }
    }
}
