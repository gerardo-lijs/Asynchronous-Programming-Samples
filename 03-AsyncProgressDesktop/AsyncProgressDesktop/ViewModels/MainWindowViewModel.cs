using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AsyncProgressDesktop.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Commands
        public ReactiveCommand<Unit, Unit> CalculatePrimeNumbers { get; }

        private int _StartNumber = 1;
        public int StartNumber
        {
            get => _StartNumber;
            set => this.RaiseAndSetIfChanged(ref _StartNumber, value);
        }

        private int _EndNumber = 5000000;
        public int EndNumber
        {
            get => _EndNumber;
            set => this.RaiseAndSetIfChanged(ref _EndNumber, value);
        }

        private string _ResultText;
        public string ResultText
        {
            get => _ResultText;
            private set => this.RaiseAndSetIfChanged(ref _ResultText, value);
        }

        private readonly ObservableAsPropertyHelper<bool> _IsCalculating;
        public bool IsCalculating => _IsCalculating.Value;

        public MainWindowViewModel()
        {
            // Create command
            CalculatePrimeNumbers = ReactiveCommand.CreateFromTask(CalculatePrimeNumbersImpl);
            CalculatePrimeNumbers.IsExecuting.ToProperty(this, x => x.IsCalculating, out _IsCalculating);
        }

        private async Task CalculatePrimeNumbersImpl()
        {
            int primesCount = await Task.Run(() =>
                 ParallelEnumerable.Range(StartNumber, EndNumber).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            ResultText = $"{primesCount} prime numbers between {StartNumber} and {EndNumber}";
        }
    }
}
