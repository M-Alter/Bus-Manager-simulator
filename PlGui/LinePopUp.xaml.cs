﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class LinePopUp : Window
    {
         public LinePopUp()
        {
            InitializeComponent();
        }

        public LinePopUp(BO.Line line)
        {
            InitializeComponent();
            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            lineGrid.Visibility = Visibility.Visible;
        }

        

        
    }
}
