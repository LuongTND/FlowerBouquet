﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.ViewModel;

namespace WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for ManageCustomerControls.xaml
    /// </summary>
    public partial class ManageCustomerControls : UserControl
    {
        public ManageCustomerControls()
        {
            InitializeComponent();

        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //if (this.DataContext != null)
            //{ ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

    }
}
