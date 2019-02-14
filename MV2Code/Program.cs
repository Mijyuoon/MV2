using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MV2 = Mijyuoon.Crypto.MV2;

namespace MV2Code {
    class Program {
        static void ShowBytes(byte[] data, bool hex) {
            Console.Write($"{data.Length:X4} @ ");

            for(int i = 0; i < data.Length; i++) {
                if(!hex) {
                    string bits = Convert.ToString(data[i], 2);
                    Console.Write($"{bits.PadLeft(8, '0')} ");
                } else {
                    Console.Write($"{data[i]:X2} ");
                }
            }

            Console.WriteLine();
        }

        static void ShowArray(byte[] data, string label) {
            Console.Write($"{label}: ");
            ShowBytes(data, true);

            var str = Encoding.GetEncoding(1252).GetString(data);
            Console.WriteLine($"{str}\n");
        }

        static void Main(string[] args) {
            var key = new MV2.Key(null);        
            var encoder = new MV2.Encoder(key, 16);
            var decoder = new MV2.Decoder(key);

            var data = Encoding.ASCII.GetBytes("Hello world from this shit.");

            var buffer = new byte[32];
            Array.Copy(data, buffer, data.Length);
            var result = encoder.Encode(buffer);

            ShowArray(data, "I");
            ShowArray(result.Flag, "F");
            ShowArray(result.Residual, "R");

            var data2 = decoder.Decode(result.Flag, result.Residual);
            ShowArray(data2, "D");

            Console.ReadKey(true);
        }
    }
}
