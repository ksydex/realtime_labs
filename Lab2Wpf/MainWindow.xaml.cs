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

namespace Lab2Wpf
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

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(_isLoaded) MessageBox.Show("Изменение размера совершено");
        }

        private void MainWindow_OnLocationChanged(object? sender, EventArgs e)
        {
            if(_isLoaded) MessageBox.Show("Перемещение совершено");
        }

        private bool _isLoaded = false;

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
        }
    }
}