﻿using FormBotWPF;
using System.Windows;

namespace SportSzeletBotWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new PromViewModel();
        InitializeComponent();
    }
}