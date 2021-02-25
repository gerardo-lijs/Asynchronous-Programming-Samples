using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTesting
{
    public static class CPU
    {
        /// <summary>
        /// Simulates an expensive CPU operation (all available cores) for the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds for which the CPU expensive operation is simulated</param>
        public static async Task SimulateExpensiveMethodAsync(int seconds = 1)
        {
            using (var cts = new CancellationTokenSource(seconds * 1000))
            {
                try
                {
                    await Task.Run(() =>
                          ExpensiveMethod(cts.Token));
                }
                catch (OperationCanceledException)
                {
                    // Expected exception in normal method flow
                }
            }
        }

        /// <summary>
        /// Simulates an expensive CPU operation (all available cores) for the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds for which the CPU expensive operation is simulated</param>
        public static void SimulateExpensiveMethod(int seconds = 1)
        {
            using (var cts = new CancellationTokenSource(seconds * 1000))
            {
                try
                {
                    ExpensiveMethod(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Expected exception in normal method flow
                }
            }
        }

        private static void ExpensiveMethod(CancellationToken cancellationToken) => 
            ParallelEnumerable.Range(1, int.MaxValue).WithCancellation(cancellationToken).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
    }
}
