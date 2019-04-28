using AsyncCancelDesktop.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace AsyncCancelDesktop.Views
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
                this.OneWayBind(ViewModel, viewModel => viewModel.ResultText, view => view.ResultTextBlock.Text).DisposeWith(disposableRegistration);

                // Commands
                this.BindCommand(ViewModel, viewModel => viewModel.ProcessImages, view => view.ProcessImagesButton).DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, viewModel => viewModel.ProcessImagesParallel, view => view.ProcessImagesParallelButton).DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, viewModel => viewModel.ProcessWeb, view => view.ProcessWebButton).DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, viewModel => viewModel.ProcessWebParallel, view => view.ProcessWebParallelButton).DisposeWith(disposableRegistration);

                // Progress
                this.OneWayBind(ViewModel, viewModel => viewModel.IsCalculating, view => view.CalculatingProgress.Visibility)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, viewModel => viewModel.IsCalculating, view => view.ResultTextBlock.Visibility, 
                    value => value ? System.Windows.Visibility.Collapsed: System.Windows.Visibility.Visible)
                    .DisposeWith(disposableRegistration);

                // Cancel
                this.BindCommand(ViewModel, viewModel => viewModel.CancelProcess, view => view.CancelButton).DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, viewModel => viewModel.IsCalculating, view => view.CancelButton.Visibility)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
