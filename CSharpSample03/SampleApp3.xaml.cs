using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace CSharpSample03
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadAppSetting();
            // 保存されたフォルダのサムネイルを表示
            if (File.Exists(textBoxImagePath.Text))
            {
                string firstDrawImagePath = String.Empty;
                getThumbnail(textBoxImagePath.Text.Substring(0, textBoxImagePath.Text.LastIndexOf(@"\")), ref firstDrawImagePath);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveAppSetting();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            openFolderDialog();
        }

        // アプリ設定値の読み込み
        private void loadAppSetting()
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
        private void saveAppSetting()
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
        private void openFolderDialog()
        {
            FolderBrowserDialog fo = new FolderBrowserDialog();
            fo.RootFolder = Environment.SpecialFolder.Desktop;

            if (fo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string firstDrawImagePath = String.Empty;
                getThumbnail(fo.SelectedPath, ref firstDrawImagePath);
                textBoxImagePath.Text = firstDrawImagePath;
            }
        }

        private void getThumbnail(string path, ref string firstDrawImagePath)
        {
            string[] files = Directory.GetFiles(path);
            var images = new List<Image>();
            foreach (var f in files)
            {
                // 画像ファイルを対象とする
                if (ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant()))
                {
                    var name = f.ToString().Substring(f.ToString().LastIndexOf(@"\") + 1);
                    images.Add(new Image { filePath = f, fileName = name });
                    // フォルダ指定時の初期表示する画像ファイルの設定
                    if (string.IsNullOrEmpty(firstDrawImagePath))
                    {
                        firstDrawImagePath = f.ToString();
                    }
                }
            }
            DataContext = images;
        }
    }

    class Image
    {
        public string filePath { get; set; }
        public string fileName { get; set; }
    }
}
