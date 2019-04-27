using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Mvvm.Calculator.Annotations;
using Wpf.Mvvm.Calculator.Model;
using Wpf.Mvvm.Calculator.View;

namespace Wpf.Mvvm.Calculator.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
 
        Numbers numbers = new Numbers();
        public ViewCalculator ViewCalculatorForm { get; set; }
        
 #region Model Properties
        private string _numb1;

        public string Numb1
        {
            get => _numb1;
            set
            {
                _numb1 = value;
                this.OnPropertyChanged("Numb1");
            }
        }

        private string _numb2;

        public string Numb2
        {
            get => _numb2;
            set
            {
                _numb2 = value;
                this.OnPropertyChanged("Numb2");
            }
        }

        private string _numbResult;

        public string NumbResult
        {
            get => _numbResult;
            set
            {
                _numbResult = value;
                this.OnPropertyChanged("NumbResult");
            }
        }

        private SolidColorBrush _colorForeground;

        public SolidColorBrush ColorForeground
        {
            get => _colorForeground;
            set
            {
                _colorForeground = value;
                this.OnPropertyChanged("ColorForeground");
            }
        }

        private string _error;
        /// <summary>
        /// Important for displaying errors
        /// </summary>
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                this.OnPropertyChanged("Error");
            }
        }

 #endregion

        private bool _canExecute = true;
        /// <summary>
        /// Important for commands
        /// </summary>
        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                if (_canExecute.Equals(value))
                {
                    return;
                }
                _canExecute = value;
            }
        }

        public CalculatorViewModel()
        {
           _additionButtonCommand = new RelayCommand(Addition, param => this._canExecute);
           _subtractionButtonCommand = new RelayCommand(Subtraction, param => this._canExecute);
           _resetButtonCommand = new RelayCommand(Reset, param => this._canExecute);

           _numb1 = "0";
           _numb2 = "0";
           _numbResult = "0";
        }


#region Button Commands
        private ICommand _additionButtonCommand;

        public ICommand AdditionButtonCommand
        {
            get => _additionButtonCommand;
            set => _additionButtonCommand = value;
        }

        private ICommand _subtractionButtonCommand;

        public ICommand SubtractionButtonCommand
        {
            get => _subtractionButtonCommand;
            set => _subtractionButtonCommand = value;
        }

        private ICommand _resetButtonCommand;

        public ICommand ResetButtonCommand
        {
            get => _resetButtonCommand;
            set => _resetButtonCommand = value;
        }

 #endregion

 #region Interface Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
 #endregion

 #region Methods

         /// <summary>
         /// Add Method
         /// </summary>
         /// <param name="obj"></param>
         private void Addition(object obj)
         {
            Error = string.Empty;
            bool result = true;
            if (decimal.TryParse(Numb1, out decimal n1))
            {
                numbers.Number1 = n1;
            }
            else
            {
                Error = $"Error in Addition Method: {Numb1} cannot converted to a decimal number!";
                result = false;
            }

            if (result)
            {
                if (decimal.TryParse(Numb2, out decimal n2))
                {
                    numbers.Number2 = n2;
                }
                else
                {
                    Error = $"Error in Addition Method: {Numb2} cannot converted to a decimal number!";
                    result = false;
                } 
            }

            if (result)
            {
                NumbResult = numbers.Add().ToString();
                SetColor();
            }
            else
            {
                NumbResult = "Error!";
            }
            
         }

         /// <summary>
         /// Substract Method
         /// </summary>
         /// <param name="obj"></param>
         private void Subtraction(object obj)
         {
             Error = string.Empty;
             bool result = true;
             if (decimal.TryParse(Numb1, out decimal n1))
             {
                 numbers.Number1 = n1;
             }
             else
             {
                 Error = $"Error in Subtraction Method: {Numb1} cannot converted to a decimal number!";
                 result = false;
             }

             if (result)
             {
                 if (decimal.TryParse(Numb2, out decimal n2))
                 {
                     numbers.Number2 = n2;
                 }
                 else
                 {
                     Error = $"Error in Subtraction Method: {Numb2} cannot converted to a decimal number!";
                     result = false;
                 } 
             }

             if (result)
             {
                 NumbResult = numbers.Sub().ToString();
                 SetColor();
             }
             else
             {
                 NumbResult = "Error!";
             }
                 
         }

         /// <summary>
         /// Reset User Interface and values to 0 and to black foreground color
         /// </summary>
         /// <param name="obj"></param>
         private void Reset(object obj)
         {
             Numb1 = "0";
             Numb2 = "0";
             NumbResult = "0";
             Error = string.Empty;
             ColorForeground = Brushes.Black;
         }

         private void SetColor()
         {
             ColorForeground = Brushes.Black;
             if (numbers.NumberResult > 0)
             {
                 ColorForeground = Brushes.Green;
             }

             if (numbers.NumberResult < 0)
             {
                 ColorForeground = Brushes.Red;
             }
         }

#endregion

       
    }
}
