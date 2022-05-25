using System;
using System.Collections.Generic;
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

namespace Lab3ServerWpf
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var s = new Semaphore(0, 3, "srv");

            var count = 3;
            AppendString("asdasdasdas");
            
            while (count > 0)
            {
                s.WaitOne();  // ожидаем, когда освободиться место
            
                AppendString($"{Thread.CurrentThread.Name} входит в библиотеку");
            
                AppendString($"{Thread.CurrentThread.Name} читает");
                Thread.Sleep(1000);
            
                AppendString($"{Thread.CurrentThread.Name} покидает библиотеку");
            
                s.Release();  // освобождаем место
            
                count--;
                Thread.Sleep(1000);
            }
        }

        private void AppendString(string str)
            => Log.Text += str + "\n";
    }
}