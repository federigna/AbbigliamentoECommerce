using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceBL
{
    public class StatisticBL
    {
        public async Task<Dictionary<string, string>> BestSellingProduct(string pCat, DateTime pDateStart, DateTime pDateEnd, string pFieldKey)
        {
            StatisticsDB wDB = new StatisticsDB();
            if (!string.IsNullOrEmpty(pCat) && pDateEnd != DateTime.MinValue &&
                pDateStart != DateTime.MinValue)
            {
                return await wDB.BestSellingProduct(pCat, pDateStart, pDateEnd, pFieldKey);
            }
            return new Dictionary<string, string>();
        }
        public async Task<Dictionary<string, string>> UserRegistred(DateTime pDateStart, DateTime pDateEnd)
        {
            StatisticsDB wDB = new StatisticsDB();
            if ( pDateEnd != DateTime.MinValue &&
               pDateStart != DateTime.MinValue)
            {
                return await wDB.UserRegistred(pDateStart, pDateEnd);
            }
            return new Dictionary<string, string>();
        }
    }
}
