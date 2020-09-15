using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    /// <summary>
    /// Description résumée de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        public MySqlConnection connection = null;
        public MySqlCommand cmd = null;
        public MySqlDataReader reader = null;

        public WebService1()
        {
            string connectionString = "server=localhost;port=3308;database=yellow_pages;uid=root;pwd=";
            this.connection = new MySqlConnection(connectionString);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Data> GetData()
        {
            this.connection.Open();

            string query = "SELECT * FROM `data`";

            this.cmd = new MySqlCommand(query, this.connection);

            reader = cmd.ExecuteReader();

            List<Data> myData = new List<Data>();


            while (reader.Read())
            {
                Data data = new Data()
                {
                    id = reader.GetInt32("id"),
                    company = reader.GetString("company"),
                    address = reader.GetString("address"),
                    zipcode = reader.GetString("zipcode"),
                    city = reader.GetString("city")
                };

                myData.Add(data);
            }

            this.connection.Close();

            return myData;

        }

        [WebMethod]
        public string GetDataFromCompany(string company)
        {
            this.connection.Open();
            var result = "";

            string query = "SELECT company FROM `data` WHERE company = @company";

            this.cmd = new MySqlCommand(query, this.connection);

            this.cmd.Parameters.Add("@company", MySqlDbType.VarChar).Value = company;

            reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                result = reader.GetString("company");
            }


            reader.Close();
            this.connection.Close();

            return result;
        }

        [WebMethod]
        public List<Data> GetDataByZip(string zipcode, string activity)
        {
            if(this.connection == null)
            {
                this.connection.Open();
            }

            string query = "SELECT * FROM `data` WHERE zipcode = @zipcode AND activity = @activity";

            this.cmd = new MySqlCommand(query, this.connection);

            this.cmd.Parameters.Add("@zipcode", MySqlDbType.VarChar).Value = zipcode;
            this.cmd.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activity;

            reader = cmd.ExecuteReader();

            List<Data> myData = new List<Data>();


            while (reader.Read())
            {
                Data data = new Data()
                {
                    id = reader.GetInt32("id"),
                    company = reader.GetString("company"),
                    address = reader.GetString("address"),
                    zipcode = reader.GetString("zipcode"),
                    city = reader.GetString("city")
                };

                myData.Add(data);
            }

            this.connection.Close();

            return myData;
        }

        [WebMethod]
        public List<Data> GetDataByCity(string city, string activity)
        {
            this.connection.Open();

            string query = "SELECT * FROM `data` WHERE city = @city AND activity = @activity";

            this.cmd = new MySqlCommand(query, this.connection);

            this.cmd.Parameters.Add("@city", MySqlDbType.VarChar).Value = city;
            this.cmd.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activity;


            reader = cmd.ExecuteReader();

            List<Data> myData = new List<Data>();


            while (reader.Read())
            {
                Data data = new Data()
                {
                    id = reader.GetInt32("id"),
                    company = reader.GetString("company"),
                    address = reader.GetString("address"),
                    zipcode = reader.GetString("zipcode"),
                    city = reader.GetString("city")
                };

                myData.Add(data);
            }

            this.connection.Close();

            return myData;
        }

        [WebMethod]
        public List<Data> FilterData(string activity, string city, string zipcode)
        {
            this.connection.Open();

            string query = "SELECT * FROM `data` WHERE activity = @activity AND city = @city AND zipcode = @zipcode";

            this.cmd = new MySqlCommand(query, this.connection);

            this.cmd.Parameters.Add("@activity", MySqlDbType.VarChar).Value = activity;
            this.cmd.Parameters.Add("@city", MySqlDbType.VarChar).Value = city;
            this.cmd.Parameters.Add("@zipcode", MySqlDbType.VarChar).Value = zipcode;

            reader = cmd.ExecuteReader();

            List<Data> myData = new List<Data>();


            while (reader.Read())
            {
                Data data = new Data()
                {
                    id = reader.GetInt32("id"),
                    company = reader.GetString("company"),
                    address = reader.GetString("address"),
                    zipcode = reader.GetString("zipcode"),
                    city = reader.GetString("city")
                };

                myData.Add(data);
            }

            this.connection.Close();

            return myData;
        }


        [WebMethod]
        public void PostData(string company, string address, string zipcode, string city, string activity)
        {
            var myCompany = GetDataFromCompany(company);
            if(!String.Equals(myCompany, company, StringComparison.CurrentCultureIgnoreCase))
            {

                this.connection.Open();

                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.connection;
                this.cmd.CommandText = "INSERT INTO data (company, address, zipcode, city, activity) VALUES (@company, @address, @zipcode, @city, @activity)";
                this.cmd.Prepare();


                this.cmd.Parameters.AddWithValue("@company", company);
                this.cmd.Parameters.AddWithValue("@address", address);
                this.cmd.Parameters.AddWithValue("@zipcode", zipcode);
                this.cmd.Parameters.AddWithValue("@city", city);
                this.cmd.Parameters.AddWithValue("@activity", activity);
                this.cmd.ExecuteNonQuery();

                this.connection.Close();
            }
        }
    }
}
