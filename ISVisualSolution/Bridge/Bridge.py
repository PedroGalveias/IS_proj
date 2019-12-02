import struct
import os
from datetime import datetime
import json
import paho.mqtt.client as mqtt

def execute_brige(filename):
	try:
		client = mqtt.Client("Bridge")
		client.connect("localhost")
	except:
		print("Error connecting to MQTT Broker")
		return
	
	try:
		f = open(filename, 'rb');
		while True:
			bytes = f.read(24)
			if bytes == b'':
				wait(10)
				continue
			else:
				id, p3, p4, temp, hum, batt, p1, p2, time, o = struct.unpack('=bbhffbbhli', bytes)
				#print("--------------------------" + os.linesep)
				print("ID: " + str(id) + os.linesep)
				print("Temperature: " + str(temp) + os.linesep)
				print("Hummidity: " + str(hum) + os.linesep)
				print("Battery: " + str(batt) + os.linesep)
				print("Timestamp: " + str(time) + " (" + str(datetime.fromtimestamp(time)) + ")" + os.linesep)
				
				data = {
					'id': id,
					'temp': temp,
					'hum': hum,
					'batt': batt,
					'time': time
				}
				
				json_data = json.dumps(data);

				client.publish("bridge", str(json_data))
				
				print("--------------------------" + os.linesep)
	except IOError:
		print("File does not exists");
	finally:
		f.close();

def main():
	execute_brige("data_1.bin")

if __name__ == "__main__":
	main()