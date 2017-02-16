using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace LoopTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PerformanceResult> results;

        public MainWindow()
        {
            InitializeComponent();

            results = new List<PerformanceResult>
            {
                new PerformanceResult<string>{ Name = "String", CreateData = () => "Hello World" },
                new PerformanceResult<int>{ Name = "Int", CreateData = () => 512 },
                new PerformanceResult<StringBuilder>{ Name = "StringBuilder", CreateData = () => new StringBuilder() },
                new PerformanceResult<DateTime>{ Name = "DateTime", CreateData = () => new DateTime() },
            };

            List1.ItemsSource = results;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Task.Factory.StartNew(() =>
            new Thread(new ThreadStart(()=> 
            {
                Dispatcher.BeginInvoke(new Action(() => RunTestButton.IsEnabled = false));
                foreach (var test in results)
                {
                    test.RunTest();
                }
                Dispatcher.BeginInvoke(new Action(() => RunTestButton.IsEnabled = true));
            })).Start();
        }
    }
}
