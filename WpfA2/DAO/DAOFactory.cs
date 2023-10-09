using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfA2.DAO
{
    public class DAOFactory
    {
        public static IDAO CreateXMLManager()
        {
            return new DAOXMLManager();
        }
    }
}
