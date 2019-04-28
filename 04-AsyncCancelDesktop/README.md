# Cancellation in a WPF application demo
Demo soure code to demonstrate the simple use of CancellationToken

In this demo we need to cancel a ParallelEnumerable (TPL) so we use the .WithCancellation extensions to pass the CancellationToken

```csharp
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
```
