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
        public async Task<Dictionary<string, string>> BestSellingProduct(string pCat, DateTime pDateStart, DateTime pDateEnd)
        {
            StatisticsDB wDB = new StatisticsDB();
           return await wDB.BestSellingProduct(pCat, pDateStart,pDateEnd);

        }
    }
}
