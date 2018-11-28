using System;
using Superpower;
using Superpower.Binary;
using Superpower.Parsers;
using System.Buffers;
namespace BinaryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");


            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
            Memory<byte> buffer = new Memory<byte>(new byte[] { 1, 2, 3, 4 });
            var header = Bytes.Sequence(new byte[] { 4, 4, 4, 4 });
            var body = Bytes.Sequence(new byte[] { 1, 2, 3 });
            var tail = Bytes.Sequence(new byte[] { 0, 0, 0, 0 });

            byte[] packet = new byte[] { 4, 4, 4, 4, 1, 2, 3, 0, 0, 0, 0 };
            var parser2 = from head in header
                          from content in body
                          from end in tail
                          select content.Source.Slice(content.Position.Absolute, content.Length);

            var parser = from one in Bytes.Single(1)
                         from two in Bytes.Single(2)
                         from three in Bytes.Single(3)
                         from four in Bytes.Single(4)
                         select BinarySpan.Merge(one, two, three, four);


            var result = parser.TryParse(buffer);
            if (result.HasValue)
            {
                foreach (byte b in result.Value.ToArray())
                {
                    Console.WriteLine(b);
                }
            }
            result = parser2.TryParse(packet.AsMemory());
            if (result.HasValue)
            {
                foreach (byte b in result.Value.ToArray())
                {
                    Console.WriteLine(b);
                }
            }
            else
            {
                Console.WriteLine(result.ErrorPosition.Absolute);
            }
            Console.ReadKey();
        }
    }
}
