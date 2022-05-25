using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Lab3CLientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string ClientName;
        
        public MainWindow()
        {
            InitializeComponent();
            ClientName = Process.GetProcessesByName(nameof(Lab3CLientWpf)).Length + "";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var v = ValueBox.Text;
            if (v == "") return;
            
            Connection.Write(ClientName + ": " + v);
            var sem = Semaphore.OpenExisting(Connection.SemaphoreName);
            sem.Release();
            
            ValueBox.Text = "";
        }
    }
}