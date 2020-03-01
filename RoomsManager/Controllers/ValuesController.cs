using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace RoomsManager.Controllers
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        [HttpPost("createDb/{name}")]
        public IActionResult PostDb([FromRoute] string name, [FromBody] JArray jsonArray)
        {
            var result = "";
            foreach(var el in jsonArray)
            {
                var jsonObject = (JObject)el;
                foreach(var prop in jsonObject.Properties())
                {
                    //result += $"{prop.Name} : {prop.Value}" + " "; // Environment.NewLine;
                    result += $"{prop.Value}" + ",";
                }
            }
            int j = -1;
            string str = "";
            for (int i = 0; i <= result.Length - 1; i++)
            {
                if (result[i].Equals(','))
                {
                    if(j == -1)
                    {
                        str = str + " ";
                        j = 1;
                    }
                    else
                    {
                        str = str + result[i];
                        j = -1;
                    }
                }
                else
                {
                        //j=-1;
                        str = str + result[i];
                    //str = str + result[i];
                }
            }   
            var connectingString = "Server=TITCHE;Database=RoomsManager.database.dev;Trusted_Connection=True;";
            SqlConnection connect = new SqlConnection(connectingString);
            try
            {
                string dbName = name.ToString();
                connect.Open();
                string query = "CREATE TABLE " + dbName + "(customerId int, " + "customerName varchar(Max));";
                SqlCommand cmd = new SqlCommand(query, connect);
                connect.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("erreur " + e.Message);
            }
            return Ok(str);
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
