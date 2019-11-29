using SOIT.Data;
using SOIT.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SOIT.Controllers
{
    public class ExchangeController : ApiController
    {
        private SOITEntities db;
        public ExchangeController()
        {
            db = new SOITEntities();
        }
        [HttpGet]
        public HttpResponseMessage GetOrganizationInformation()
        {
            try
            {
                var detail = new
                {
                    Name = "Civil",
                    Address = "Kathmandu"
                };

                return Request.CreateResponse(HttpStatusCode.OK, detail, new JsonMediaTypeFormatter(), "application/json");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Data Conflict occured", new JsonMediaTypeFormatter(), "application/json");
                //throw;
            }

        }

        [HttpGet]     
        public HttpResponseMessage GetOutputResult()
        {
            string output1 = @"AUD1000000010100//1,
                    CATEGORY=10000:1:1,
                    ACCOUNT.TITLE.1=AUSTRALIAN DOLLER:1:1,
                    ACCOUNT.TITLE.2=AUSTRALIAN DOLLER:1:1,
                    SHORT.TITLE=AUSTRALIAN DOLLER:1:1,
                    MNEMONIC=JEEVANTEST:1:1,
                    POSITION.TYPE=TR:1:1,
                    CURRENCY=AUD:";

            List<string> splittedOutput = output1.Split(',').ToList();

            string[] accno = splittedOutput.First().Replace("//","_").Split('_');
            string AccountNo = accno[1]=="1" ? accno[0]:"";
            string AccountNo_3digit = AccountNo.Substring(AccountNo.Length - 3, 3);
            string finalAccountNo = "NP0010" + AccountNo_3digit;
            
            string AccountTitleObj = splittedOutput.Where(a => a.Contains("TITLE")).FirstOrDefault();
            string AccountName = AccountTitleObj.Split('=')[1];
            //string insertQuery = @"Insert into tableA(AccountNo,AccountName) values('"+AccountNo+"','"+AccountName+"')";
            string insertQuery = $@"Insert into PendinAccounts(AccountNo,AccountName) values('{AccountNo}','{AccountName}')";
            //string insertQuery = string.Format(@"Insert into tableA(AccountNo,AccountName) values('{0}','{1}')",AccountNo,AccountName);
            //string insertQuery = db.Database.SqlQuery<string>("AddPendingAccountInfo", new { AccountNo, AccountName }).FirstOrDefault();
            
            db.Database.ExecuteSqlCommand(insertQuery);
            var result = new
            {
                AccountNo = AccountNo,
                AccountName=AccountName,
            };
            return Request.CreateResponse(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "application/json");
        }

        [HttpPost]
        public HttpResponseMessage SaveOutputResult(PendingAccountsViewModel accounts)
        {
            //save logic here

            var result = new
            {
                AccountNo = accounts.AccountNo,
                AccountName = accounts.AccountName,
            };
            return Request.CreateResponse(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "application/json");
        }

    }
}
