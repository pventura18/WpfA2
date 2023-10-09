using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfA2.Model
{
    public class Statistics
    {
        public long Year { get; set; }
        public string Month { get; set; }
        public long TotalNews { get; set; }

        public long TotalUsed { get; set; }
        public long AmountNews { get; set; }
        public long AmountUsed { get; set; }

        public Statistics(long year, string month, long totalNews, long totalUsed, long amountNews, long amountUsed)
        {
            this.Year = year;
            this.Month = month;
            this.TotalNews = totalNews;
            this.TotalUsed = totalUsed;
            this.AmountNews = amountNews;
            this.AmountUsed = amountUsed;
        }
    }
}
