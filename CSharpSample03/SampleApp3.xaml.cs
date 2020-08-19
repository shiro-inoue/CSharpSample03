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
        }

        // アプリ設定値の読み込み
        private void loadAppSetting()
        {
            Left = Properties.Settings.Default.left;
            Top = Properties.Settings.Default.top;
            Width = (Properties.Settings.Default.width == 0) ? 800 : Properties.Settings.Default.width;
            Height = (Properties.Settings.Default.height == 0) ? 600 : Properties.Settings.Default.height;
            textBoxImagePath.Text = Properties.Settings.Default.imagePath;
        }
 
        // アプリ設定値の保存
        private void saveAppSetting()
        {
            Properties.Settings.Default.left = Left;
            Properties.Settings.Default.top = Top;
            Properties.Settings.Default.width = Width;
            Properties.Settings.Default.height = Height;
            Properties.Settings.Default.imagePath = textBoxImagePath.Text;
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadAppSetting();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveAppSetting();
        }

    }
}
