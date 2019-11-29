import struct
import numpy as np
import os

def bread(filename):
	with open(filename, 'rb') as f:
		while True:
			bytes = f.read(24)
			if bytes == b'':
				break
			else:
				id, temp, hum, batt, time, o = struct.unpack('=iffili', bytes)
				print("--------------------------" + os.linesep)
				print("ID: " + str(id) + os.linesep)
				print("Temperature: " + str(temp) + os.linesep)
				print("Hummidity: " + str(hum) + os.linesep)
				print("Battery: " + str(batt) + os.linesep)
				print("Timestamp: " + str(time) + os.linesep)
				print("--------------------------" + os.linesep)

bread("data.bin")