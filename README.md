# Flight Inspection App
##### Advanced Programming 2 - Assignment 1



This source code is a desktop app for analyzing flights.
This WPF app connects to a Flight Gear simulator, and display in a graphic way information regarding the flight
Also, there is an option of choosing an anomaly detection algorithm to display the anomalies that were found during the flight.

## Application description and goal
This app allows us to view flight data on a dedicated simulator and explore them. Our users are flight researchers or pilots who want to view data, sampled at a certain rate during a flight.
The flight data includes the steering mode, speed, altitude direction, etc., and are recorded into a text file, which can be loaded in our app.
The app will play the data like a movie from the beginning of the recording to the end, it will graphically display the plane about the earth, the rudder position, and additional flight data from several different views, including a view designed to find anomalies in the data.

## Prerequisites

- Download Visual Studio 2019 with .NET framework v4.7.1
- WPF
- C#
- .NET 5.0
- Download the Windows version for Flight Gear Simulator on https://www.flightgear.org/
- After you are done with the previous step make sure to go to the following path ```C:\Program Files\FlightGear 2020.3.6\data\Protocol``` and then put there our XML file (https://github.com/Gitit-Shapira/Flightgear/blob/main/playback_small.xml).

## Getting Started

Clone the project via the command line:
```sh
git clone https://github.com/Gitit-Shapira/Flightgear.git
```

Now, open the ```FlightInspectionapp.exe``` app you already downloaded before, if everything works as it should, you will see the following screen:

![WhatsApp Image 2021-04-14 at 14 33 18](https://user-images.githubusercontent.com/38204874/114704463-4e713d80-9d2f-11eb-84fc-6c4a0337495a.jpeg)

Now you need to update the setting to allow our project to communicate with the ```Flight Gear Simulator``,
for that you need to go to setting and in the text box enter the following text:
``` 
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null
```

A screenshot is attached:

![WhatsApp Image 2021-04-14 at 14 46 20](https://user-images.githubusercontent.com/59093573/114705659-a197c000-9d30-11eb-9fe0-e7c373cb07cc.jpeg)

Make sure to click the ```Fly``` button and wait to see the following screen before you continue:

![WhatsApp Image 2021-04-14 at 14 30 40](https://user-images.githubusercontent.com/38204874/114703614-2f25e080-9d2e-11eb-9237-14c7a15019b1.jpeg)

Now, in the ```Visual Studio``` click the ```Start``` button to clean and build the project, and the following window will pop up:

![WhatsApp Image 2021-04-14 at 14 15 50](https://user-images.githubusercontent.com/38204874/114703747-5b416180-9d2e-11eb-929e-b1ba5b52e859.jpeg)



## Features
- Choosing CSV - click on the ```Open CSV``` button to start your flight. You will be asked to choose a CSV file that contains the flight info (a CSV file in which flight data sampled at some rate is recorded).
  You can use our demo CSV file that is located in the following path:  https://github.com/Gitit-Shapira/Flightgear/blob/main/reg_flight.csv
- Choosing XML - click on the ```Open XML``` button and choose XML file from your system :
- Joystick - the movements of the flight is done according to the X and Y coordinates (from the CSV file)
- Flight Data - This Lets you see some different data:
    - Flight altitude
    - Flight speed
    - Flight direction
    - Yaw, roll, and pitch info
- Control bar:
    - Speed value - you can set the speed of the flight by selecting an option in the right corner drop-down button called ```Play Speed```

    - Play button - Use the ```Play``` button to resume the flight after the ```Stop``` or ```Pause``` button was clicked.
    - Pause button - Click the ```Pause``` button to freeze the flight.
    - Stop button - When the ```Stop``` button is pressed, the flight is being frozen, but now by clicking the ```Play``` button the flight will start from the beginning.
    - Slide bar - Allow you to move the tick along with the slider, and then the flight will jump to the corresponding time CSV line according to its position you chose.
- Flight parameters graphs: The graph is affected by the values of the flight parameter, so you can play with them and then the graph will be updated accordingly, which is presenting the progression of its value through the flight.
- Anomaly detector - This allows you to select an anomaly detection algorithm. The algorithm will detect at what moments in time an anomaly occurred and will mark it prominently so that you can effectively jump into that moment in time and investigate it.



## Project Hierarchy

The main files in our project are as follow:

- Model:
    - IFlightgearMonitorModel.cs - interface for the model in the architecture MVVM
    - MyFlightgearMonitorModel - class which implements the IFlightgearMonitorModel interface
- View Model
    - ViewModel.cs - ??
    - MainWindowViewModel.cs - The main view model
    - FlightDataViewModel.cs - The view model of flight information.
    - FlightgearMonitorViewModel.cs - The view model of  ??
    - GraphsViewModel.cs - The view model of graphs presented to the user.
    - JoystickViewModel.cs - The view model of the Joystick    -
- View:
    - GraphsView - responsible to display the graphs
    - FlightDetails - responsible to display the flight information.
    - ControlBar - responsible to display the control bar, which consists of the Play, Pause, Stop buttons, the slider, and the play speed.
    - Joystick - responsible to display the joystick
    - MainWindow -  responsible to display the main screen which contains the menu.

## More documentation
- UML:
- Control Bar: https://github.com/Gitit-Shapira/Flightgear/blob/main/FlightMonitor/ControlBar.md
- Joystick: https://github.com/Gitit-Shapira/Flightgear/blob/main/FlightMonitor/Joystick.md
- Graph: https://github.com/Gitit-Shapira/Flightgear/blob/main/FlightMonitor/Graph.md
- DLL plugin


## Demo video


## Writers
- Gitit Shapira
- Roi Peleg
- Dvir Asaf
- Or Memiya












#
