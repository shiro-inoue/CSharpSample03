using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
//using System.Windows.Forms; MessageBox出力時に明示する必要が出るらしいのでコメントアウト

namespace CSharpSample03
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WpfApp4View : Window
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        //private List<WpfApp4ViewModel> viewModel = new List<WpfApp4ViewModel>();

        public WpfApp4View()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAppSetting();
            RedisplayThumbnail();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveAppSetting();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog();
        }

        // アプリ設定値の読み込み
        private void LoadAppSetting()
        {
            Left = Properties.Settings.Default.left;
            Top = Properties.Settings.Default.top;
            Width = (Properties.Settings.Default.width == 0) ? 800 : Properties.Settings.Default.width;
            Height = (Properties.Settings.Default.height == 0) ? 600 : Properties.Settings.Default.height;
            textBoxImagePath.Text = Properties.Settings.Default.imagePath;
            // ウインドウ最大化
            WindowState = Properties.Settings.Default.maximized;
        }

        // アプリ設定値の保存
        private void SaveAppSetting()
        {
            Properties.Settings.Default.left = Left;
            Properties.Settings.Default.top = Top;
            Properties.Settings.Default.width = Width;
            Properties.Settings.Default.height = Height;
            Properties.Settings.Default.imagePath = textBoxImagePath.Text;
            // ウインドウ最大化
            Properties.Settings.Default.maximized = WindowState;

            Properties.Settings.Default.Save();
        }

        // フォルダ選択ダイアログの生成
        private void OpenFolderDialog()
        {
            var fo = new System.Windows.Forms.FolderBrowserDialog();
            fo.RootFolder = Environment.SpecialFolder.Desktop;

            if (fo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxImagePath.Text = GetImageInfo(fo.SelectedPath);
            }
        }

        // 保存されたフォルダのサムネイルを表示
        private void RedisplayThumbnail()
        {
            if (File.Exists(textBoxImagePath.Text))
            {
                _ = GetImageInfo(Path.GetDirectoryName(textBoxImagePath.Text));
            }
        }

        // 画像ファイル情報を取得
        private string GetImageInfo(string path)
        {
            string[] files = Directory.GetFiles(path);
            var images = files.Where(f => ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant()))
                              .Select(f => new WpfApp4ViewModel(){fileFullPath = f, fileName = Path.GetFileName(f)});

            DataContext = images;
            return images.Count != 0? images[0].fileFullPath: String.Empty;
        }
    }
}
