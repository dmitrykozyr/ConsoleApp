   M S B A M L   `   `   `  ����?  ;MVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MVVM.Views
MVVM.Views  5      1clr-namespace:MVVM.ConvertersMVVM.Converters  65   P LLoadingSpinnerControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:LoadingSpinnerControl;assembly=LoadingSpinnerControlLoadingSpinnerControl 6f   '    !MVVM.Views.ReservationListingView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:MVVM.Views     5      4
convertersclr-namespace:MVVM.Converters     65   dloadingspinnercontrolBclr-namespace:LoadingSpinnerControl;assembly=LoadingSpinnerControl     6f     � 	Resources  5
          BooleanToVisibilityConverter9   3MVVM.Converters.InverseBolleanToVisibilityConverter ' #InverseBolleanToVisibilityConverter��%:   &	        &	      �� 5   
   6'     5   
   69   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      �� $��0��5      $��View Reservations��5        
 Header#��[ 5!      �� 5#      $��Make Reservation��5$      $��1��5%      $��10 0 0 0q�5&      ��5'      �� *MakeReservationCommand+5)      � 5+   
   $��1��6   $��0 25 0 0q�6   .+�5.      *  $LoadingSpinnerControl.LoadingSpinner     	Thickness$ 5Q�5/         Color
 �����   Diameter$	 100Q�51         	IsLoading 52      �� *	IsLoading+� 54      y�6   �� *	IsLoading+ �� 	Converter# [ .+�56      �� 
���  ����57      �� *ErrorMessage+y�59      �� *HasErrorMessage+# [  �� 5;      ��5<      �� *Reservations+y�5=      �� *HasErrorMessage+# [ ��5?      �� 5@       �� 
TargetType" ��6    .�5A      �� 	 �� Property	 � ��
 �� Value$
 Stretch=�6K   5B       �� View 5E      �� 5F      .P�5H      �� $��Room ID��6-    �� CellTemplate 5I   "   �� 5J   &   .R�5K   *   �� ��64   �� *RoomID+5L   '   5N      �� 5P      $��Username��6-    5Q   "   �� 5R   &   .R�5S   *   ��  �� Padding$ 0 0 20 0q�6N   ��64   �� *
Username+6N   5T   '   5V      �� 5X      $��Room Start Date��6-    5Y   "   �� 5Z   &   .R�5[   *   �� $ 0 0 20 0q�6O   ��64   �� *	StartDate+6O   5\   '   5^      �� 5`      $��End Date��6-    5a   "   �� 5b   &   .R�5c   *   �� $ 0 0 20 0q�6M   ��64   �� *	EndDate+6M   5d   '   5f      5h      5k      5l      5m      5o      5p      