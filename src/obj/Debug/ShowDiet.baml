   M S B A M L   `   `   `  ����F  BIHC_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IHC_ProjectIHC_Project  5          IHC_Project.ShowDiet   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:IHC_Project     5      .��5	      �   � ColumnDefinitions  5
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
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ dietName��5      $��0��6"   $��0��62   $��2��6?   $
��name��6P   $��Center=�6_   $	��Top=�6|   $��30(�6�   $��Corbel)�6�   $
��Bold"�6�   
�������6�   �� 5    
   $��img/diet_img7.png��6   $��Bottom=�6+   $��Center=�6F   $	��280��6c   $��15��6q   $	��280��6�   $��0,0,0,10q�6�   �� 5"   
   $��1��6   $��1��6    $
��Day:��6-   $��Center=�6<   
�������$��25,0,0,0q�6q   �� 5#   
   -   5        $��dayBox��5#      $��1��6!   $��1��61   $��Center=�6?   $��Center=�6\   $	��120��6w   $��24��6�   $��
65,1,322,1q�6�   .@�5$      �� $��Monday��6   �� 5%      $��Tuesday��6   �� 5&      $��	Wednesday��6   �� 5'      $��Thursday��6   �� 5(      $��Friday��6   �� 5)      $��Saturday��6   �� 5*      $��Sunday��6   5+      �� 5-   
   $��1��6   $��1��6    $��Meal:��6-   
�������$��Center=�6P   $��206,0,261,0q�6r   �� 5.   
   -   5        $��mealBox��5.      $��1��6"   $��Center=�63   $��Center=�6P   $	��120��6k   $��24��6w   $��253,1,134,1q�6�   $��1��6�   .@�5/      �� $��	Breakfast��6   �� 50      $��Morning snack��6   �� 51      $��Lunch��6   �� 52      $��Afternoon snack��6   �� 53      $��Dinner��6   �� 54      $��Supper��6   55      �� 56   
   $��1��6   $��1��6!   $��Center=�6.   $��60��6I   $��24��6T   $
��Show��6`   $��
407,3,40,3q�6o   �� 57   
   $��1��6   $��2��6    �� 58   
   -   5        $ 	foodLabel��58      $��1��6#   $��3��63   $
��Food��6@   
�������$��Center=�6b   $��133,0,334,0q�6�   �� 59   
   -   5        $ quantityLabel��59      $��1��6'   $��3��67   $��Quantity��6D   
�������$��Center=�6j   $��333,0,116,0q�6�   �� 5:   
   -   5        $��foodBox1��5:      $��1��6#   $��4��63   $��Center=�6@   $��Center=�6]   $	��120��6x   $��24��6�   $��
90,0,297,0q�6�   �� 5;   
   -   5        $��quantityBox1��5;      $��1��6'   $��4��67   $��Center=�6D   $��Center=�6a   $	��120��6|   $��24��6�   $��
299,0,88,0q�6�   .@�5<      �� $	��50g��6   �� 5=      $
��100g��6   �� 5>      $
��150g��6   �� 5?      $
��200g��6   �� 5@      $
��250g��6   5A      �� 5B   
   $��1��6   $��5��6    �� 5C   
   -   5        $��foodBox2��5C      $��1��6#   $��6��63   $��Center=�6@   $��Center=�6]   $	��120��6x   $��24��6�   $��
89,0,298,0q�6�   �� 5D   
   -	   5        $��quantityBox2��5D      $��1��6'   $��6��67   $��Center=�6D   $��Center=�6a   $	��120��6|   $��24��6�   $��
299,0,88,0q�6�   .@�5E      �� $	��50g��6   �� 5F      $
��100g��6   �� 5G      $
��150g��6   �� 5H      $
��200g��6   �� 5I      $
��250g��6   5J      �� 5K   
   $��1��6   $��7��6    �� 5L   
   -
   5        $��foodBox3��5L      $��1��6#   $��8��63   $��Center=�6@   $��Center=�6]   $	��120��6x   $��24��6�   $��
89,0,298,0q�6�   �� 5M   
   -   5        $��quantityBox3��5M      $��1��6'   $��8��67   $��Center=�6D   $��Center=�6a   $	��120��6|   $��24��6�   $��
299,0,88,0q�6�   .@�5N      �� $	��50g��6   �� 5O      $
��100g��6   �� 5P      $
��150g��6   �� 5Q      $
��200g��6   �� 5R      $
��250g��6   5S      �� 5T   
   $��1��6   $��9��6    �� 5U   
   -   5        $��foodBox4��5U      $��1��6#   $��10��63   $��Center=�6A   $��Center=�6^   $	��120��6y   $��24��6�   $��
89,0,298,0q�6�   �� 5V   
   -   5        $��quantityBox4��5V      $��1��6'   $��10��67   $��Center=�6E   $��Center=�6b   $	��120��6}   $��24��6�   $��
299,0,88,0q�6�   .@�5W      �� $	��50g��6   �� 5X      $
��100g��6   �� 5Y      $
��150g��6   �� 5Z      $
��200g��6   �� 5[      $
��250g��6   5\      �� 5]   
   $��1��6   $��11��6    �� 5^   
   -   5        $��foodBox5��5^      $��1��6#   $��12��63   $��Center=�6A   $��Center=�6^   $	��120��6y   $��24��6�   $��
88,0,299,0q�6�   �� 5_   
   -   5        $��quantityBox5��5_      $��1��6'   $��12��67   $��Center=�6E   $��Center=�6b   $	��120��6}   $��24��6�   $��
299,0,88,0q�6�   .@�5`      �� $	��50g��6   �� 5a      $
��100g��6   �� 5b      $
��150g��6   �� 5c      $
��200g��6   �� 5d      $
��250g��6   5e      �� 5g   
   -   5         ��Name$ deleteButton��5g      $��1��6'   $��14��67   $	��100��6E   $��55��6Q   $��Right=�6]   $��Bottom=�6y   ���#e23131$��	0,0,199,0q�6�    � ToolTip 5h      �� 5i      $��Delete��6   .��5k      �� .+�5l      �� $��img/delete3.png��6   $��50��61   $��50��6=   5m      5n      �� 5o   
   -   5        $ 
editButton��5o      $��1��6%   $��14��65   $	��100��6C   $��55��6O   $��Center=�6[   $��Bottom=�6x   ���#8E44AD$��307,0,100,0q�6�    5p      �� 5q      $
��Edit��6   .��5s      �� .+�5t      �� $��img/edit3.png��6   $��50��6/   $��50��6;   5u      5v      �� 5w   
   -   5        $ 
saveButton��5w      $y�Hidden=�6%   $��1��69   $��14��6I   $	��100��6W   $��55��6c   $��Center=�6o   $��Bottom=�6�   ���#e28b18$��307,0,100,0q�6�    5x      �� 5y      $
��Save��6   .��5{      �� .+�5|      �� $��img/save6.png��6   $��50��6/   $��50��6;   5}      5~      �� 5   
   -   5        $��1��5      $��14��6!   $	��100��6/   $��55��6;   $��Right=�6G   $��Bottom=�6c   ���#16A085 5�      �� 5�      $
��Back��6   .��5�      �� .+�5�      �� $��img/back2.png��6   $��50��6/   $��50��6;   5�      5�      5�      5�      