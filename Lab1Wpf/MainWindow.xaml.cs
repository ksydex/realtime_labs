using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Lab1Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetComboBox();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var seconds = int.Parse(SecondsTextBox.Text);
            var processList = Process.GetProcesses().Where(x => x.ProcessName.Contains(ProcessList.Text)).ToList();

            bool ShouldKill(Process process)
                => (DateTime.Now - process.StartTime) > TimeSpan.FromSeconds(seconds);

            foreach (var process in processList.Where(ShouldKill)) process.Kill();
        }
        
        private void SetComboBox()
        {
            ProcessList.ItemsSource = Process.GetProcesses().Select(x => x.ProcessName);
        }
    }
}