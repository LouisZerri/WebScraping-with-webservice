using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WebApplication1;

namespace WebScraping
{
    public class WebScraping
    {
        private ObservableCollection<Scraper> scraper = new ObservableCollection<Scraper>();
        public WebService1 webservice = new WebService1();

        public ObservableCollection<Scraper> Scraper
        {
            get { return scraper; }
            set { scraper = value; }
        }

        public void ScrapeData(string page, string activity)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);
            

            var results = doc.DocumentNode.SelectNodes("//*[@class= 'zone-bi   ']");

            foreach(var result in results)
            {
                var entreprise = HttpUtility.HtmlDecode(result.SelectSingleNode(".//h3[@class= 'company-name noTrad']").InnerText);
                var address = HttpUtility.HtmlDecode(result.SelectSingleNode(".//div[@class = 'adresse-container noTrad']").InnerText);
                var zipcode = HttpUtility.HtmlDecode(result.SelectSingleNode(".//div[@class = 'adresse-container noTrad']").InnerText);
                var town  = HttpUtility.HtmlDecode(result.SelectSingleNode(".//div[@class = 'adresse-container noTrad']").InnerText);

                int name = address.IndexOf(",");
                int zip = address.IndexOf(",");

                entreprise = entreprise.TrimStart();
                entreprise = entreprise.TrimEnd();

                if (name > 0)
                {
                    address = address.Substring(0, name);
                    address = address.TrimStart();
                    address = address.TrimEnd();
                }

                if(zip > 0)
                {
                    zipcode = zipcode.Substring(zip + 2, 6);
                    zipcode = zipcode.TrimStart();
                    zipcode = zipcode.TrimEnd();

                    town = town.Substring(zip + 8);
                    town = town.TrimStart();
                    town = town.TrimEnd();
                }

                scraper.Add(new Scraper { Entreprise = entreprise, Adresse = address, Code = zipcode, Ville = town});
                webservice.PostData(entreprise, address, zipcode, town, activity);

            }


            

        }
    }
}
