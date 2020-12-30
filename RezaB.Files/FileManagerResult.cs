using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files
{
    /// <summary>
    /// Encapsulates the result for <see cref="IFileManager"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public class FileManagerResult<T> : IDisposable
    {
        /// <summary>
        /// The result of the operation.
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// Internal exception if any exception thrown in the <see cref="IFileManager"/>.
        /// </summary>
        public Exception InternalException { get; set; }
        /// <summary>
        /// Creates a new result for <see cref="IFileManager"/>.
        /// </summary>
        /// <param name="result">The result data.</param>
        public FileManagerResult(T result)
        {
            Result = result;
        }
        /// <summary>
        /// Creates a new result for <see cref="IFileManager"/>.
        /// </summary>
        /// <param name="exception">The internal exception.</param>
        public FileManagerResult(Exception exception)
        {
            InternalException = exception;
        }
        /// <summary>
        /// Creates a new result for <see cref="IFileManager"/>.
        /// </summary>
        /// <param name="result">The result data.</param>
        /// <param name="exception">The internal exception.</param>
        public FileManagerResult(T result, Exception exception)
        {
            Result = result;
            InternalException = exception;
        }

        public void Dispose()
        {
            if (Result == null)
                return;
            if (Result is IDisposable disposableResult)
            {
                disposableResult.Dispose();
            }
        }
    }
}
