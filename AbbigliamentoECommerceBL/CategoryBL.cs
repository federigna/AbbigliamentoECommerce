using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceBL
{
    public class CategoryBL
    {
        public async Task<List<Category>> GetCategory(string pNomeRaccolta)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            List<Category> wwResult = await wDB.GetCategory(pNomeRaccolta);
            return wwResult;
        }
    }
}
