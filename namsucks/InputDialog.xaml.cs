using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace namsucks
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {

        private string suffix = "";

        private string createDate = "";

        public string filePath
        {
            set
            {
                suffix = System.IO.Path.GetExtension(value);
                createDate = new System.IO.FileInfo(value).LastWriteTime.ToString("yyyyMMdd");
                string name = System.IO.Path.GetFileNameWithoutExtension(value);
                fileName = createDate + "：" + "[" + name + "]" + suffix;
                ResponseTextBox.Text = name;
            }
        }
        
        public string fileName
        {
            set
            {
                FileNameTextBlock.Text = value;
            }
            get
            {
                return FileNameTextBlock.Text;
            }
        }

        public InputDialog()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Focus();
            ResponseTextBox.Focus();
            ResponseTextBox.SelectAll();
        }

        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ResponseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Clicked(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                fileName = "";
                Close();
            }
        }

        private void ResponseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fileName = createDate + "：";
            if (ResponseTextBox.Text.Split(' ').Length == 1 || !Properties.Settings.Default.UseTag)
            {
                fileName += ResponseTextBox.Text + suffix;
                return;
            }
            foreach (string tag in ResponseTextBox.Text.Split(' '))
            {
                fileName += Properties.Settings.Default.TagForm[0] + tag + Properties.Settings.Default.TagForm[1];
            }
            fileName += suffix;
        }
    }
}
