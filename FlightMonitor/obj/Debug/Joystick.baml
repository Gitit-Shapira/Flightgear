   M S B A M L   `   `   `  ����H  DFlightMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FlightMonitorFlightMonitor  5          FlightMonitor.Joystick   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FlightMonitor     5      .��5	      � $��0,0,-43,-49q�6     � RowDefinitions  5
   
   �� 5      $	��94* �6   �� 5      $
��131* �6   .+�5   
   �� -   5         ��Name$
 Base��5      $��230,55q�6    $	��340��60   $	��340��6<   $��2��6I   .+�5      A� $
��Left=�6   $	��340��61   $	��Top=�6>   $	��340��6V   ��5      
� 5      .��5      �  � Color$ 	#FF2C2A2A��6'    � Offset$ 1Q�69   � 5      $ 	#FF3A3737��6'   5      5      A� 5      $
��Left=�6   $	��170��61   $	��Top=�6>   $	��170��6V    �� Left$ 84��6b   
 �� Top$ 84��6s   ��5      
� 5      .��5      � $ 	#FF0E0E0E��6'   $ 1Q�69   � 5      $ 	#FF1D1D1D��6'   � 5      $ 	#FF323030��6'   $ 0.453Q�69   5      5      F� 5       "�����~C�[��xօP��� ���[�	���	#FF575757$
��Left=�6Y   $��18.375��6t    �� Stretch$
 Fill=�6�    � UseLayoutRounding .  $	��Top=�6�   $��42.75��6�   $ 147.875��6�   $ 37.625��6�   F� 5!      "�����~C�[��xօP��� ���[�	���	#FF575757$
��Left=�6Y   $��18.375��6t   $
 Fill=�6�    .  $	��Top=�6�   $��42.75��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $ 147.875��6�   $ 284.125��6	  z�5"      �� 5#      	 �� ScaleY$	 -1Q�6%   5%      F� 5&      "�����~C�[��xօP��� ���[�	���	#FF575757$
��Left=�6Y   $��18.375��6t   $
 Fill=�6�    .  $	��Top=�6�   $��42.75��6�   $ 0.5,0.5&�6�   $ 270.875��6�   $ 162.125��6	  z�5'      V� 5(      .~�5)      �� $	 -1Q�6)   
 �� ScaleX$
 -1Q�65   �� 5*       �� Angle$	 -90Q�6*   5+      5-      F� 5.      "�����~C�[��xօP��� ���[�	���	#FF575757$
��Left=�6Z   $��18.375��6u   $
 Fill=�6�    .  $	��Top=�6�   $��42.75��6�   $ 0.5,0.5&�6�   $ 24.375��6�   $ 163.625��6	  z�5/      V� 50      .~�51      �� $ 90Q�6*   �� 52      $
 -1Q�6)   53      55      �� 57      -   5        $
 Knob��57      $	��Top=�6$   $
��Left=�6<   $��0��6W   $��0��6a   $ 0.5,0.5&�6l   $	 125��6�   $	 125��6�   .+�59      A� -   5         A�Name$ KnobBase��59      $
��Left=�6f   $��90��6�   $	��Top=�6�   $��90��6�   $ 0.5,0.5&�6�    6,   �� *VM_Y+ 6H   �� *VM_X+��5:      
� 5;      .��5<      � $ 	#FF8A8A8A��6+   � 5=      $ 	#FF979797��6+   $ 1Q�6=   5>      5@      A� 5A      $
��Left=�6T   $��74.313��6o   $	��Top=�6   $��82.189��6�   $ 0.5,0.5&�6�    .   6   �� *VM_Y+ 67   �� *VM_X+��5B      
� 5C      .��5D      � $ 	#C0828080��6+   $ 0.797Q�6=   � 5E      $ 	#FD000000��6+   5F      z�5H      V� 5I      .~�5J      �� $ -28.434Q�6.   �� 5K       �� AngleX$ -2.144Q�6,   T� 5L       T� X$ -1.199Q�61    T� Y$ 0.649Q�6<   5M      5O      A� 5P      $
��Left=�6   $��75.491��65   $	��Top=�6F   $��70.887��6^   $ 0.5,0.5&�6m    .  $ 12.396��6�   $ 5.057��6�   ��5Q      �� 5R       �� EndPoint$ 0.5,1&�6.    �� 
StartPoint$ 0.5,0&�6?   .��5S      � $ 	#00858585��6+   $ 0Q�6=   � 5T      $ 	#1AFFFFFF��6+   $ 1Q�6=   � 5U      $ 	#3FC2C2C2��6+   $ 0.349Q�6=   5V      z�5X      V� 5Y      .~�5Z      ��  �� CenterX$ 3Q�6,    �� CenterY$ -4Q�68   �� 5[      $ -.7628Q�6.   5\      5^      A� 5_      $
��Left=�6S   $��72.722��6n   $	��Top=�6~   $��72.936��6�   $ 0.5,0.5&�6�    .   6   �� *VM_Y+ 66   �� *VM_X+��5`      
� 5a      .��5b      � $ 	#9A909090��6+   $ 1Q�6=   � 5c      $
 Gray��6+   5d      z�5f      �� 5g      $ -31.733Q�6*   6<   5i      A� 5j      $
��Left=�6   $��37��65   $	��Top=�6A   $��39��6Y   $ 0.5,0.5&�6d    .  $ 14.001��6�   $ 11.001��6�   z�5k      �� 5l      $ -8Q�6(   67   ��5n      
� 5o      .��5p      � $ 	#FF898989��6+   $ 0Q�6=   � 5q      $ 	#38777777��6+   $ 1Q�6=   � 5r      $ 	#55676767��6+   $ 0.672Q�6=   5s      5u      F� 5v      �����fÔ �ҵ �`�� 0j� ѐ�� �� Ѵ� ؍�<7�  �7�J#� ��=�v�j X:�0�f H���t ��� �"��  � � �7 �Ŕ ��l �ڙ �b�W �� Ѡ6G ��� QR B h��|?�0@���@ 0ѶeC  �8�    p�3�N�  �R� (� �@&& �� �<5R�` ј�BR�` Ѹ�J �� Ѱ�<�Ŕ �$�hL� �d�� @]� �@� �ǆ ��� ��j �+?    �+?:O0 �h6��- �d�� �6 �TE� �I �D� v�[ ��8� ��a �@�� �|: Ѩ!� ��$ �Tv� � ��n��j0@�� 	���	#99EEEEEE$
��Left=�6�  $��20.833��6�  $
 Fill=�6�   .  $	��Top=�6�  $��21.667��6�  $ 18.166��6  $ 15.917��6  z�5x      T� 5y      -   5        knobPosition��5y   )    � 	Resources 5{      ��%    5|      5}      5~      �� 5   
   -   5        $��slider_rudder��5       � Minimum$ -1Q�6-    � Maximum$ 1Q�6:   $
��Left=�6F   $��230,225.333,0,39.333q�6a   $	��340��6�   
����  �$��1��6�    � Value 6   �� *	VM_Rudder+6�   �� 5�   
   -   5        $��slider_throttle��5�   v   $ -1Q�6/   $ 1Q�6<   $
��Left=�6H   $��	-4,39,0,0q�6c   $	��Top=�6�   $	��339��6�   
����+��$��21��6�   $ 0.5,0.5&�6�   $��1��6�    6   �� *VM_Throttle+z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      $ -89.59Q�6&   T� 5�      5�      5�      �� 5�   
   $
��Left=�6   $��40��6/   $��
373,10,0,0q�6;   $
��Wrap=�6O   $��aileron��6c   $	��Top=�6r   $��81��6�   $ 
1.925,0.13&�6�   $��25(�6�   �� 5�   
   $
��Left=�6   $��32��6/   $��	596,1,0,0q�6;   $
��Wrap=�6N   $��elevator��6b   $	��Top=�6r   $��94��6�   $ -1.06,0.719&�6�   $��1��6�   $��25(�6�    � LayoutTransform 5�      �� 5�      $ 90Q�6"   5�      �� 5�   
   $
��Left=�6   $��40��6/   $
��Wrap=�6;   $��rudder��6O   $	��Top=�6]   $��81��6u   $ 
1.925,0.13&�6�   $��25(�6�   $��359,250.333,0,0q�6�   $��1��6�   �� 5�   
   $
��Left=�6   $��32��6/   $
��Wrap=�6;   $��throttle��6O   $	��Top=�6_   $	��101��6w   $ -1.06,0.719&�6�   $��25(�6�   $��
92,207,0,0q�6�   $��2��6�    5�      �� 5�      $ 90Q�6"   5�      5�      5�      