   M S B A M L   `   `   `  ���� E  ACinema.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Cinema.WPF
Cinema.WPF      Cinema.WPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:Cinema.WPF       � Title$  
MainWindow��$	��400��$	��600��.���  � ShowGridLines .  � RowDefinitions �� $��30 ��� $��* ��� $��40 � � ColumnDefinitions �� .+��� -    ��Name$ button��$��2��$��0��$��Обновить��$
��Left=�$��10,0,0,0q�$��75��$��35��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$��1��$��0��   
IsReadOnly . $
��Left=�$��0,0,0,0q�$	��Top=�$
��auto��$
��auto��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   
Visibility$	 Hidden=�1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 ��  �� Path$ Id�     Header$ Название��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 4* 
 �� $ Title�  $ Начало фильма��$ 3* 
 �� $ Start� �� StringFormat$ {0:dd.MM.yyyy hh:mm}��  $, &Число свободных мест��$ 2* 
 �� $ 
FreePlaces�4  .System.Windows.Controls.DataGridTemplateColumn  $ Купить билеты��$
 2.3*    CellTemplate �� .R��� -   $��купить билет��
 � Tag �� *.+���� *	showModal+���� $ Id�