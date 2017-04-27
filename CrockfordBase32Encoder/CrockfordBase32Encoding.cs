using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

[assembly: InternalsVisibleTo("CrockfordBaseEncoder.Tests")]
namespace CrockfordBaseEncoder
{
    public class CrockfordBase32Encoding
    {
        private const int Base = 32;
        private const int CheckDigitBase = 37;

        private static readonly IDictionary<int, char> valueEncodings;
        private static readonly IDictionary<int, char> checkDigitEncodings;
        private static readonly IDictionary<char, int> valueDecodings;
        private static readonly IDictionary<char, int> checkDigitDecodings;
        static CrockfordBase32Encoding()
        {
            var symbols = new SymbolDefinitions();
            valueEncodings = symbols.ValueEncodings;
            checkDigitEncodings = symbols.CheckDigitEncodings;
            valueDecodings = symbols.ValueDecodings;
            checkDigitDecodings = symbols.CheckDigitDecodings;
        }

        public string Encode(ulong input, bool includeCheckDigit)
        {
            var chunks = SplitInto5BitChunks(input);
            var characters = chunks.Select(chunk => valueEncodings[chunk]);

            if (includeCheckDigit)
            {
                var checkValue = (int)(input % CheckDigitBase);
                characters = characters.Concat(new [] { checkDigitEncodings[checkValue] });
            }

            return new string(characters.ToArray());
        }

        internal static IEnumerable<byte> SplitInto5BitChunks(ulong input)
        {
            const int bitsPerChunk = 5;
            const int shift = (sizeof (ulong) * 8) - bitsPerChunk; //a ulong is 64bit, - 5 bits per "chunk" gives us 59 bits.
            var chunks = new List<byte>();
            do
            {
                var lastChunk = input << shift >> shift;
                chunks.Insert(0, (byte)lastChunk);
                input >>= bitsPerChunk;
            } while (input > 0);
            return chunks;
        }

        public ulong? Decode(string encodedString, bool treatLastCharacterAsCheckDigit)
        {
            if (encodedString == null)
                throw new ArgumentNullException(nameof(encodedString));

            if (encodedString.Length == 0)
                return null;

            IEnumerable<char> charactersInReverse = encodedString.Reverse().ToArray();

            int? expectedCheckValue = null;
            if (treatLastCharacterAsCheckDigit)
            {
                var checkDigit = charactersInReverse.First();
                if (!checkDigitDecodings.ContainsKey(checkDigit)) return null;
                expectedCheckValue = checkDigitDecodings[checkDigit];

                charactersInReverse = charactersInReverse.Skip(1);
            }

            ulong number = 0;
            ulong currentBase = 1;
            foreach (var character in charactersInReverse)
            {
                if (!valueDecodings.ContainsKey(character)) return null;

                var value = valueDecodings[character];
                number += (ulong)value * currentBase;

                currentBase *= Base;
            }

            if (expectedCheckValue.HasValue
                && (int)(number % CheckDigitBase) != expectedCheckValue)
            {
                return null;
            }

            return number;
        }
    }
}