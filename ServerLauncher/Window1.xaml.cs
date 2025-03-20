using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ServerLauncher
{
    public partial class NewWindow : Window
    {
        private DispatcherTimer timer;

        public NewWindow()
        {
            InitializeComponent();

            // 初始化定时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // 每 5 秒检测一次
            timer.Tick += Timer_Tick;
            timer.Start();

            // 初始检测
            CheckTslGameProcess();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 定时检测进程状态
            CheckTslGameProcess();
        }

        private void CheckTslGameProcess()
        {
            // 获取所有名为 "TslGame" 的进程
            Process[] processes = Process.GetProcessesByName("TslGame");

            if (processes.Length > 0)
            {
                // 如果进程存在，更新界面为“运行”，并设置为绿色
                StatusText.Text = "运行";
                StatusText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                // 如果进程不存在，更新界面为“未运行”，并设置为红色
                StatusText.Text = "未运行";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}