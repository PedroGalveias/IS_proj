using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SensorController : ApiController
    {
        // GET api/<controller>
        [Route("api/sensors")]
        public IEnumerable<Sensor> GetAllSensors()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [Route("api/sensors/{id}")]
        public IHttpActionResult GetSensorById(int id)
        {
            throw new NotImplementedException();

            /*
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

                //SqlCommand cmd = new SqlCommand();
                //var sensor = sensors.FirstOrDefault((p) => p.Id == id);
                
                //if (sensor == null)
                //{
                    //return NotFound();
                //}
                 //reader.Close();
                 //conn.Close();
                 //return Ok(sensor);
                 

                return NotFound();

            }
            catch (Exception)
            {

                throw;
            }
            */
        }

        // POST api/<controller>
        [Route("api/sensors")]
        public IHttpActionResult Post([FromBody]string typeSensor, [FromBody]string value)
        {
            throw new NotImplementedException();
            /*
            try
            {
                sensorType.Add(typeSensor, value);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
            */
        }

        // PUT api/<controller>/5
        [Route("api/sensors/{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        [Route("api/sensors/{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
