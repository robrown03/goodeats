using GoodEats.CLI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodEats.CLI
{
    public class App
    {
        private readonly IEnumerable<IOperation> _operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="operations">The operations.</param>
        public App(IEnumerable<IOperation> operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public async Task Run(string[] args)
        {
            if (args.Length > 0 && Enum.TryParse(args[0], true, out OperationType operationType))
            {
                var operation = _operations.FirstOrDefault(x => x.OperationType == operationType);
                await operation.Run();
            }
            else
            {
                Console.WriteLine("Please Enter a Command to excute:");
                Console.WriteLine($"1) {OperationType.LoadData}");
                await Run(new[] { Console.ReadLine() });
            }
        }       
    }
}
