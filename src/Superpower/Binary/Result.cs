﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpower.Binary
{
    public static class Result
    {
        public static Result<T> Empty<T>(BinarySpan remainder) => new Result<T>(remainder, null, null, false);
        public static Result<T> Empty<T>(BinarySpan remainder, string[] expectations) => new Result<T>(remainder, null, expectations, false);
        /// <summary>
        /// An empty result indicating no value could be parsed.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="remainder">The start of un-parsed input.</param>
        /// <param name="errorMessage">Error message to present.</param>
        /// <returns>A result.</returns>
        public static Result<T> Empty<T>(BinarySpan remainder, string errorMessage)
        {
            return new Result<T>(remainder, errorMessage, null, false);
        }

        /// <summary>
        /// A result carrying a successfully-parsed value.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="location">The location corresponding to the beginning of the parsed span.</param>
        /// <param name="remainder">The start of un-parsed input.</param>
        /// <returns>A result.</returns>
        public static Result<T> Value<T>(T value, BinarySpan location, BinarySpan remainder)
        {
            return new Result<T>(value, location, remainder, false);
        }
        /// <summary>
        /// Convert an empty result of one type into another.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <typeparam name="U">The target type.</typeparam>
        /// <param name="result">The value to convert.</param>
        /// <returns>A result of type <typeparamref name="U"/> carrying the same information as <paramref name="result"/>.</returns>
        public static Result<U> CastEmpty<T, U>(Result<T> result)
        {
            return new Result<U>(result.Remainder, result.ErrorMessage, result.Expectations, result.Backtrack);
        }

        /// <summary>
        /// Combine two empty results.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="first">The first value to combine.</param>
        /// <param name="second">The second value to combine.</param>
        /// <returns>A result of type <typeparamref name="T"/> carrying information from both results.</returns>
        public static Result<T> CombineEmpty<T>(Result<T> first, Result<T> second)
        {
            if (first.Remainder != second.Remainder)
                return second;

            var expectations = first.Expectations;
            if (expectations == null)
                expectations = second.Expectations;
            else if (second.Expectations != null)
            {
                expectations = new string[first.Expectations.Length + second.Expectations.Length];
                var i = 0;
                for (; i < first.Expectations.Length; ++i)
                    expectations[i] = first.Expectations[i];
                for (var j = 0; j < second.Expectations.Length; ++i, ++j)
                    expectations[i] = second.Expectations[j];
            }

            return new Result<T>(second.Remainder, second.ErrorMessage, expectations, second.Backtrack);
        }
    }
}
