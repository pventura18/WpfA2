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
using WpfA2.DAO;
using WpfA2.Model;

namespace WpfA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDAO manager = DAOFactory.CreateXMLManager();
        public MainWindow()
        {
            InitializeComponent();

            //ComboBox years
            foreach(long year in manager.GetDistinctYears())
            {
                cmbYears.Items.Add(year);
            }
            cmbYears.Items.Add("ALL");

            //ComboBox months
            string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC", "ALL" };
            foreach(string month in months)
            {
                cmbMonths.Items.Add(month);
            }
        }

        private void btnGetSalesMonthByMonth_Click(object sender, RoutedEventArgs e)
        {
            dgvDataOfAYear.ItemsSource = manager.GetSalesMonthByMonth(Convert.ToInt32(cmbYears.Text));
        }

        private void btnGetSalesByYear_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistic = manager.GetSalesByYear(Convert.ToInt32(cmbYears.Text));

            txtboxYear.Text = statistic.Year.ToString();
            txtboxMonth.Text = statistic.Month.ToString();
            txtboxtotalNews.Text = statistic.TotalNews.ToString();
            txtboxtotalUsed.Text = statistic.TotalUsed.ToString();
            txtboxamountNews.Text = statistic.AmountNews.ToString();
            txtboxamountUsed.Text = statistic.AmountUsed.ToString();

        }

        private void btnGetSalesByMonth_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistic = manager.GetSalesByMonth(cmbMonths.Text);

            txtboxYear.Text = statistic.Year.ToString();
            txtboxMonth.Text = statistic.Month.ToString();
            txtboxtotalNews.Text = statistic.TotalNews.ToString();
            txtboxtotalUsed.Text = statistic.TotalUsed.ToString();
            txtboxamountNews.Text = statistic.AmountNews.ToString();
            txtboxamountUsed.Text = statistic.AmountUsed.ToString();
        }

        private void btnGetSalesByYearAndMonth_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistic = manager.GetSalesByYearAndMonth(Convert.ToInt64(cmbYears.Text), cmbMonths.Text);

            txtboxYear.Text = statistic.Year.ToString();
            txtboxMonth.Text = statistic.Month.ToString();
            txtboxtotalNews.Text = statistic.TotalNews.ToString();
            txtboxtotalUsed.Text = statistic.TotalUsed.ToString();
            txtboxamountNews.Text = statistic.AmountNews.ToString();
            txtboxamountUsed.Text = statistic.AmountUsed.ToString();
        }

        private void btnUpdateStatistics_Click(object sender, RoutedEventArgs e)
        {
            frmUpdateStatistics updatedWindow = new frmUpdateStatistics(manager, Convert.ToInt64(cmbYears.Text), cmbMonths.Text, Convert.ToInt64(txtboxtotalNews.Text), Convert.ToInt64(txtboxtotalUsed.Text), Convert.ToInt64(txtboxamountNews.Text), Convert.ToInt64(txtboxamountUsed.Text));
            updatedWindow.ShowDialog();
        }
    }
}
