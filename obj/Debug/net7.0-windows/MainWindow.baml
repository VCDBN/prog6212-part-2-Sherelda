   M S B A M L   `   `   `  ����B  >WPFPROG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPFPROGWPFPROG  5   	       WPFPROG.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WPFPROG     5   	     � Title$!  Time Management Application��5   
   $	��600��6.   $	��845��6;   .��5	      � .+�5
   
   �� $��20q�6   
������.+�5      �� $��
Add Module��6   $��10q�6+   .��5      �� $��10q�6   .+�5      �� -   5        $��
ModuleCode��5      $��	10,0,10,5q�60   $'��!Enter Module Code (e.g. PROG6212)��6C   �� 5      -   5        $��
ModuleName��5      $��	10,0,10,5q�60   $-��'Enter Module Name (e.g. Programming 2B)��6C   �� 5      -   5        $��ModuleCredits��5      $��	10,0,10,5q�63   $'��!Enter Number of Credits (e.g. 14)��6F   �� 5      -   5        $��ModuleHours��5      $��	10,0,10,5q�61   $)��#Enter Class Hours Per Week (e.g. 5)��6D   �� 5      -   5        $��
Add Module��5      $��
10,10,10,0q�62   $	��207��6Z   5      5      �� 5      $��Semester Details��6   $��10q�61   .��5      �� $��10q�6   .+�5      �� -   5        $��SemesterWeeks��5      $��	10,0,10,5q�63   $'��!Enter Number of Weeks in Semester��6F   (  "System.Windows.Controls.DatePicker  5      -   5        $��	StartDate��5   !   $��	10,0,10,5q�62      Text$# Select Start Date of Semester��6E   �� 5      -   5        $��Set Semester Details��5      $��
10,10,10,0q�6<   
�������$	��207��6�   5      5      �� 5       -	   5        $��ModuleListView��5       $��
10,10,10,5q�6-    �� View 5!      �� 5"      .P�5#      �� $
��Code��6)    �� Width$	 100��6]    �� DisplayMemberBinding 67   �� *Code+6]   �� 5$      $
��Name��6)   $	 200��6]    67   �� *Name+6]   �� 5%      $��Self-Study Hours/Week��6)   $	 150��6x    6H   �� *SelfStudyHours+6x   5&      5(      �� 5+      $��Record Study Hours��6   $��10q�63   .��5,      �� $��10q�6   .+�5-      �� -
   5        $��StudyModuleSelector��5-      $��	10,0,10,5q�6:    �� DisplayMemberPath$
 Code��6M    �� Text$ Select a Module��6f   �� 5.      -   5        $��HoursStudiedText��5.      $��	10,0,10,5q�66   $��Enter Hours Studied��6I     5/      -   5        $��	StudyDate��5/   !   $��	10,0,10,5q�62   $ Select Date��6E   �� 50      -   5        $��Record Study��50      $��
10,10,10,0q�64   $	��207��6^   51      52      53      54      55      