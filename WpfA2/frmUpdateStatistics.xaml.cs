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
using System.Windows.Shapes;
using WpfA2.DAO;

namespace WpfA2
{
    /// <summary>
    /// Lógica de interacción para frmUpdateStatistics.xaml
    /// </summary>
    public partial class frmUpdateStatistics : Window
    {
        public frmUpdateStatistics(IDAO manager, long year, string month, long totalNews, long totalUsed, long amountNews, long amountUsed)
        {
            manager.UpdateStatistics(new Model.Statistics(year, month, totalNews, totalUsed, amountNews, amountUsed));
            InitializeComponent();
            txtboxYear.Text = year.ToString();
            txtboxMonth.Text = month;
            txtboxtotalNews.Text = totalNews.ToString();
            txtboxtotalUsed.Text = totalUsed.ToString();
            txtboxamountNews.Text = amountNews.ToString();
            txtboxamountUsed.Text = amountUsed.ToString();
        }
    }
}
