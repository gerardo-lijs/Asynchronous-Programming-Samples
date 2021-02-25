using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCancelDesktop.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Commands
        public ReactiveCommand<Unit, Unit> CalculatePrimeNumbers { get; }

        // Cancel
        public ReactiveCommand<Unit, Unit> CalculateCancel { get; }
        private CancellationTokenSource _ctsCalculatePrimeNumbers { get; set; }

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

            // TODO: Program another command with this CPU Load code. Add CancellationToken
            //await LoadTesting.CPU.SimulateExpensiveMethodAsync(15);

            // Cancel command
            CalculateCancel = ReactiveCommand.Create(() => _ctsCalculatePrimeNumbers?.Cancel(true));
        }

        private async Task CalculatePrimeNumbersImpl()
        {
            _ctsCalculatePrimeNumbers = new CancellationTokenSource();

            try
            {
                int primesCount = await Task.Run(() =>
                       ParallelEnumerable.Range(StartNumber, EndNumber).WithCancellation(_ctsCalculatePrimeNumbers.Token).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

                ResultText = $"{primesCount} prime numbers between {StartNumber} and {EndNumber}";
            }
            catch (OperationCanceledException)
            {
                ResultText = "Cancelled";
            }
        }
    }
}
