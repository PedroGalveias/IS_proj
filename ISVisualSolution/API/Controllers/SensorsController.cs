using API.Models;
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
        Dictionary<string, string> sensorType = new Dictionary<string, string>();

        List<Sensor> sensors = new List<Sensor>{
            new Sensor{Id= 1, SensorType = new Dictionary<string, string>().Add("temp", "22.42"), Battery= 99,Timestamp=157122222
},
            }

        // GET api/<controller>
        [Route("api/sensors")]
        

        public IEnumerable<Sensor> GetAllSensors()
        {
            return sensors;
        }

        // GET api/<controller>/5
        [Route("api/sensors/{id}")]
        public IHttpActionResult GetSensorById(int id)
        {
            SqlConnection conn = null;
            var sensor = sensor.FirstOrDefault((p) => p.Id == id);

            try
            {
                conn.Open();

                if (sensor == null)
                {
                    return NotFound();
                }
                return Ok(sensor);

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
        public IHttpActionResult Post([FromBody]string typeSensor, [FromBody]string value)
        {
            try
            {
                sensorType.Add(typeSensor, value);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
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