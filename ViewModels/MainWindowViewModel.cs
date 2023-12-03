using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace VTCalc.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private double _firstValue;
        private double _secondValue;
        private Operation _operation = Operation.Add;

        /// <summary>
        /// Value shown on screen
        /// </summary>
        public double ShownValue
        {
            get => _secondValue;
            set => this.RaiseAndSetIfChanged(ref _secondValue, value);
        }

        public ReactiveCommand<int, Unit> AddNumberCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveLastNumberCommand { get; }
        public ReactiveCommand<Operation, Unit> ExecuteOperationCommand { get; }
        public MainWindowViewModel()
        {
            AddNumberCommand = ReactiveCommand.Create<int>(AddNumber);

            ExecuteOperationCommand = ReactiveCommand.Create<Operation>(ExecuteOperation);
            
            RemoveLastNumberCommand = ReactiveCommand.Create(RemoveLastNumber);
        }

        public void AddNumber(int value)
        {
            ShownValue = ShownValue * 10d + value;
        }

        public void RemoveLastNumber()
        {
            ShownValue = (int)ShownValue / 10;
        }
        public void ClearScreen()
        {
            ShownValue = 0;
            _operation = Operation.Add;
            _firstValue = 0;
        }

        public void ExecuteOperation(Operation operation)
        {
            Debug.WriteLine("_operation:" + _operation.ToString());

            switch (_operation)
            {
                case Operation.Add:
                    {
                        _firstValue += _secondValue;
                        ShownValue = 0;
                        break;
                    }
                case Operation.Subtract:
                    {
                        _firstValue -= _secondValue;
                        ShownValue = 0;
                        break;
                    }
                case Operation.Multiply:
                    {
                        _firstValue *= _secondValue;
                        ShownValue = 0;
                        break;
                    }
                case Operation.Divide:
                    {
                        _firstValue /= _secondValue;
                        ShownValue = 0;
                        break;
                    }
            }

            if (operation == Operation.Result)
            {
                ShownValue = _firstValue;
                _operation = Operation.Add;
                _firstValue = 0;
            }
            else
            {
                _operation = operation;
            }
        }


        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
