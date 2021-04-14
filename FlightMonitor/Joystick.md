### Description

We would like to allow our user, as a pilot researcher, to see the status of the main rudders of the aircraft in a joystick-like display.
The joystick is not used to control the aircraft, but only displays the position of the rudders as they are fed out of the file. The joystick and rudders change position according to flight time.

### View
The view is used to present the GUI of the joystick on the screen.

### View Model
the view model part is used to fetch the data from the model and pass it to the GUI - which is the corresponding class in the View.
The main method here is waiting for an update of a property of the model, and when it happens will update the joystick to display the position of the rudders as they are fed out of the file according to flight time.

![Screenshot 2021-04-14 185516](https://user-images.githubusercontent.com/59093573/114740932-fa785000-9d52-11eb-979b-e926c34ea90e.png)
