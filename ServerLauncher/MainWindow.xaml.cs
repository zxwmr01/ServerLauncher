using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ServerLauncher
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
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

        //启动服务器进程
        private static void StartTSLServer()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "TslGame.exe",
                    Arguments = "/Game/Maps/Erangel/Erangel_Main?listen?game=/Game/Blueprints/TSLGameMode.TSLGameMode_C -nullrhi -nosound -AllowJoinAnyMatchState -Server -port=8888 -NoVerifyGC -NoEAC -NoBattleEye",
                    UseShellExecute = true,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动 TslGame Server：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //结束服务器进程
        private static void KillTSLProcess()
        {
           try
           {
              // 获取所有名为 "TslGame" 的进程
              Process[] processes = Process.GetProcessesByName("TslGame");

                   if (processes.Length > 0)
                    {
                        foreach (var process in processes)
                        {
                            process.Kill(); // 强制结束进程
                            process.WaitForExit(); // 等待进程完全退出
                        }

                        MessageBox.Show("TslGame Server 已成功关闭！", "操作成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("TslGame Server 没有在运行！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法结束 TslGame Server: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        //启动游戏进程
        private static void StartTSLGame()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "TslGame.exe",
                    Arguments = "127.0.0.1:8888 -NoVerifyGC -NoEAC -NoBattleEye",
                    UseShellExecute = true,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动 TslGame：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //监测 TslGame Server 是否正在运行（自动逻辑）
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
                // 如果进程存在，更新界面为“运行”
                StatusText.Text = "运行";
                StatusText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                // 如果进程不存在，更新界面为“未运行”
                StatusText.Text = "未运行";
                StatusText.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        //界面上的按钮事件
        //启动 TslGame 服务器
        private async void Button_StartTSLServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 启动TslGame.exe
                MessageBoxResult result = MessageBox.Show(
                    "你确定要这么做吗？",
                    "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await Task.Delay(3000);
                    StartTSLServer();
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动 TslGame Server: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    //结束 TslGame 服务器
      private void Button_KillTSLServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(
                "你确定要这么做吗？",
                "提示",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                { 
                    KillTSLProcess();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法结束 TslGame Server: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //启动 TslGame
        private void Button_Start_TslGame(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(
                "你确定要这么做吗？",
                "提示",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StartTSLGame();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动 TslGame {ex.Message}", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //退出应用
        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //关于我们
        private void Button_OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            // 创建新窗口的实例
            Window2 newWindow = new Window2();
            // 显示新窗口
            newWindow.Show();
        }
    }
}