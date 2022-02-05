using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace namsucks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DropHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files) {
                    Debug.WriteLine(file);
                    var dialog = new InputDialog();
                    dialog.filePath = file;
                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            string targetPath = "";
                            if (!System.IO.Directory.Exists(SaveDirTextBox.Text))
                            {
                                targetPath = System.IO.Path.Join(System.IO.Path.GetDirectoryName(file), dialog.fileName);
                            }
                            else
                            {
                                targetPath = System.IO.Path.Join(SaveDirTextBox.Text, dialog.fileName);
                            }
                            System.IO.File.Move(file, targetPath);
                            MessageBox.Show(file + "\n=>" + targetPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Move File Failed. \n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
