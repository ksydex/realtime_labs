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
using Lab3CLientWpf;

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

            var thread = new Thread(Loop);
            thread.Start();
        }

        public void Loop()
        {
            var semaphore = new Semaphore(0, 2, Connection.SemaphoreName);
            while (true)
            {
                var v = semaphore.WaitOne(500);
                if (v) OnChanged();
            }
        }

        void OnChanged()
        {
            Dispatcher.Invoke(new Action<string>(value => Log.Text = value), Connection.Read());
        }
    }
}