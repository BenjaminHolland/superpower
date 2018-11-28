using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpower.Parsers
{
    public static class Bytes
    {

        public static BinaryParser<Binary.BinarySpan> Single(byte value)
        {
            return input =>
            {
                var result = input.Consume();
                if (result.HasValue && result.Value == value)
                    return Binary.Result.Value(input.Until(result.Remainder), input, result.Remainder);
                return Binary.Result.Empty<Binary.BinarySpan>(input, $"expected {value.ToString("X2")}");
            };
        }
        public static BinaryParser<Binary.BinarySpan> Sequence(byte[] buffer)
        {
            return input =>
            {
                var remainder = input;
                for (int i = 0; i < buffer.Length; i++)
                {
                    var cur = remainder.Consume();
                    if (!cur.HasValue || cur.Value != buffer[i])
                    {
                        if (cur.Location == input) return Binary.Result.Empty<Binary.BinarySpan>(cur.Location, "buffer");
                        return Binary.Result.Empty<Binary.BinarySpan>(cur.Location, "buffer");
                    }
                    remainder = cur.Remainder;
                }
                return Binary.Result.Value(input.Until(remainder), input, remainder);
            };
        }
    }
}
