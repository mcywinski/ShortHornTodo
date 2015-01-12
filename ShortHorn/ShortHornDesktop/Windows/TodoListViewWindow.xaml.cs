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
using System.Windows.Shapes;
using ShortHorn.Desktop.ViewModels;

namespace ShortHorn.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for TodoListViewWindow.xaml
    /// </summary>
    public partial class TodoListViewWindow : Window
    {
        private TodoListViewViewModel todoListViewViewModel;

        public TodoListViewWindow()
        {
            InitializeComponent();
            this.todoListViewViewModel = new TodoListViewViewModel();
            this.DataContext = this.todoListViewViewModel;
        }

        private async void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            await this.todoListViewViewModel.GetTodoLists();
        }
    }
}
