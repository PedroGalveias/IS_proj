using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlertsForm.models
{
	class Sensor
	{
		//ex: msg
		/*{"id": 1, "temp": 22.489999771118164, "hum": 47.290000915527344, "batt": 100, "time": 1575058625, "sensors": ["temp", "hum"]}*/

		public int Id { get; set; }
		public Array Tipo { get; set; }
		public List<double> Valor { get; set; }

		public Sensor(string json)
		{
			JObject jSensor = JObject.Parse(json);
			//JToken jSensor = jObject[""]; //aqui deberia ir sensor? pero en el JSON ex no se lo encuentra en el inicio
			this.Id = Convert.ToInt16(jSensor["id"]); //max id 32767;
			this.Tipo = jSensor["sensors"].ToArray();
			int nValores = this.Tipo.Length;
			this.Valor = new List<double>();
			/*foreach (string tipoSensor in this.Tipo)
            {
                this.Valor.Add(Convert.ToDouble(jSensor[tipoSensor]));
            }*/
			for (int i = 0; i < nValores; i++)
			{
				this.Valor.Add(Convert.ToDouble(jSensor[Tipo.GetValue(i).ToString()]));
			}

		}
	}
}
