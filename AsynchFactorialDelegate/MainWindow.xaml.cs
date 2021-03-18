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
using Common;

namespace AsynchFactorialDelegate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate ulong PerformCalculation(ulong number);
        PerformCalculation pC = new PerformCalculation(MathOps.Factorial);
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            ulong arg = ulong.Parse(txtArg.Text);
            lblStatus.Content = "Calculating...";
            pC.BeginInvoke(arg, FactorialCompleted, null);
        }

        void FactorialCompleted(IAsyncResult asynchResult)
        {
            Func<ulong, ulong> target = (Func<ulong, ulong>)asynchResult.AsyncState;
            ulong result = target.EndInvoke(asynchResult);
            Dispatcher.BeginInvoke(new Action<ulong>(UpdateResult), result);
            Dispatcher.BeginInvoke(new Action<string>(UpdateStatus), "Completed");
        }

        void UpdateResult(ulong result)
        {
            lblResult.Content = result;
        }

        void UpdateStatus(string status)
        {
            lblStatus.Content = status;
        }
    }
}
