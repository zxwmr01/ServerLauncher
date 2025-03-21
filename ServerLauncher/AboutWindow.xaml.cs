using System.Diagnostics;
using System.Windows;

namespace ServerLauncher
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        //加入 Discord 按钮
        private void Button_JoinDC_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://discord.gg/yrcaukyEcw";
            try
            {
                // 使用默认浏览器打开链接
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // 如果系统中没有默认浏览器，会抛出异常
                MessageBox.Show($"无法打开链接：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //加入 QQ 群 按钮
        private void Button_JoinQQ_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://qm.qq.com/q/q8dzw0E8Xm";
            try
            {
                // 使用默认浏览器打开链接
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // 如果系统中没有默认浏览器，会抛出异常
                MessageBox.Show($"无法打开链接：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}