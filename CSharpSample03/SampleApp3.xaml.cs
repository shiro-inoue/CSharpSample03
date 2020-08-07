using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpSample03
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadAppSetting();
        }

        // アプリ設定値の読み込み
        private void loadAppSetting()
        {
            this.Left = Properties.Settings.Default.left;
            this.Top = Properties.Settings.Default.top;
            this.Width = Properties.Settings.Default.width;
            this.Height = Properties.Settings.Default.height;
            this.textBoxImagePath.Text = Properties.Settings.Default.imagePath;
        }
 
        // アプリ設定値の保存
        private void saveAppSetting()
        {
            Properties.Settings.Default.left = this.Left;
            Properties.Settings.Default.top = this.Top;
            Properties.Settings.Default.width = this.Width;
            Properties.Settings.Default.height = this.Height;
            Properties.Settings.Default.imagePath = this.textBoxImagePath.Text;
            Properties.Settings.Default.Save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveAppSetting();
        }
    }
}
