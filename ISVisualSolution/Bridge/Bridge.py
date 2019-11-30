import struct
import os
from datetime import datetime

def execute_brige(filename):
	with open(filename, 'rb') as f:
		while True:
			bytes = f.read(24)
			if bytes == b'':
				break
			else:
				id, p3, p4, temp, hum, batt, p1, p2, time, o = struct.unpack('=bbhffbbhli', bytes)
				print("--------------------------" + os.linesep)
				print("ID: " + str(id) + os.linesep)
				print("Temperature: " + str(temp) + os.linesep)
				print("Hummidity: " + str(hum) + os.linesep)
				print("Battery: " + str(batt) + os.linesep)
				print("Timestamp: " + str(time) + " (" + str(datetime.fromtimestamp(time)) + ")" + os.linesep)
				print("--------------------------" + os.linesep)


def main():
	execute_brige("data_1.bin")

if __name__ == "__main__":
	main()