   M S B A M L   `   `   `  ����F  BIHC_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IHC_ProjectIHC_Project  5          IHC_Project.ShowClass   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:IHC_Project     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��296* �6   �� 5      $
��511* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	className��5      $��0��6#   $��0��63   $
��name��6@   $��Center=�6O   $��Center=�6s   $	��Top=�6�   $��30(�6�   $��Corbel)�6�   $
��Bold"�6�   
�������6�   �� 5   
   $��img/class8.png��6   $��Bottom=�6(   $��Center=�6C   $	��280��6`   $��12��6n   $	��280��6�   $��0,0,0,10q�6�   �� 5    
   $��1��6   $��1��6    $��Instructor:��6-   
�������$��
43,0,390,0q�6V   �� 5!   
   -   5        $��instructorBox��5!      $��1��6(   $��1��68   $��Center=�6F   $��Center=�6c   $	��150��6~   $��24��6�   $��125,2,232,0q�6�   �� 5"   
   $��1��6   $��1��6    $��Time:��6-   
�������$��60��6P   $��313,0,134,0q�6[   �� 5#   
   -   5        $��timeBox��5#      $��1��6$   $��1��64   $��Center=�6B   $	��Top=�6_   $��80��6w   $��24��6�   $��
368,2,59,0q�6�   .@�5$      �� $��08:00��6   �� 5%      $��09:00��6   �� 5&      $��10:00��6   �� 5'      $��11:00��6   �� 5(      $��14:00��6   �� 5)      $��15:00��6   �� 5*      $��16:00��6   �� 5+      $��17:00��6   �� 5,      $��18:00��6   �� 5-      $��19:00��6   �� 5.      $��20:00��6   5/      �� 50   
   $��1��6   $��2��6    �� 51   
   $��1��6   $��3��6    $��
Duration: ��6-   
�������$��80��6U   $��
46,0,381,0q�6a   �� 52   
   -   5        $��durationBox��52      $��1��6&   $��3��66   $��Center=�6D   $	��Top=�6a   $	��149��6y   $��24��6�   $��126,2,232,0q�6�   .@�53      �� $��30min��6   �� 54      $��60min��6   �� 55      $��90min��6   56      �� 57   
   $��1��6   $��3��6    $��
Vacancies:��6-   
�������$��70��6U   $��291,0,155,0q�6`   �� 58   
   -   5        $��vacanciesBox��58      $��1��6(   $��3��68   $��Center=�6E   $��Center=�6g   $	��Top=�6�   $��80��6�   $��24��6�   $��
368,2,59,0q�6�   �� 59   
   $��1��6   $��4��6    �� 5:   
   $��1��6   $��5��6    �� 5;   
   $��1��6   $��7��6    �� 5<   
   -   5        $ participantsLabel��5<      $��1��6+   $��8��6;   $��Participant:��6H   
�������$��80��6r   $��
36,0,391,0q�6}   �� 5=   
   -   5        $��participantsBox��5=      $��1��6*   $��8��6:   $��Center=�6H   $	��Top=�6e   $	��149��6}   $��24��6�   $��126,2,232,0q�6�   �� 5>   
   -   5         ��Name$ removeButton��5>   &   $y�Hidden=�6   $��1��6=   $��8��6M   $��Center=�6Z   $��60��6u   $��24��6�   $��Remove��6�   $��
388,1,59,1q�6�   6�   �� 5?   
   -	   5        $ 
showButton��5?      $��1��6%   $��8��65   $��Center=�6B   $��60��6]   $��24��6h   $
��Show��6t   $��304,1,143,1q�6�   6�   �� 5@   
   $��1��6   $��9��6    �� 5A   
   -
   5        $ 	nameLabel��5A   $   $y�Hidden=�6   $��1��68   $��10��6H   $��Class name:��6V   
�������$��80��6   $��
36,0,391,0q�6�   �� 5B   
   -   5        $��nameBox��5B   &   $y�Hidden=�6   $��1��65   $��10��6E   $��Center=�6S   $��Center=�6u   $	��Top=�6�   $	��149��6�   $��24��6�   $��126,2,232,0q�6�   �� 5E   
   -   5        $ deleteButton��5E      $��1��6'   $��11��67   $	��100��6E   $��55��6Q   $��Right=�6]   $��Bottom=�6y   ���#e23131$��	0,0,199,0q�6�    � ToolTip 5F      �� 5G      $��Delete��6   .��5I      �� .+�5J      �� $��img/delete3.png��6   $��50��61   $��50��6=   5K      5L      �� 5M   
   -   5        $ 
editButton��5M      $��1��6%   $��11��65   $	��100��6C   $��55��6O   $��Center=�6[   $��Bottom=�6x   ���#8E44AD$��307,0,100,0q�6�    5N      �� 5O      $
��Edit��6   .��5Q      �� .+�5R      �� $��img/edit3.png��6   $��50��6/   $��50��6;   5S      5T      �� 5U   
   -   5        $ 
saveButton��5U      $y�Hidden=�6%   $��1��69   $��11��6I   $	��100��6W   $��55��6c   $��Center=�6o   $��Bottom=�6�   ���#e28b18$��307,0,100,0q�6�    5V      �� 5W      $
��Save��6   .��5Y      �� .+�5Z      �� $��img/save6.png��6   $��50��6/   $��50��6;   5[      5\      �� 5]   
   -   5        $��1��5]      $��11��6!   $	��100��6/   $��55��6;   $��Right=�6G   $��Bottom=�6c   ���#16A085 5^      �� 5_      $
��Back��6   .��5a      �� .+�5b      �� $��img/back2.png��6   $��50��6/   $��50��6;   5c      5d      5e      5f      