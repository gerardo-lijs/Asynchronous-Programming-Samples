using AsyncProgressDesktop.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace AsyncProgressDesktop.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.WhenActivated(disposableRegistration =>
            {
                // Values
                this.Bind(ViewModel, viewModel => viewModel.StartNumber, view => view.StartNumberNumericUpDown.Value, value => value, value => (int)value).DisposeWith(disposableRegistration);
                this.Bind(ViewModel, viewModel => viewModel.EndNumber, view => view.EndNumberNumericUpDown.Value, value => value, value => (int)value).DisposeWith(disposableRegistration);
                this.OneWayBind(ViewModel, viewModel => viewModel.ResultText, view => view.ResultTextBlock.Text).DisposeWith(disposableRegistration);

                // Commands
                this.BindCommand(ViewModel, viewModel => viewModel.CalculatePrimeNumbers, view => view.CalculatePrimeNumbersButton).DisposeWith(disposableRegistration);

                // Progress
                this.OneWayBind(ViewModel, viewModel => viewModel.IsCalculating, view => view.CalculatingProgress.Visibility)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, viewModel => viewModel.IsCalculating, view => view.ResultTextBlock.Visibility, 
                    value => value ? System.Windows.Visibility.Collapsed: System.Windows.Visibility.Visible)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
