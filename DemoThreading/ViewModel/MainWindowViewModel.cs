using DemoThreading;
using DemoThreading.Classes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

internal class MainWindowViewModel : ViewModelBase,IDataErrorInfo
{
    private readonly CustomRandomNumberGenerator randomNumberGenerator;
    private readonly FilterNumberMultiples filterNumberClass;


    public event Action ScrollAllNumbersToEnd;
    public event Action ScrollFilteredNumbersToEnd;



    private bool displayMsgBox;

    public ObservableCollection<int> AllNumbersList { get; private set; }
    public ObservableCollection<int> FilteredNumbersList { get; private set; }

    public RelayCommand StartCommand { get; private set; }
    public RelayCommand StopCommand { get; private set; }


    private int? _lowerLimit;
    public int? LowerLimit
    {
        get => _lowerLimit;
        set
        {
            if (_lowerLimit != value)
            {
                _lowerLimit = value;
                OnPropertyChanged();
                UpdateLimits();
            }
        }
    }


    private int? _upperLimit;
    public int? UpperLimit
    {
        get => _upperLimit;
        set
        {
            if (_upperLimit != value)
            {
                _upperLimit = value;
                OnPropertyChanged();
                UpdateLimits();
            }
        }
    }




    private string inputNumber;
    public string InputNumber
    {
        get => inputNumber;
        set
        {
            if (inputNumber != value)
            {
                inputNumber = value;
                OnPropertyChanged();
                OnInputNumberChanged();
            }
        }
    }

    private bool isStartEnabled;
    public bool IsStartEnabled
    {
        get => isStartEnabled;
        set
        {
            if (isStartEnabled != value)
            {
                isStartEnabled = value;
                OnPropertyChanged();
            }
        }
    }


    private bool isStopEnabled;
    public bool IsStopEnabled
    {
        get => isStopEnabled;
        set
        {
            if (isStopEnabled != value)
            {
                isStopEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    public string this[string columnName]
    {
        get
        {
            if (columnName == nameof(LowerLimit))
            {
                if (!LowerLimit.HasValue || LowerLimit < 0)
                {
                    return "Lower limit must be a positive integer.";
                }
            }

            if (columnName == nameof(UpperLimit))
            {
                if (!UpperLimit.HasValue || UpperLimit < 0)
                {
                    return "Upper limit must be a positive integer.";
                }

                if (UpperLimit <= LowerLimit)
                {
                    return "Upper limit must be greater than lower limit.";
                }
            }

            return null; // No error
        }
    }

    public string Error => null; // Not using this for overall error validation

    public MainWindowViewModel()
    {
        randomNumberGenerator = new CustomRandomNumberGenerator();
        filterNumberClass = new FilterNumberMultiples();

        AllNumbersList = new ObservableCollection<int>();
        FilteredNumbersList = new ObservableCollection<int>();

        StartCommand = new RelayCommand(execute=> StartGenerating(), canExecute => IsStartEnabled);
        StopCommand = new RelayCommand(execute => StopGenerating(), canExecute => IsStopEnabled);

        randomNumberGenerator.NumberGenerated += OnNumberGenerated;
        filterNumberClass.DivisibleNumberFound += OnDivisibleNumberFound;

        IsStartEnabled = true;
        IsStopEnabled = false;
    }

   


    private void OnNumberGenerated(int number)
    {
        Application.Current.Dispatcher.Invoke( () =>
        {
            AllNumbersList.Add(number);
            ScrollAllNumbersToEnd?.Invoke();

            if (int.TryParse(InputNumber, out int input) && input != 0)
            {
                filterNumberClass.CheckNumber(number, input);

            }
        });
    }

    private void OnDivisibleNumberFound(int number)
    {

        FilteredNumbersList.Add(number);
        ScrollFilteredNumbersToEnd?.Invoke();
    }

    private void StartGenerating()
    {

            if (UpperLimit <= LowerLimit||!LowerLimit.HasValue || !UpperLimit.HasValue || string.IsNullOrWhiteSpace(InputNumber)) {

            if (!LowerLimit.HasValue)
            {
                MessageBox.Show("Enter Lower Limit to see the results", "Empty Lower Limit", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 

            }
            if (!UpperLimit.HasValue)
            {
                MessageBox.Show("Enter Upper Limit to see the results", "Empty Upper Limit", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            if (UpperLimit <= LowerLimit)
            {
                MessageBox.Show(" Lower Limit must be less than Upper Limit", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (string.IsNullOrWhiteSpace(InputNumber))
            {
                MessageBox.Show("Enter Number to see the results", "Empty Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        AllNumbersList.Clear();
        FilteredNumbersList.Clear();

        IsStartEnabled = false;
        IsStopEnabled = true;
        randomNumberGenerator.GenerateNumbers(LowerLimit.Value,UpperLimit.Value);
    }

    private void StopGenerating()
    {
        IsStartEnabled = true;
        IsStopEnabled = false;
        randomNumberGenerator.StopGeneratingNumbers();
    }

   private void UpdateLimits()
    {
        StopGenerating();
        StartCommand.RaiseCanExecuteChanged();
        StopCommand.RaiseCanExecuteChanged();
    }

    private void OnInputNumberChanged()
    {
       if (displayMsgBox) return;

        if (!int.TryParse(InputNumber, out _))
        {
            displayMsgBox = true;
            MessageBox.Show("Please enter a valid integer.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            InputNumber = string.Empty;
            displayMsgBox = false;
            return;
        }

        if (int.TryParse(InputNumber, out int input) && input != 0)
        {
            foreach (var number in AllNumbersList)
            {
                filterNumberClass.CheckNumber(number, input);
            }
        }
    }

   }
