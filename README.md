# Asynchronous programming in .NET samples

* [Responsive WPF application demo](01-AsyncDesktop)  
Demo source code of a WPF application to demonstrate responsive UI and the beneficts of using async
* [Scalability and performance in web applications demo](02-AsyncWebAPI)  
Demo source code of two Web API to demonstrate performance of using async with IO-bound and CPU-bound operations
* [Progress in a WPF application demo](03-AsyncProgressDesktop)  
Demo soure code to demonstrate the simple use of a ProgressRing
* [Cancellation in a WPF application demo](04-AsyncCancelDesktop)  
Demo soure code to demonstrate the simple use of CancellationToken
* [Parallel ForEach and Task.WhenAny demo application](05-ParallelDemoDesktop)  
Demo soure code of a simple WPF Desktop Application to demonstrate how you can use Task Parallel Library ForEach for CPU-bound operations and how you can use Task.WhenAny for IO-bound operations  

## Notes
* In WPF desktop sample I didn't use MVVM to have a minimalistic example
* In Progress, Cancellation and Parallel samples source code I used [ReactiveUI](https://reactiveui.net/) as MVVM framework, [MahApps](https://mahapps.com/) and [OpenCVSharp](https://github.com/shimat/opencvsharp) for simulating a CPU-bound expensive operation. If you are interested in this projects it's worth checkin it out since they are a minimalistic example and easy to see how they work.
* The demo applications use .NET Core 3 Preview 4 so if you don't have it already you an get it from [here](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* You may need to enable .NET Core 3 in VS2019 [(howto)](https://visualstudiomagazine.com/articles/2019/03/08/vs-2019-core-tip.aspx)
