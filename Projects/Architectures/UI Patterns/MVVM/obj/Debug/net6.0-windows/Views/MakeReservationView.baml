   M S B A M L   `   `   `  ����?  ;MVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MVVM.Views
MVVM.Views  5      1clr-namespace:MVVM.ViewModelsMVVM.ViewModels  65   $    MVVM.Views.MakeReservationView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:MVVM.Views     5      4
viewmodelsclr-namespace:MVVM.ViewModels     65   .��5	      �   � IsSharedSizeScope$
  True��6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��5      $��Make reservation��5       
  Header#��[  5      � 5   
   $��1��6   $��0 25 0 0q�6    5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� $��0��6   $��Username��6%   �� 5      $��1��5       $��0 5 0 0q�5"      ��5!      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5"      5#      � 5%   
   $��2��6   $��0 10 0 0q�6    5&      �� 5'      $
��auto �6    �� 5(      $
��auto �6     � ColumnDefinitions 5*      �� 5+      $
��auto �6#    z� SharedSizeGroup$ FirstColumn��60   �� 5,      $
��auto �6#   $ SecondColumn��60   .+�5/      �� $��0��50      $��0��51      $��	Floor No.��52      �� 54      $��1��55      $��0��56      $��0 5 0 0q�57      ��58      �� *FloorNumber+$ PropertyChanged=��� 5:      $��0��5;      $��1��5<      $��10 0 0 0q�5=      $��Room No.��5>      �� 5@      $��1��5A      $��1��5B      $��10 5 0 0q�5C      ��5D      �� *
RoomNumber+$ PropertyChanged=�5E      � 5G   
   $��3��6    5I      �� 5J      $
��auto �6    �� 5K      $
��auto �6     5N      �� 5O      $
��auto �6#   $ FirstColumn��60   �� 5P      $
��auto �6#   $ SecondColumn��60   .+�5S      �� $��0��5T      $��0��5U      $��0 0 0 0q�5V      $��
Start Date��5W      (  "System.Windows.Controls.DatePicker  5Y      $��1��5Z      $��0��5[      $��0 5 0 0q�5\         SelectedDate 5]      �� *	StartDate+$ PropertyChanged=��� 5_      $��0��5`      $��1��5a      $��10 0 0 0q�5b      $��End Date��5c        5e      $��1��5f      $��1��5g      $��10 5 0 0q�5h       5i      �� *	EndDate+$ PropertyChanged=�5k      �� 5q   
   $��4��6   $��0 25 0 0q�6"    �� Orientation$ 
Horizontal=�64   .+�5r      �� $��Submit��6   ��6&   �� *SubmitCommand+�� 5s      $��Cancel��6   $��10 0 0 0q�6H   ��6&   �� *CancelCommand+6H   5t      5v      5w      