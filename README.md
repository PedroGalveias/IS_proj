# Systems Integration Project 2019/2020

## Implementing Smart Information and Communication Technology (ICT) for well-being, energy efficiency and surveillance in public buildings 


## What is the project about?
Internet of Things will have a big impact in our society in terms of security, surveillance, well-being and even on climate
change. Cities, plants, buildings, offices, homes, cars will be turned into smart places and appliances that can save
energy costs, by measuring and controlling electricity consumption and even correcting our bad behaviors, such as
letting lights and temperature systems turned on after left the infrastructure. Homes and buildings will bring more
comfort to our life by automatically setting the environment (e.g. temperature, humidity, media appliances and alike)
according to our preferences, and detect just-in-time resource wasting such as water and gas leaks. IoT will also impact
on access control and surveillance by relying in real time monitoring. The impact is so large that even the climate
change problem would benefit from it.
If we consider our school campus or the entire Polytechnic of Leiria schools’ campus, the employment of IoT could have
an interesting impact at our scale. Hence, this project deals with implementing smart information and communication
Technology (ICT) concepts in public buildings, of course, following an evolutionary approach as we will improve the
system with new features and capabilities year by year.
The IPLeiriaSmartCampus solution consists in the development of an integrated platform that relies on IoT in order to
improve energy efficiency, well-being and surveillance in all the Polytechnic of Leiria campus.
In this first project iteration only the Campus 2 is to be considered but the result solution must be flexible enough to be
possible to be applied to any Polytechnic of Leiria Campus. More specifically, the first project iteration deals with
temperature and humidity of Library only. Hence, the main aim of the first project iteration is to record temperature
and humidity of the Library building. Once the whole campus is covered by WiFi network it could be a good idea to rely
on this network in order to grab sensors data. Due to the lack of suitable hardware (sensor nodes), in this first project
iteration you must be able to deal with simulated sensors, which means that a kind of bridge device (and software) will
be needed in order to push simulated sensor’s data into the WiFi network.
As far as simulated sensors is concerned, you must assume that library building already includes an old and local
temperature and humidity monitoring system that beeps when temperature or humidity falls outside safety
(considering books and other contents) values. It is known that this system is running in the receptionist computer and
apart from beeping it also logs temperature and humidity into a local binary file. When the local system logs data to the
binary file (data.bin) it also presents on console the following information:

   SENSOR ID: 1
  TEMPERATURE: 22.42
  HUMIDITY: 47.76
  BATTERY: 99
  TIMESTAMP: 1571568195 (2019-10-20 11:43:15)
  SENSOR ID: 2
  TEMPERATURE: 22.83
  HUMIDITY: 47.74
  BATTERY: 98
  TIMESTAMP: 1571568195 (2019-10-20 11:43:15)
 
As the file structure is not known, you must rely on this printed information in order to guess and implement an
application in order to also forward this information to the WiFi network. Considering the printed information, it is also
possible to guess that the local monitoring system includes 2 physical sensors (id: 1 (upstairs) and id: 2 (downstairs)),
one per building floor and battery data ranges from 0 to 100%. An Unix executable file is provided to mimic the
operation of the library monitoring system in your laptop and you can assume that you have rights to install your
“connector” into the receptionist computer (in this case, in your laptop).
The simulated sensor’s data must be forward into a system where multiple (micro) applications can exist to visualize,
monitor and analyze that data.
For now, two local desktop applications must be developed. The first one must be able to show real-time temperature
and humidity data per floor in tabular and chart formats. There is no need to locally store sensor data. However, while
active, the application must keep and show the extracted sensor data since the moment it is opened until the moment
it is closed.
The second micro application must be able to configure and trigger alerts, according to the conditions: >, <, = and
between and each configured alarm could be enabled or disabled. All required configuration must be stored in an
appropriate local file and generated alerts must be shared.
Apart from (micro) applications, sensor’s data must be forwarded into a database and a globally API must be available
to give the chance to school community to access and use the collected data, alerts, etc., following different interaction
approaches, probably in community applications or applications developed in other curricular units. API must include,
at least, register a ‘personal’ sensor, add new sensor data, invalidate sensor data (for the case of malfunctioning
sensors), get sensor data per floor with or without date interval, get a list of all the sensors and get alarms data.
Remember that API and other kind of data sharing are only available to qualified users/clients.
Optionally, the API may include also actions to store temperature sensors’ data coming from community mobile devices
(personal sensors).
Your work is to implement the solution and write the project report. Remember that the System Architecture of the
overall solution must be included in the project report and well explained. It is mandatory! As another tip, we advise
you to first draw the system architecture and only after that start implement it.

## Browsers support

| [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/edge/edge_48x48.png" alt="IE / Edge" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>IE / Edge | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/firefox/firefox_48x48.png" alt="Firefox" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Firefox | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/chrome/chrome_48x48.png" alt="Chrome" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Chrome | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/safari/safari_48x48.png" alt="Safari" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Safari | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/safari-ios/safari-ios_48x48.png" alt="iOS Safari" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>iOS Safari | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/samsung-internet/samsung-internet_48x48.png" alt="Samsung" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Samsung | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/opera/opera_48x48.png" alt="Opera" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Opera | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/vivaldi/vivaldi_48x48.png" alt="Vivaldi" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)<br>Vivaldi |
| --------- | --------- | --------- | --------- | --------- | --------- | --------- | --------- |
| IE11, Edge| last 2 versions| last 2 versions| last 2 versions| last 2 versions| last 2 versions| last 2 versions| last 2 versions
