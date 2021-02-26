using OpenCvSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemoDesktop.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Commands
        public ReactiveCommand<Unit, Unit> ProcessImages { get; }
        public ReactiveCommand<Unit, Unit> ProcessImagesParallel { get; }

        public ReactiveCommand<Unit, Unit> ProcessWeb { get; }
        public ReactiveCommand<Unit, Unit> ProcessWebParallel { get; }

        // Cancel
        public ReactiveCommand<Unit, Unit> CancelProcess { get; }
        private CancellationTokenSource _ctsCancel { get; set; }

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
            ProcessImages = ReactiveCommand.CreateFromTask(ProcessImagesImpl);
            ProcessImagesParallel = ReactiveCommand.CreateFromTask(ProcessImagesParallelImpl);

            ProcessWeb = ReactiveCommand.CreateFromTask(ProcessWebImpl);
            ProcessWebParallel = ReactiveCommand.CreateFromTask(ProcessWebParallelImpl);

            // Progress
            _IsCalculating = this.WhenAnyObservable(x => x.ProcessImages.IsExecuting, x => x.ProcessImagesParallel.IsExecuting,
                x => x.ProcessWeb.IsExecuting, x => x.ProcessWebParallel.IsExecuting,
                (processImages, processImagesParallel, processWeb, processWebParallel) => processImages || processImagesParallel || processWeb || processWebParallel)
                .ToProperty(this, x => x.IsCalculating);

            // Cancel command
            CancelProcess = ReactiveCommand.Create(() => _ctsCancel?.Cancel(true));
        }

        private async Task ProcessImagesImpl()
        {
            _ctsCancel = new CancellationTokenSource();

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                await Task.Run(() =>
                {
                    foreach (var filename in System.IO.Directory.EnumerateFiles(Environment.CurrentDirectory, "*.png"))
                    {
                        for (int i = 0; i < 500; i++)
                        {
                            _ctsCancel.Token.ThrowIfCancellationRequested();

                            using var src = new Mat(filename);
                            using var resut = src.Canny(50, 200);
                        }

                        //using (new Window($"src image {filename}", src))
                        //using (new Window($"resut image {filename}", resut))
                        //{
                        //    //Cv2.WaitKey();
                        //}
                    }
                });

                ResultText = $"Process took {sw.Elapsed.TotalSeconds} seconds";
            }
            catch (OperationCanceledException)
            {
                ResultText = "Cancelled";
            }
        }

        private async Task ProcessImagesParallelImpl()
        {
            _ctsCancel = new CancellationTokenSource();

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        Parallel.ForEach(System.IO.Directory.EnumerateFiles(Environment.CurrentDirectory, "*.png"), (filename) =>
                        {
                            for (int i = 0; i < 500; i++)
                            {
                                _ctsCancel.Token.ThrowIfCancellationRequested();

                                using var src = new Mat(filename);
                                using var resut = src.Canny(50, 200);
                            }

                            //using (new Window($"src image {filename}", src))
                            //using (new Window($"resut image {filename}", resut))
                            //{
                            //    //Cv2.WaitKey();
                            //}
                        });
                    }
                    catch (AggregateException ex) when (ex.InnerExceptions.Any(x => x is OperationCanceledException))
                    {
                        // Flat cancellation on any ParallelForEach as a simple Cancel
                        throw new OperationCanceledException();
                    }
                });

                ResultText = $"Process took {sw.Elapsed.TotalSeconds} seconds";
            }
            catch (OperationCanceledException)
            {
                ResultText = "Cancelled";
            }
        }

        private async Task ProcessWebImpl()
        {
            _ctsCancel = new CancellationTokenSource();

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var websites = new List<string>() { "https://www.dotnetfoundation.org", "https://twitter.com/GerardoLijs", "https://github.com/gerardo-lijs" };

                foreach (var web in websites)
                {
                    // Check cancel
                    _ctsCancel.Token.ThrowIfCancellationRequested();

                    // Download web
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        string response = await client.GetStringAsync(web);
                    }
                }

                ResultText = $"Process took {sw.Elapsed.TotalSeconds} seconds";
            }
            catch (OperationCanceledException)
            {
                ResultText = "Cancelled";
            }
        }

        private async Task ProcessWebParallelImpl()
        {
            _ctsCancel = new CancellationTokenSource();

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var websites = new List<string>() { "https://www.dotnetfoundation.org", "https://twitter.com/GerardoLijs", "https://github.com/gerardo-lijs" };

                // Option 1 - Use a local function
                async Task DownloadWeb(string web)
                {
                    // Download web
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        string response = await client.GetStringAsync(web);
                    }

                    // Check cancel
                    _ctsCancel.Token.ThrowIfCancellationRequested();
                }

                // Option 2 - Use a lambda if you prefer
                //Func<string, Task> downloadWeb = async (web) =>
                //{
                //    // Download web
                //    using (var client = new System.Net.Http.HttpClient())
                //    {
                //        string response = await client.GetStringAsync(web);
                //    }

                //    // Check cancel
                //    _ctsCancel.Token.ThrowIfCancellationRequested();
                //};

                // Option 3 - Use a normal Task returning method in the class

                var downloadTasks = websites.Select(x => DownloadWeb(x));
                await Task.WhenAll(downloadTasks);

                ResultText = $"Process took {sw.Elapsed.TotalSeconds} seconds";
            }
            catch (OperationCanceledException)
            {
                ResultText = "Cancelled";
            }
        }
    }
}
