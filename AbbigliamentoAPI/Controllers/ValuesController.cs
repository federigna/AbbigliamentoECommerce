using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AbbigliamentoECommerceBL;
using AbbigliamentoECommerceBL.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AbbigliamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            return "value";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async void SendMail(string pUrlPDF,string pNumOrder,string pIdUser)
        {
            FileStream wPFD = ManagementDocument.CreateOrderDocument(pUrlPDF, pNumOrder, await new CartBL().GetCartByUser(pIdUser),
               await new UserBL().SigIn(pIdUser));
            //invio Mail
            try
            {
                MailManagment.SendEmail(wPFD.Name, wLogUser);
            }
            catch (Exception ex)
            {

                Log.Error("Errore durante l'invio della mail.", ex);

            }
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
