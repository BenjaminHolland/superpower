using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpower
{
    public delegate Binary.Result<T> BinaryParser<T>(Binary.BinarySpan input);
}
