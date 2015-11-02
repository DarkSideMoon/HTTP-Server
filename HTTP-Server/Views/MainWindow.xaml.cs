using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Threading;
using HttpServer.Library;

namespace HttpServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server _server;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += delegate { this.Inialize(); };
        }

        private void Inialize()
        {
            listHosts.Items.Add("127.0.0.1");
        }

        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            int maxThreadsCount = Environment.ProcessorCount * 4;
            ThreadPool.SetMaxThreads(maxThreadsCount, maxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);

            Thread thread = new Thread(new ParameterizedThreadStart(StartServer));
            thread.Start(80);
        }

        private void StartServer(object port)
        {
            this._server = new Server((int)port);
        }

        private void listHosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = listHosts.SelectedValue;
            System.Diagnostics.Process.Start((string)"http://" + item);
        }
    }
}
