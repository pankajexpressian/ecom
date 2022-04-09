using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }

    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException()
            : base($"One or more validation failure occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(fg => fg.Key, fg => fg.ToArray());
        }
    }

}
