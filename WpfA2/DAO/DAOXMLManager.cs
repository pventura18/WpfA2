using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfA2.Model;
using System.Xml.XPath;

namespace WpfA2.DAO
{
    public class DAOXMLManager : IDAO
    {
        public const string FILENAME = "CarsSoldInMaryland.xml";
        XDocument xDocCarsSold = null;

        public DAOXMLManager()
        {
            xDocCarsSold = XDocument.Load(FILENAME);
        }
        public List<long> GetDistinctYears()
        {
            HashSet<long> years = new HashSet<long>();
            String xpathQuery = "/response/row/row";
            var selectNodes = xDocCarsSold.XPathSelectElements(xpathQuery);
            foreach (XElement element in selectNodes)
            {
                long year = Convert.ToInt64(element.Element("year").Value);
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }
            return years.ToList();
        }

        public Statistics GetSalesByYear(long year)
        {
            if (!GetDistinctYears().Contains(year)) throw new Exception("This year doesn't exist!");
            String xpathQuery = "/response/row/row[year=" + year + "]";
            var selectNodes = xDocCarsSold.XPathSelectElements(xpathQuery);
            long totalNews = 0;
            long totalUsed = 0;
            long amountNews = 0;
            long amountUsed = 0;
            foreach (XElement element in selectNodes)
            {
                totalNews += Convert.ToInt64(element.Element("new").Value);
                totalUsed += Convert.ToInt64(element.Element("used").Value);
                amountNews += Convert.ToInt64(element.Element("total_sales_new").Value);
                amountUsed += Convert.ToInt64(element.Element("total_sales_used").Value);
            }

            return new Statistics(year, "ALL", totalNews, totalUsed, amountNews, amountUsed);
        }

        public List<Statistics> GetSalesMonthByMonth(long year)
        {
            if (!GetDistinctYears().Contains(year)) throw new Exception("This year doesn't exist!");
            String xpathQuery = "/response/row/row[year=" + year + "]";
            var selectNodes = xDocCarsSold.XPathSelectElements(xpathQuery);
            List<Statistics> statistitcsMonthByMonth = new List<Statistics>();
            foreach (XElement element in selectNodes)
            {
                string month = element.Element("month").Value;
                long totalNews = Convert.ToInt64(element.Element("new").Value);
                long totalUsed = Convert.ToInt64(element.Element("used").Value);
                long amountNews = Convert.ToInt64(element.Element("total_sales_new").Value);
                long amountUsed = Convert.ToInt64(element.Element("total_sales_used").Value);
                statistitcsMonthByMonth.Add(new Statistics(year, month, totalNews, totalUsed, amountNews, amountUsed));
            }

            return statistitcsMonthByMonth;
        }


        public Statistics GetSalesByMonth(string month)
        {
            String xpathQuery = "/response/row/row[month='" + month + "']";
            var selectNodes = xDocCarsSold.XPathSelectElements(xpathQuery);

            long cont = 0;
            long totalNews = 0;
            long totalUsed = 0;
            long amountNews = 0;
            long amountUsed = 0;
            foreach (XElement element in selectNodes)
            {
                totalNews += Convert.ToInt64(element.Element("new").Value);
                totalUsed += Convert.ToInt64(element.Element("used").Value);
                amountNews += Convert.ToInt64(element.Element("total_sales_new").Value);
                amountUsed += Convert.ToInt64(element.Element("total_sales_used").Value);
                cont++;
            }
            if (cont == 0) throw new Exception("This month doesn't exist!");

            return new Statistics(-1, month, totalNews, totalUsed, amountNews, amountUsed);
        }


        public Statistics GetSalesByYearAndMonth(long year, string month)
        {
            String xpathQuery = "/response/row/row[month='" + month + "' and year=" + year + "]";
            XElement element = xDocCarsSold.XPathSelectElement(xpathQuery);
            long totalNews;
            long totalUsed;
            long amountNews;
            long amountUsed;
            if (element.HasElements)
            {
                totalNews = Convert.ToInt64(element.Element("new").Value);
                totalUsed = Convert.ToInt64(element.Element("used").Value);
                amountNews = Convert.ToInt64(element.Element("total_sales_new").Value);
                amountUsed = Convert.ToInt64(element.Element("total_sales_used").Value);
            }
            else throw new Exception("This year or month don't exist!");
            return new Statistics(year, month, totalNews, totalUsed, amountNews, amountUsed);
        }


        public bool UpdateStatistics(Statistics oneStatistics)
        {

            bool found;
            string xpathQuery = "/response/row/row[month='" + oneStatistics.Month + "' and year=" + oneStatistics.Year.ToString() + "]";
            XElement selectedNodes = xDocCarsSold.XPathSelectElement(xpathQuery);
            if (selectedNodes == null) found = false;
            else
            {
                selectedNodes.Element("year").Value = oneStatistics.Year.ToString();
                selectedNodes.Element("total_sales_new").Value = oneStatistics.TotalNews.ToString();
                selectedNodes.Element("total_sales_used").Value = oneStatistics.TotalUsed.ToString();
                selectedNodes.Element("used").Value = oneStatistics.AmountUsed.ToString();
                selectedNodes.Element("new").Value = oneStatistics.AmountNews.ToString();
                selectedNodes.Element("month").Value = oneStatistics.Month.ToString();

                found = true;
            }
            

            xDocCarsSold.Save("updateStatisic.xml");
            return found;

        }
    }
}
