using System;
using System.Windows.Forms;
using NAudio.Wave; // 請確保已添加相應的引用
using System.IO; // 請確保已添加相應的引用

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private WaveOutEvent waveOut; // 音效播放器
        private AudioFileReader audioFileReader; // 音效檔讀取器
        private string strSelectTime = ""; // 用來記錄鬧鐘設定時間

        public Form1()
        {
            InitializeComponent();
            comboboxInitialzation();  // 下拉選單初始化
            timerClock.Start();       // 啟動時鐘
            timerAlert.Tick += new EventHandler(timerAlert_Tick); // 添加事件處理程序
        }

        private void comboboxInitialzation()
        {
            // 初始化小時下拉選單的選項
            for (int i = 0; i <= 23; i++)
                cmbHour.Items.Add(i.ToString("D2"));
            cmbHour.SelectedIndex = 0;

            // 初始化分鐘下拉選單的選項
            for (int i = 0; i <= 59; i++)
                cmbMin.Items.Add(i.ToString("D2"));
            cmbMin.SelectedIndex = 0;
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToString("HH:mm:ss"); // 更新時間顯示
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // 更新日期顯示
            txtWeekDay.Text = DateTime.Now.DayOfWeek.ToString(); // 更新星期幾顯示
        }

        private void cmbHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 可添加需要的邏輯
        }

        private void cmbMin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 可添加需要的邏輯
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            // 可添加需要的邏輯
        }

        private void txtWeekDay_TextChanged(object sender, EventArgs e)
        {
            // 可添加需要的邏輯
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {
            // 可添加需要的邏輯
        }

        private void btnSetAlert_Click(object sender, EventArgs e)
        {
            timerAlert.Start(); // 啟動鬧鐘計時器
            btnSetAlert.Enabled = false;
            btnCancelAlert.Enabled = true;
            strSelectTime = cmbHour.SelectedItem.ToString() + ":" + cmbMin.SelectedItem.ToString(); // 擷取小時和分鐘的下拉選單文字，用來設定鬧鐘時間
        }

        private void btnCancelAlert_Click(object sender, EventArgs e)
        {
            // 停止鬧鐘計時器
            timerAlert.Stop();

            // 停止播放聲音
            stopWaveOut();

            // 更新按鈕狀態
            btnSetAlert.Enabled = true;
            btnCancelAlert.Enabled = false;
        }

        private void timerAlert_Tick(object sender, EventArgs e)
        {
            // 判斷現在時間是否已經是鬧鐘設定的時間
            if (strSelectTime == DateTime.Now.ToString("HH:mm"))
            {
                try
                {
                    stopWaveOut(); // 停止之前的播放

                    // 播放聲音檔
                    string audioFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "alert.wav");
                    audioFileReader = new AudioFileReader(audioFilePath);
                    waveOut = new WaveOutEvent();
                    waveOut.Init(audioFileReader);
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("無法播放聲音檔，錯誤資訊: " + ex.Message);
                }
                finally
                {
                    timerAlert.Stop(); // 停止鬧鐘計時器
                }
            }
        }

        private void stopWaveOut()
        {
            // 停止播放聲音
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
