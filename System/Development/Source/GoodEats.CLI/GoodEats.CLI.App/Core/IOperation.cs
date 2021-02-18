using System.Threading.Tasks;

namespace GoodEats.CLI.Core
{
    public interface IOperation
    {
        /// <summary>
        /// Gets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        OperationType OperationType { get; }
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        Task Run();
    }
}
