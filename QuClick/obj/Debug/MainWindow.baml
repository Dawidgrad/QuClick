   M S B A M L   `   `   `  ����B  >QuClick, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:QuClickQuClick  5   	       QuClick.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:QuClick     5   	     � Title$  QuClick��5   	   $	��300��6   $	��400��6&   .��5
      ��  �� Padding$ 10q�6   ._�5   
   �� .+�5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��Start / Stop keybind��6   $
��Bold"�68   $
��0 10q�6J   �� 5      -   5         ��Name$ StartStopLabel��5      $��1��64   $��
No keybind��6D   $
��Bold"�6V   $
��0 10q�6h   5      �� 5      -   5         ��Name$ StartStopKeybind��5      $
��Lato)�6N   $��12(�6`   $��Press to record key��6n   $��5q�6�   $��	10 0 10 0q�6�   6�   � 5       5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��Turn keybinds on / off��6   $
��Bold"�6:   $
��0 10q�6L   �� 5      -   5        $ ToggleLabel��5      $��1��61   $
��Bold"�6A   $
��0 10q�6S   $��Not implemented yet��6a   5       �� 5!      -   5        $ ToggleKeybind��5!      $
��Lato)�6H   $��12(�6Z   $��Press to record key��6h   $��5q�6�   $��	10 0 10 0q�6�   6�   � 5#      $
��0 10q�6    5$      �� 5%      $��* �6'   �� 5&      $��* �6'   .+�5)      �� $��0��6   $��Fix cursor to spot��6,   $
��Bold"�6F   �� 5*      -   5        $ FixKeybindLabel��5*      $��1��65   $��
No keybind��6E   $
��Bold"�6W   5+      �� 5,      -   5        $ 
FixKeybind��5,      $
��Lato)�6B   $��12(�6T   $��Press to record keybind��6b   $��5q�6�   $��	10 0 10 0q�6�   6�   � 5.      $
��0 20q�6    5/      �� 50      $��* �6'   �� 51      $��* �6'   �� 52      $��* �6'   �� 53      $��* �6'   .+�56      �� $��0��6   $��	Frequency��6,   $
��Bold"�6=   $
��0 10q�6O   �� 57      -   5         ��Name$ 	Frequency��57      $��1��6-   $��2q�6=   $��0 7 0 7q�6I   �� 58      $��2��6   $��Clicks/s��6,    �� Padding$ 5 10 0 0q�6<   �� 59      -   5        $ FrequencyButton��59      $��3��6P   $��	10 7 10 7q�6`   $��Change��6s   5:      5;      5<      5>      