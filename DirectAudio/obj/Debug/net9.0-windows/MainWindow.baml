   M S B A M L   `   `   `  ����F  BDirectAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DirectAudioDirectAudio  5   	       DirectAudio.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DirectAudio     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   ��  �� Orientation$ Vertical=�6   $��50q�6,   .+�5   
   �� -   5        $��Ton abspielen��5      $	��150��5      $��30��5      �� 5      -   5         ��Name$ FrequencySlider��5       � Minimum$	 300Q�5   	    � Maximum$
 2000Q�5   	    � Value$	 440Q�5   	   $	��300��5   	   $��50��5   	   $��20q�5   	   ��5      �� 5   
    �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 ��6&   .R�5      � .+�5      �� 
 ��Name$
 TrackBackground��6%   $��10��5   %   
������� �� CornerRadius$ 5��5   %   ]� 5        ]�Name$ 
PART_Track��6$    ]� DecreaseRepeatButton 5!   "   �� 5"   &    �� DecreaseLarge#��Z 63   ��5#   *   �� 5$   .   "	 ��6>   .R�5%   2   �� $��10��69   ���	#FF4C8BF5$ 5 0 0 5��5'   9   5(   /   5*   '    ]� IncreaseRepeatButton 5,   "   �� 5-   &    �� IncreaseLarge#��Z 63   ��5.   *   �� 5/   .   "	 ��6>   .R�50   2   �� $��10��69   
�������$ 0 5 5 0��52   9   53   /   55   '    ]� Thumb 57   "   n� 58   &   -   5        ��59   *   �� 5:   .   "	 n�6>   .R�5;   2   A� $��20��6:   $��20��5<   :   ���	#FF4C8BF55=   :   5>   /   5@   '   5B      5C      5D      5F      5G      5I      5M      5N      5O      