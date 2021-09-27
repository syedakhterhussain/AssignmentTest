using System;
using System.Collections.Generic;
using System.Linq;

using WebAPIAndReactUI.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace WebAPIAndReactUI.Data
{

  
public class CovidDbInitializer 
    {
       
       

    public void CreateDb(CovidDataContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();

             seedSummeryDataAsync(context);

        }

        public void seedSummeryDataAsync(CovidDataContext context)
        {
  

            var Request = WebRequest.Create(Constant.SummeryUrl);

            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            var Response = (HttpWebResponse)Request.GetResponse();
             
            var DataStream = Response.GetResponseStream();
            
            var Reader = new StreamReader(DataStream);
          
            var JsonString = Reader.ReadToEnd();
           
            Reader.Close();
            DataStream.Close();
            Response.Close();
            Summery ReturnedJson = JsonConvert.DeserializeObject<Summery>(JsonString);
            parseSummeryJson(ReturnedJson,context);



        }

        public void seedCountryHistorryDataAsync(CovidDataContext context)
        {


            var Request = WebRequest.Create(Constant.CountryHistoryUrl);
            
            var Response = (HttpWebResponse)Request.GetResponse();

            var DataStream = Response.GetResponseStream();

            var Reader = new StreamReader(DataStream);

            var JsonString = Reader.ReadToEnd();

            Reader.Close();
            DataStream.Close();
            Response.Close();
            List<CountryHistory> ReturnedJson = JsonConvert.DeserializeObject<List<CountryHistory>>(JsonString);

            parseCountryHistorryJson(ReturnedJson,context);

        }

        private void parseSummeryJson(Summery ReturnedJson, CovidDataContext context)
        {
          
            Summery summery = new Summery();

              if (ReturnedJson != null)
            {
                summery.ID = ReturnedJson.ID;
                summery.Message = ReturnedJson.Message;
                summery.Global = ReturnedJson.Global;
                summery.Date = ReturnedJson.Date;
                summery.Countries = ReturnedJson.Countries;
                context.GlobalStats.Add(summery.Global);
                seedCountryHistorryDataAsync(context);
            }

          
        }

        private void parseCountryHistorryJson(List<CountryHistory> ReturnedJson, CovidDataContext context)
        {

           

            if (ReturnedJson != null)
            {
                CountryHistory history;
                for (int k = 0; k < ReturnedJson.Count; k++)
                {
                     history= new CountryHistory();
                    history.Country = ReturnedJson[k].Country ;
                    history.CountryCode = ReturnedJson[k].CountryCode;
                    history.City= ReturnedJson[k].City;
                    history.CityCode = ReturnedJson[k].CityCode;
                    history.Province = ReturnedJson[k].Province;
                    history.Lat = ReturnedJson[k].Lat;
                    history.Lon = ReturnedJson[k].Lon;
                    history.Cases = ReturnedJson[k].Cases;
                    history.Status = ReturnedJson[k].Status;
                    history.Date = ReturnedJson[k].Date;



                }

            }
            context.SaveChanges();

        }

       
    }
}