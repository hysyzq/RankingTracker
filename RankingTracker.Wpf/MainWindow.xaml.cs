using log4net;
using RankingTracker.Connector;
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

namespace RankingTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                btnSearch.IsEnabled = false;
                spinner.Visibility = Visibility.Visible;

                var apiConnector = new ApiConnector();

                var result = apiConnector.TrackRanking(txtSearchText.Text, txtSearchUrl.Text);

                spinner.Visibility = Visibility.Hidden;
                txtResult.Content = result;
                btnSearch.IsEnabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
