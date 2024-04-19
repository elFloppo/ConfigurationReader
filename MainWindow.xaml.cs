﻿using ConfigurationReader.ViewModels;
using System.Windows;

namespace ConfigurationReader
{
    public partial class MainWindow : Window
    {
        public MainWindow() 
        { 
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
