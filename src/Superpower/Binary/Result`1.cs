using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpower.Binary
{
    public class Result<T>
    {
        readonly T _value;
        public BinarySpan Location { get; }
        public BinarySpan Remainder { get; }
        public bool HasValue { get; }
        public Position ErrorPosition => HasValue ? Position.Empty : Location.Position;
        public string ErrorMessage { get; }
        public string[] Expectations { get; }
        internal bool IsPartial(BinarySpan from) => from != Remainder;
        internal bool Backtrack { get; set; }
        public T Value => HasValue ? _value : throw new InvalidOperationException("Result has no value");
        internal Result(T value, BinarySpan location, BinarySpan remainder, bool backtrack)
        {
            Location = location;
            Remainder = remainder;
            _value = value;
            HasValue = true;
            ErrorMessage = null;
            Expectations = null;
            Backtrack = backtrack;
        }

        internal Result(BinarySpan location, BinarySpan remainder, string errorMessage, string[] expectations, bool backtrack)
        {
            Location = location;
            Remainder = remainder;
            _value = default(T);
            HasValue = false;
            Expectations = expectations;
            ErrorMessage = errorMessage;
            Backtrack = backtrack;
        }

        internal Result(BinarySpan remainder, string errorMessage, string[] expectations, bool backtrack)
        {
            Location = Remainder = remainder;
            _value = default(T);
            HasValue = false;
            Expectations = expectations;
            ErrorMessage = errorMessage;
            Backtrack = backtrack;
        }
    }
}
