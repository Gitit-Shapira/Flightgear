   M S B A M L   `   `   `  ����H  DFlightMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FlightMonitorFlightMonitor  5          FlightMonitor.GraphsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FlightMonitor     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  68   .��5	      �   � ShowGridLines  .  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��5      .+�5      �� -   5         ��Name$ comboBox��5      {�5      �� *VM_FilesInput+��5      �� *VM_ColumnNames+ �� SelectedValue 5      �� *VM_Selection+5      �� 5   
   -   5        $��1��5      $��1��6   $��Open DLL��5      6$     OxyPlot.Wpf.Plot  5   
   $��2��6   $��0��6       TitleFontSize$ 10Q�6k   ���	#7FFFFFFF   Title 61   ��  �� ElementName$ comboBox�� �� Path$ SelectedItem�	   Series	 5        OxyPlot.Wpf.LineSeries  5       ��6!   �� *VM_SelFeatDataPoints+5"        5#   
   $��2��6   $��1��6    $ 10Q�6N   ���	#7FFFFFFF 61   �� *
VM_CorFeat+	 5$        5%      ��6!   �� *VM_CorFeatDataPoints+5'        5(   
   $��3��6   $��1��6    $+ %Linear regression of the two features��61   $ 10Q�6_   ���	#7FFFFFFF	 5)        5*      
������ !  OxyPlot.Wpf.DataPointSeries
   
DataFieldX$
 X��6f      
DataFieldY$ Y��6u   ��6:   �� *VM_LinRegDataPoints+6u     5+      
������    	LineStyle$
 None=�6:      
MarkerType$ Circle=�6L      
MarkerSize$ 1Q�6`      
MarkerFill$ Black��6o   $
 X��6�   $ Y��6�   ��6�   �� *VM_CombinedDataPoints+6�     5,      
������ $
 None=�6:   $ Circle=�6L   $ 1Q�6`   $ HotPink��6o   $
 X��6�   $ Y��6�   ��6�   �� *VM_RecentCombinedDataPoints+6�   5.        5/   
   $��3��6   $ 	Anomalies��6!   $ 10Q�63   ���	#7FFFFFFF.	 50        
������ $
 X��6\   $ Y��6k   ��66   �� *VM_DLLDisplay+6k     51      
������ $
 None=�66   $ Circle=�6H   $ 1Q�6\   $ Black��6k   $
 X��6�   $ Y��6�   ��6~   �� *VM_CombinedDataPoints+6�     52      
������ $
 None=�66   $ Circle=�6H   $ 1Q�6\   $ 	OrangeRed��6k   $
 X��6�   $ Y��6�   ��6�   �� *VM_AnomalyPoints+6�   53      54      55      