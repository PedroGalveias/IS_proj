using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SensorsController : ApiController
    {
        // GET api/<controller>
        [Route("api/sensors")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("api/sensors/{id}")]
        public IHttpActionResult GetSensorById(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn.Open();
                /*  SqlCommand cmd = new SqlCommand();
                var sensor = sensors.FirstOrDefault((p) => p.Id == id);
                
                if (sensor == null)
                {
                    return NotFound();
                }
                 reader.Close();
                 conn.Close();
                 return Ok(sensor);
                 */

                return NotFound();

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST api/<controller>
        [Route("api/sensors")]
        public void Post([FromBody]string value)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<controller>/5
        [Route("api/sensors/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [Route("api/sensors/{id}")]
        public void Delete(int id)
        {
        }
    }
}