   M S B A M L   `   `   `  ����H  DFlightMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FlightMonitorFlightMonitor  5   	   -clr-namespace:FlightMonitorFlightMonitor  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35P<clr-namespace:Microsoft.Win32;assembly=PresentationFrameworkMicrosoft.Win32 61       FlightMonitor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:FlightMonitor     5   	   +conclr-namespace:FlightMonitor     5   	   Nwin32<clr-namespace:Microsoft.Win32;assembly=PresentationFramework     61   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  6|   -   5          �Name$  main_window��5   	    � Title$ 
MainWindow��5	   	   $	��650��6   $
��1000��6)    J� 	AllowDrop . ��5
      �� 5   
    �� ImageSource$ Sky.png��6    j� Stretch$
 Fill=�6+   .��5      �  � ShowGridLines .  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��2* �6   .+�5   
   � $��0��6   $��2��6$   $��2��61   .+�5      �� -   5         ��Name$ ListBox1��5      $��Stretch=�5      $��Stretch=�5      ��5      �� *VM_AnomalyList+5      5      !   FlightMonitor.FlightDetails  5    
   -   5        	  Name$	 FlightDetails1��5       $��3��64   $��3��6E      FlightMonitor.ControlBar  5!   
   -   5        
  Name$
 ControlBar1��5!      $��0��6.   $��4��5"      $��4��6-   $��2��6;      FlightMonitor.GraphsView  5#   
   -   5          Name$ GraphsView1��5#      $��1��6.   $��2��5$      $��2��6&   (� 5%   
   $��2��6   $��2��6"   .��5&         FlightMonitor.Joystick  -   5          Name$ 	Joystick1��5&      $	��679��6.   $	��439��6:   5'      �� 5(   
   -   5        $��0��5(      $��0��6   $��Open CSV��5)      6$   �� 5*   
   -   5        $��1��5*      $��0��6   $��Open XML��5+      6$   5-      5.      