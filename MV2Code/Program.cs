using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MV2 = Mijyuoon.Crypto.MV2;

namespace MV2Code {
    class Program {
        static void GenerateKey(string kfile) {
            var key = MV2.Key.Generate(MV2.KeySize.K256);
            File.WriteAllBytes(kfile, key);
        }

        static void EncryptFile(string inkey, string infile, string outflag, string outres) {
            var key = new MV2.Key(File.ReadAllBytes(inkey));
            var encoder = new MV2.Encoder(key, rounds: 16);

            var result = encoder.Encode(File.ReadAllBytes(infile));
            File.WriteAllBytes(outflag, result.Flag);
            File.WriteAllBytes(outres, result.Residual);
        }

        static void DecryptFile(string inkey, string inflag, string inres, string outfile) {
            var key = new MV2.Key(File.ReadAllBytes(inkey));
            var decoder = new MV2.Decoder(key);

            var flag = File.ReadAllBytes(inflag);
            var res = File.ReadAllBytes(inres);
            File.WriteAllBytes(outfile, decoder.Decode(flag, res));
        }

        static void CheckFiles(params string[] paths) {
            foreach(var path in paths) {
                if(!File.Exists(path)) {
                    Console.WriteLine("File '{0}' does not exist", path);
                    Environment.Exit(-1);
                }
            }
        }

        static void ShowUsage(string extra = null) {
            if(extra != null) {
                Console.WriteLine(extra);
            }

            Console.WriteLine("Usage:");
            Console.WriteLine("  mv2code -genkey <key>");
            Console.WriteLine("  mv2code -enc <key> <file> <flags> <kernel>");
            Console.WriteLine("  mv2code -dec <key> <flags> <kernel> <file>");

            Environment.Exit(0);
        }

        static void Main(string[] args) {
            if(args.Length < 1) {
                ShowUsage("No option provided");
            }

            switch(args[0]) {
            case "-genkey":
                if(args.Length < 2) {
                    ShowUsage("Too few arguments provided");
                }
                GenerateKey(args[1]);
                break;

            case "-enc":
                if(args.Length < 5) {
                    ShowUsage("Too few arguments provided");
                }
                CheckFiles(args[1], args[2]);
                EncryptFile(args[1], args[2], args[3], args[4]);
                break;

            case "-dec":
                if(args.Length < 5) {
                    ShowUsage("Too few arguments provided");
                }
                CheckFiles(args[1], args[2], args[3]);
                DecryptFile(args[1], args[2], args[3], args[4]);
                break;

            default:
                ShowUsage($"Invalid option '{args[0]}'");
                break;
            }
        }
    }
}
