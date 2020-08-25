﻿using System;
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
            LoadAppSetting();
            // 保存されたフォルダのサムネイルを表示
            if (File.Exists(textBoxImagePath.Text))
            {
                _ = GetImageInfo(GetFolderPath(textBoxImagePath.Text));
            }
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
            FolderBrowserDialog fo = new FolderBrowserDialog();
            fo.RootFolder = Environment.SpecialFolder.Desktop;

            if (fo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxImagePath.Text = GetImageInfo(fo.SelectedPath);
            }
        }

        private string GetImageInfo(string path)
        {
            string firstDrawImagePath = String.Empty;
            string[] files = Directory.GetFiles(path);
            var imageInfo = new List<ImageInfo>();
            foreach (var f in files)
            {
                // 画像ファイルを対象とする
                if (ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant()))
                {
                    string name = GetFileName(f.ToString());
                    imageInfo.Add(new ImageInfo { fileFullPath = f, fileName = name });
                    // フォルダ指定時の初期表示する画像ファイルの設定
                    if (string.IsNullOrEmpty(firstDrawImagePath))
                    {
                        firstDrawImagePath = f.ToString();
                    }
                }
            }
            DataContext = imageInfo;
            return firstDrawImagePath;
        }

        private string GetFolderPath(string filePath)
        {
            return filePath.Substring(0, filePath.LastIndexOf(@"\"));
        }

        private string GetFileName(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf(@"\") + 1);
        }
    }

    class ImageInfo
    {
        public string fileFullPath { get; set; }
        public string fileName { get; set; }
    }
}
