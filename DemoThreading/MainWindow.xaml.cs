﻿using DemoThreading.Classes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoThreading
{
    public partial class MainWindow : Window
    {
        private ClassA classA;
        private ClassB classB;
        public ObservableCollection<int> AllNumbersList { get; set; }
        public ObservableCollection<int> NumbersList { get; set; } // Observable collection for ListView
        private bool _isUpdatingText = false;
        public MainWindow()
        {

            InitializeComponent();
            btnStop.IsEnabled = false;

            AllNumbersList = new ObservableCollection<int>();

            NumbersList = new ObservableCollection<int>();

            // Set the DataContext to allow binding in XAML
            DataContext = this;

            // Initialize ClassA and ClassB
            classA = new ClassA();
            classB = new ClassB();

            AllNumbersList.CollectionChanged += AllNumbersList_CollectionChanged;
            NumbersList.CollectionChanged += MultipleList_CollectionChanged;



            classA.NumberGenerated += OnNumberGenerated;
            classB.DivisibleNumberFound += OnDivisibleNumberFound;
        }
        private void OnNumberGenerated(int number)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllNumbersList.Add(number);
                int inputNumber;
                if (int.TryParse(inputTextBox.Text, out inputNumber) && inputNumber != 0)
                {
                    classB.CheckNumber(number, inputNumber);
                }
            });
        }

        private void OnDivisibleNumberFound(int number)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                NumbersList.Add(number);
            });
        }
        private void AllNumbersList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Use the Dispatcher to ensure ScrollIntoView is called after the UI updates
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    allNumsListView.ScrollIntoView(AllNumbersList[AllNumbersList.Count - 1]);
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        
    }

        private void MultipleList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Use the Dispatcher to ensure ScrollIntoView is called after the UI updates
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    multipleNumsListView.ScrollIntoView(NumbersList[NumbersList.Count - 1]);
                }), System.Windows.Threading.DispatcherPriority.Background);
            }

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputTextBox.Text))
            {
                MessageBox.Show("Enter Number to see the results", "Empty Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AllNumbersList.Clear();
            NumbersList.Clear();
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            classA.GenerateNumbers();
        
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            classA.StopGeneratingNumbers();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            classA.StopGeneratingNumbers();
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            int inputNumber;

            if (_isUpdatingText)
                return;

            //NumbersList.Clear();
            if (!int.TryParse(inputTextBox.Text, out _))
            {
                _isUpdatingText = true;
                // If not an integer, show a warning message
                MessageBox.Show("Please enter a valid integer.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);

                // Clear the TextBox to remove invalid input
                inputTextBox.Text = string.Empty;

                // Move the cursor to the beginning of the TextBox
                inputTextBox.CaretIndex = 0;
                _isUpdatingText = false;

                return;
            }

            if (int.TryParse(inputTextBox.Text, out inputNumber) && inputNumber != 0)
            {
                foreach (var number in AllNumbersList)
                {
                    classB.CheckNumber(number, inputNumber); // Pass the divisor to ClassB for filtering
                }
            }

        }
    }
}