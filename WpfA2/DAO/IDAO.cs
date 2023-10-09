using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfA2.Model;

namespace WpfA2.DAO
{
    public interface IDAO
    {
        public List<long> GetDistinctYears();

        public Statistics GetSalesByYear(long year);

        public List<Statistics> GetSalesMonthByMonth(long year);

        public Statistics GetSalesByMonth(string month);

        public Statistics GetSalesByYearAndMonth(long year, string month);

        public bool UpdateStatistics(Statistics oneStatistics);
    }
}
