﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.Data {
    internal static class TransformTable {
        // LUT for (residual, flag) pairs
        static readonly (BitValue, BitValue)[] EncodeData = new[] {
            (new BitValue(0b___0000, 4), new BitValue(0b_100, 3)), // val = 0x00
            (new BitValue(0b___0001, 4), new BitValue(0b_100, 3)), // val = 0x01
            (new BitValue(0b___0010, 4), new BitValue(0b_100, 3)), // val = 0x02
            (new BitValue(0b___0011, 4), new BitValue(0b_100, 3)), // val = 0x03
            (new BitValue(0b___0100, 4), new BitValue(0b_100, 3)), // val = 0x04
            (new BitValue(0b___0101, 4), new BitValue(0b_100, 3)), // val = 0x05
            (new BitValue(0b___0110, 4), new BitValue(0b_100, 3)), // val = 0x06
            (new BitValue(0b___0111, 4), new BitValue(0b_100, 3)), // val = 0x07
            (new BitValue(0b___1000, 4), new BitValue(0b_100, 3)), // val = 0x08
            (new BitValue(0b___1001, 4), new BitValue(0b_100, 3)), // val = 0x09
            (new BitValue(0b___1010, 4), new BitValue(0b_100, 3)), // val = 0x0A
            (new BitValue(0b___1011, 4), new BitValue(0b_100, 3)), // val = 0x0B
            (new BitValue(0b___1100, 4), new BitValue(0b_100, 3)), // val = 0x0C
            (new BitValue(0b___1101, 4), new BitValue(0b_100, 3)), // val = 0x0D
            (new BitValue(0b___1110, 4), new BitValue(0b_100, 3)), // val = 0x0E
            (new BitValue(0b___1111, 4), new BitValue(0b_100, 3)), // val = 0x0F
            (new BitValue(0b___0000, 4), new BitValue(0b_000, 3)), // val = 0x10
            (new BitValue(0b___0001, 4), new BitValue(0b_000, 3)), // val = 0x11
            (new BitValue(0b___0010, 4), new BitValue(0b_000, 3)), // val = 0x12
            (new BitValue(0b___0011, 4), new BitValue(0b_000, 3)), // val = 0x13
            (new BitValue(0b___0100, 4), new BitValue(0b_000, 3)), // val = 0x14
            (new BitValue(0b___0101, 4), new BitValue(0b_000, 3)), // val = 0x15
            (new BitValue(0b___0110, 4), new BitValue(0b_000, 3)), // val = 0x16
            (new BitValue(0b___0111, 4), new BitValue(0b_000, 3)), // val = 0x17
            (new BitValue(0b___1000, 4), new BitValue(0b_000, 3)), // val = 0x18
            (new BitValue(0b___1001, 4), new BitValue(0b_000, 3)), // val = 0x19
            (new BitValue(0b___1010, 4), new BitValue(0b_000, 3)), // val = 0x1A
            (new BitValue(0b___1011, 4), new BitValue(0b_000, 3)), // val = 0x1B
            (new BitValue(0b___1100, 4), new BitValue(0b_000, 3)), // val = 0x1C
            (new BitValue(0b___1101, 4), new BitValue(0b_000, 3)), // val = 0x1D
            (new BitValue(0b___1110, 4), new BitValue(0b_000, 3)), // val = 0x1E
            (new BitValue(0b___1111, 4), new BitValue(0b_000, 3)), // val = 0x1F
            (new BitValue(0b__00000, 5), new BitValue(0b__10, 2)), // val = 0x20
            (new BitValue(0b__00001, 5), new BitValue(0b__10, 2)), // val = 0x21
            (new BitValue(0b__00010, 5), new BitValue(0b__10, 2)), // val = 0x22
            (new BitValue(0b__00011, 5), new BitValue(0b__10, 2)), // val = 0x23
            (new BitValue(0b__00100, 5), new BitValue(0b__10, 2)), // val = 0x24
            (new BitValue(0b__00101, 5), new BitValue(0b__10, 2)), // val = 0x25
            (new BitValue(0b__00110, 5), new BitValue(0b__10, 2)), // val = 0x26
            (new BitValue(0b__00111, 5), new BitValue(0b__10, 2)), // val = 0x27
            (new BitValue(0b__01000, 5), new BitValue(0b__10, 2)), // val = 0x28
            (new BitValue(0b__01001, 5), new BitValue(0b__10, 2)), // val = 0x29
            (new BitValue(0b__01010, 5), new BitValue(0b__10, 2)), // val = 0x2A
            (new BitValue(0b__01011, 5), new BitValue(0b__10, 2)), // val = 0x2B
            (new BitValue(0b__01100, 5), new BitValue(0b__10, 2)), // val = 0x2C
            (new BitValue(0b__01101, 5), new BitValue(0b__10, 2)), // val = 0x2D
            (new BitValue(0b__01110, 5), new BitValue(0b__10, 2)), // val = 0x2E
            (new BitValue(0b__01111, 5), new BitValue(0b__10, 2)), // val = 0x2F
            (new BitValue(0b__10000, 5), new BitValue(0b__10, 2)), // val = 0x30
            (new BitValue(0b__10001, 5), new BitValue(0b__10, 2)), // val = 0x31
            (new BitValue(0b__10010, 5), new BitValue(0b__10, 2)), // val = 0x32
            (new BitValue(0b__10011, 5), new BitValue(0b__10, 2)), // val = 0x33
            (new BitValue(0b__10100, 5), new BitValue(0b__10, 2)), // val = 0x34
            (new BitValue(0b__10101, 5), new BitValue(0b__10, 2)), // val = 0x35
            (new BitValue(0b__10110, 5), new BitValue(0b__10, 2)), // val = 0x36
            (new BitValue(0b__10111, 5), new BitValue(0b__10, 2)), // val = 0x37
            (new BitValue(0b__11000, 5), new BitValue(0b__10, 2)), // val = 0x38
            (new BitValue(0b__11001, 5), new BitValue(0b__10, 2)), // val = 0x39
            (new BitValue(0b__11010, 5), new BitValue(0b__10, 2)), // val = 0x3A
            (new BitValue(0b__11011, 5), new BitValue(0b__10, 2)), // val = 0x3B
            (new BitValue(0b__11100, 5), new BitValue(0b__10, 2)), // val = 0x3C
            (new BitValue(0b__11101, 5), new BitValue(0b__10, 2)), // val = 0x3D
            (new BitValue(0b__11110, 5), new BitValue(0b__10, 2)), // val = 0x3E
            (new BitValue(0b__11111, 5), new BitValue(0b__10, 2)), // val = 0x3F
            (new BitValue(0b_000000, 6), new BitValue(0b__01, 2)), // val = 0x40
            (new BitValue(0b_000001, 6), new BitValue(0b__01, 2)), // val = 0x41
            (new BitValue(0b_000010, 6), new BitValue(0b__01, 2)), // val = 0x42
            (new BitValue(0b_000011, 6), new BitValue(0b__01, 2)), // val = 0x43
            (new BitValue(0b_000100, 6), new BitValue(0b__01, 2)), // val = 0x44
            (new BitValue(0b_000101, 6), new BitValue(0b__01, 2)), // val = 0x45
            (new BitValue(0b_000110, 6), new BitValue(0b__01, 2)), // val = 0x46
            (new BitValue(0b_000111, 6), new BitValue(0b__01, 2)), // val = 0x47
            (new BitValue(0b_001000, 6), new BitValue(0b__01, 2)), // val = 0x48
            (new BitValue(0b_001001, 6), new BitValue(0b__01, 2)), // val = 0x49
            (new BitValue(0b_001010, 6), new BitValue(0b__01, 2)), // val = 0x4A
            (new BitValue(0b_001011, 6), new BitValue(0b__01, 2)), // val = 0x4B
            (new BitValue(0b_001100, 6), new BitValue(0b__01, 2)), // val = 0x4C
            (new BitValue(0b_001101, 6), new BitValue(0b__01, 2)), // val = 0x4D
            (new BitValue(0b_001110, 6), new BitValue(0b__01, 2)), // val = 0x4E
            (new BitValue(0b_001111, 6), new BitValue(0b__01, 2)), // val = 0x4F
            (new BitValue(0b_010000, 6), new BitValue(0b__01, 2)), // val = 0x50
            (new BitValue(0b_010001, 6), new BitValue(0b__01, 2)), // val = 0x51
            (new BitValue(0b_010010, 6), new BitValue(0b__01, 2)), // val = 0x52
            (new BitValue(0b_010011, 6), new BitValue(0b__01, 2)), // val = 0x53
            (new BitValue(0b_010100, 6), new BitValue(0b__01, 2)), // val = 0x54
            (new BitValue(0b_010101, 6), new BitValue(0b__01, 2)), // val = 0x55
            (new BitValue(0b_010110, 6), new BitValue(0b__01, 2)), // val = 0x56
            (new BitValue(0b_010111, 6), new BitValue(0b__01, 2)), // val = 0x57
            (new BitValue(0b_011000, 6), new BitValue(0b__01, 2)), // val = 0x58
            (new BitValue(0b_011001, 6), new BitValue(0b__01, 2)), // val = 0x59
            (new BitValue(0b_011010, 6), new BitValue(0b__01, 2)), // val = 0x5A
            (new BitValue(0b_011011, 6), new BitValue(0b__01, 2)), // val = 0x5B
            (new BitValue(0b_011100, 6), new BitValue(0b__01, 2)), // val = 0x5C
            (new BitValue(0b_011101, 6), new BitValue(0b__01, 2)), // val = 0x5D
            (new BitValue(0b_011110, 6), new BitValue(0b__01, 2)), // val = 0x5E
            (new BitValue(0b_011111, 6), new BitValue(0b__01, 2)), // val = 0x5F
            (new BitValue(0b_100000, 6), new BitValue(0b__01, 2)), // val = 0x60
            (new BitValue(0b_100001, 6), new BitValue(0b__01, 2)), // val = 0x61
            (new BitValue(0b_100010, 6), new BitValue(0b__01, 2)), // val = 0x62
            (new BitValue(0b_100011, 6), new BitValue(0b__01, 2)), // val = 0x63
            (new BitValue(0b_100100, 6), new BitValue(0b__01, 2)), // val = 0x64
            (new BitValue(0b_100101, 6), new BitValue(0b__01, 2)), // val = 0x65
            (new BitValue(0b_100110, 6), new BitValue(0b__01, 2)), // val = 0x66
            (new BitValue(0b_100111, 6), new BitValue(0b__01, 2)), // val = 0x67
            (new BitValue(0b_101000, 6), new BitValue(0b__01, 2)), // val = 0x68
            (new BitValue(0b_101001, 6), new BitValue(0b__01, 2)), // val = 0x69
            (new BitValue(0b_101010, 6), new BitValue(0b__01, 2)), // val = 0x6A
            (new BitValue(0b_101011, 6), new BitValue(0b__01, 2)), // val = 0x6B
            (new BitValue(0b_101100, 6), new BitValue(0b__01, 2)), // val = 0x6C
            (new BitValue(0b_101101, 6), new BitValue(0b__01, 2)), // val = 0x6D
            (new BitValue(0b_101110, 6), new BitValue(0b__01, 2)), // val = 0x6E
            (new BitValue(0b_101111, 6), new BitValue(0b__01, 2)), // val = 0x6F
            (new BitValue(0b_110000, 6), new BitValue(0b__01, 2)), // val = 0x70
            (new BitValue(0b_110001, 6), new BitValue(0b__01, 2)), // val = 0x71
            (new BitValue(0b_110010, 6), new BitValue(0b__01, 2)), // val = 0x72
            (new BitValue(0b_110011, 6), new BitValue(0b__01, 2)), // val = 0x73
            (new BitValue(0b_110100, 6), new BitValue(0b__01, 2)), // val = 0x74
            (new BitValue(0b_110101, 6), new BitValue(0b__01, 2)), // val = 0x75
            (new BitValue(0b_110110, 6), new BitValue(0b__01, 2)), // val = 0x76
            (new BitValue(0b_110111, 6), new BitValue(0b__01, 2)), // val = 0x77
            (new BitValue(0b_111000, 6), new BitValue(0b__01, 2)), // val = 0x78
            (new BitValue(0b_111001, 6), new BitValue(0b__01, 2)), // val = 0x79
            (new BitValue(0b_111010, 6), new BitValue(0b__01, 2)), // val = 0x7A
            (new BitValue(0b_111011, 6), new BitValue(0b__01, 2)), // val = 0x7B
            (new BitValue(0b_111100, 6), new BitValue(0b__01, 2)), // val = 0x7C
            (new BitValue(0b_111101, 6), new BitValue(0b__01, 2)), // val = 0x7D
            (new BitValue(0b_111110, 6), new BitValue(0b__01, 2)), // val = 0x7E
            (new BitValue(0b_111111, 6), new BitValue(0b__01, 2)), // val = 0x7F
            (new BitValue(0b0000000, 7), new BitValue(0b__11, 2)), // val = 0x80
            (new BitValue(0b0000001, 7), new BitValue(0b__11, 2)), // val = 0x81
            (new BitValue(0b0000010, 7), new BitValue(0b__11, 2)), // val = 0x82
            (new BitValue(0b0000011, 7), new BitValue(0b__11, 2)), // val = 0x83
            (new BitValue(0b0000100, 7), new BitValue(0b__11, 2)), // val = 0x84
            (new BitValue(0b0000101, 7), new BitValue(0b__11, 2)), // val = 0x85
            (new BitValue(0b0000110, 7), new BitValue(0b__11, 2)), // val = 0x86
            (new BitValue(0b0000111, 7), new BitValue(0b__11, 2)), // val = 0x87
            (new BitValue(0b0001000, 7), new BitValue(0b__11, 2)), // val = 0x88
            (new BitValue(0b0001001, 7), new BitValue(0b__11, 2)), // val = 0x89
            (new BitValue(0b0001010, 7), new BitValue(0b__11, 2)), // val = 0x8A
            (new BitValue(0b0001011, 7), new BitValue(0b__11, 2)), // val = 0x8B
            (new BitValue(0b0001100, 7), new BitValue(0b__11, 2)), // val = 0x8C
            (new BitValue(0b0001101, 7), new BitValue(0b__11, 2)), // val = 0x8D
            (new BitValue(0b0001110, 7), new BitValue(0b__11, 2)), // val = 0x8E
            (new BitValue(0b0001111, 7), new BitValue(0b__11, 2)), // val = 0x8F
            (new BitValue(0b0010000, 7), new BitValue(0b__11, 2)), // val = 0x90
            (new BitValue(0b0010001, 7), new BitValue(0b__11, 2)), // val = 0x91
            (new BitValue(0b0010010, 7), new BitValue(0b__11, 2)), // val = 0x92
            (new BitValue(0b0010011, 7), new BitValue(0b__11, 2)), // val = 0x93
            (new BitValue(0b0010100, 7), new BitValue(0b__11, 2)), // val = 0x94
            (new BitValue(0b0010101, 7), new BitValue(0b__11, 2)), // val = 0x95
            (new BitValue(0b0010110, 7), new BitValue(0b__11, 2)), // val = 0x96
            (new BitValue(0b0010111, 7), new BitValue(0b__11, 2)), // val = 0x97
            (new BitValue(0b0011000, 7), new BitValue(0b__11, 2)), // val = 0x98
            (new BitValue(0b0011001, 7), new BitValue(0b__11, 2)), // val = 0x99
            (new BitValue(0b0011010, 7), new BitValue(0b__11, 2)), // val = 0x9A
            (new BitValue(0b0011011, 7), new BitValue(0b__11, 2)), // val = 0x9B
            (new BitValue(0b0011100, 7), new BitValue(0b__11, 2)), // val = 0x9C
            (new BitValue(0b0011101, 7), new BitValue(0b__11, 2)), // val = 0x9D
            (new BitValue(0b0011110, 7), new BitValue(0b__11, 2)), // val = 0x9E
            (new BitValue(0b0011111, 7), new BitValue(0b__11, 2)), // val = 0x9F
            (new BitValue(0b0100000, 7), new BitValue(0b__11, 2)), // val = 0xA0
            (new BitValue(0b0100001, 7), new BitValue(0b__11, 2)), // val = 0xA1
            (new BitValue(0b0100010, 7), new BitValue(0b__11, 2)), // val = 0xA2
            (new BitValue(0b0100011, 7), new BitValue(0b__11, 2)), // val = 0xA3
            (new BitValue(0b0100100, 7), new BitValue(0b__11, 2)), // val = 0xA4
            (new BitValue(0b0100101, 7), new BitValue(0b__11, 2)), // val = 0xA5
            (new BitValue(0b0100110, 7), new BitValue(0b__11, 2)), // val = 0xA6
            (new BitValue(0b0100111, 7), new BitValue(0b__11, 2)), // val = 0xA7
            (new BitValue(0b0101000, 7), new BitValue(0b__11, 2)), // val = 0xA8
            (new BitValue(0b0101001, 7), new BitValue(0b__11, 2)), // val = 0xA9
            (new BitValue(0b0101010, 7), new BitValue(0b__11, 2)), // val = 0xAA
            (new BitValue(0b0101011, 7), new BitValue(0b__11, 2)), // val = 0xAB
            (new BitValue(0b0101100, 7), new BitValue(0b__11, 2)), // val = 0xAC
            (new BitValue(0b0101101, 7), new BitValue(0b__11, 2)), // val = 0xAD
            (new BitValue(0b0101110, 7), new BitValue(0b__11, 2)), // val = 0xAE
            (new BitValue(0b0101111, 7), new BitValue(0b__11, 2)), // val = 0xAF
            (new BitValue(0b0110000, 7), new BitValue(0b__11, 2)), // val = 0xB0
            (new BitValue(0b0110001, 7), new BitValue(0b__11, 2)), // val = 0xB1
            (new BitValue(0b0110010, 7), new BitValue(0b__11, 2)), // val = 0xB2
            (new BitValue(0b0110011, 7), new BitValue(0b__11, 2)), // val = 0xB3
            (new BitValue(0b0110100, 7), new BitValue(0b__11, 2)), // val = 0xB4
            (new BitValue(0b0110101, 7), new BitValue(0b__11, 2)), // val = 0xB5
            (new BitValue(0b0110110, 7), new BitValue(0b__11, 2)), // val = 0xB6
            (new BitValue(0b0110111, 7), new BitValue(0b__11, 2)), // val = 0xB7
            (new BitValue(0b0111000, 7), new BitValue(0b__11, 2)), // val = 0xB8
            (new BitValue(0b0111001, 7), new BitValue(0b__11, 2)), // val = 0xB9
            (new BitValue(0b0111010, 7), new BitValue(0b__11, 2)), // val = 0xBA
            (new BitValue(0b0111011, 7), new BitValue(0b__11, 2)), // val = 0xBB
            (new BitValue(0b0111100, 7), new BitValue(0b__11, 2)), // val = 0xBC
            (new BitValue(0b0111101, 7), new BitValue(0b__11, 2)), // val = 0xBD
            (new BitValue(0b0111110, 7), new BitValue(0b__11, 2)), // val = 0xBE
            (new BitValue(0b0111111, 7), new BitValue(0b__11, 2)), // val = 0xBF
            (new BitValue(0b1000000, 7), new BitValue(0b__11, 2)), // val = 0xC0
            (new BitValue(0b1000001, 7), new BitValue(0b__11, 2)), // val = 0xC1
            (new BitValue(0b1000010, 7), new BitValue(0b__11, 2)), // val = 0xC2
            (new BitValue(0b1000011, 7), new BitValue(0b__11, 2)), // val = 0xC3
            (new BitValue(0b1000100, 7), new BitValue(0b__11, 2)), // val = 0xC4
            (new BitValue(0b1000101, 7), new BitValue(0b__11, 2)), // val = 0xC5
            (new BitValue(0b1000110, 7), new BitValue(0b__11, 2)), // val = 0xC6
            (new BitValue(0b1000111, 7), new BitValue(0b__11, 2)), // val = 0xC7
            (new BitValue(0b1001000, 7), new BitValue(0b__11, 2)), // val = 0xC8
            (new BitValue(0b1001001, 7), new BitValue(0b__11, 2)), // val = 0xC9
            (new BitValue(0b1001010, 7), new BitValue(0b__11, 2)), // val = 0xCA
            (new BitValue(0b1001011, 7), new BitValue(0b__11, 2)), // val = 0xCB
            (new BitValue(0b1001100, 7), new BitValue(0b__11, 2)), // val = 0xCC
            (new BitValue(0b1001101, 7), new BitValue(0b__11, 2)), // val = 0xCD
            (new BitValue(0b1001110, 7), new BitValue(0b__11, 2)), // val = 0xCE
            (new BitValue(0b1001111, 7), new BitValue(0b__11, 2)), // val = 0xCF
            (new BitValue(0b1010000, 7), new BitValue(0b__11, 2)), // val = 0xD0
            (new BitValue(0b1010001, 7), new BitValue(0b__11, 2)), // val = 0xD1
            (new BitValue(0b1010010, 7), new BitValue(0b__11, 2)), // val = 0xD2
            (new BitValue(0b1010011, 7), new BitValue(0b__11, 2)), // val = 0xD3
            (new BitValue(0b1010100, 7), new BitValue(0b__11, 2)), // val = 0xD4
            (new BitValue(0b1010101, 7), new BitValue(0b__11, 2)), // val = 0xD5
            (new BitValue(0b1010110, 7), new BitValue(0b__11, 2)), // val = 0xD6
            (new BitValue(0b1010111, 7), new BitValue(0b__11, 2)), // val = 0xD7
            (new BitValue(0b1011000, 7), new BitValue(0b__11, 2)), // val = 0xD8
            (new BitValue(0b1011001, 7), new BitValue(0b__11, 2)), // val = 0xD9
            (new BitValue(0b1011010, 7), new BitValue(0b__11, 2)), // val = 0xDA
            (new BitValue(0b1011011, 7), new BitValue(0b__11, 2)), // val = 0xDB
            (new BitValue(0b1011100, 7), new BitValue(0b__11, 2)), // val = 0xDC
            (new BitValue(0b1011101, 7), new BitValue(0b__11, 2)), // val = 0xDD
            (new BitValue(0b1011110, 7), new BitValue(0b__11, 2)), // val = 0xDE
            (new BitValue(0b1011111, 7), new BitValue(0b__11, 2)), // val = 0xDF
            (new BitValue(0b1100000, 7), new BitValue(0b__11, 2)), // val = 0xE0
            (new BitValue(0b1100001, 7), new BitValue(0b__11, 2)), // val = 0xE1
            (new BitValue(0b1100010, 7), new BitValue(0b__11, 2)), // val = 0xE2
            (new BitValue(0b1100011, 7), new BitValue(0b__11, 2)), // val = 0xE3
            (new BitValue(0b1100100, 7), new BitValue(0b__11, 2)), // val = 0xE4
            (new BitValue(0b1100101, 7), new BitValue(0b__11, 2)), // val = 0xE5
            (new BitValue(0b1100110, 7), new BitValue(0b__11, 2)), // val = 0xE6
            (new BitValue(0b1100111, 7), new BitValue(0b__11, 2)), // val = 0xE7
            (new BitValue(0b1101000, 7), new BitValue(0b__11, 2)), // val = 0xE8
            (new BitValue(0b1101001, 7), new BitValue(0b__11, 2)), // val = 0xE9
            (new BitValue(0b1101010, 7), new BitValue(0b__11, 2)), // val = 0xEA
            (new BitValue(0b1101011, 7), new BitValue(0b__11, 2)), // val = 0xEB
            (new BitValue(0b1101100, 7), new BitValue(0b__11, 2)), // val = 0xEC
            (new BitValue(0b1101101, 7), new BitValue(0b__11, 2)), // val = 0xED
            (new BitValue(0b1101110, 7), new BitValue(0b__11, 2)), // val = 0xEE
            (new BitValue(0b1101111, 7), new BitValue(0b__11, 2)), // val = 0xEF
            (new BitValue(0b1110000, 7), new BitValue(0b__11, 2)), // val = 0xF0
            (new BitValue(0b1110001, 7), new BitValue(0b__11, 2)), // val = 0xF1
            (new BitValue(0b1110010, 7), new BitValue(0b__11, 2)), // val = 0xF2
            (new BitValue(0b1110011, 7), new BitValue(0b__11, 2)), // val = 0xF3
            (new BitValue(0b1110100, 7), new BitValue(0b__11, 2)), // val = 0xF4
            (new BitValue(0b1110101, 7), new BitValue(0b__11, 2)), // val = 0xF5
            (new BitValue(0b1110110, 7), new BitValue(0b__11, 2)), // val = 0xF6
            (new BitValue(0b1110111, 7), new BitValue(0b__11, 2)), // val = 0xF7
            (new BitValue(0b1111000, 7), new BitValue(0b__11, 2)), // val = 0xF8
            (new BitValue(0b1111001, 7), new BitValue(0b__11, 2)), // val = 0xF9
            (new BitValue(0b1111010, 7), new BitValue(0b__11, 2)), // val = 0xFA
            (new BitValue(0b1111011, 7), new BitValue(0b__11, 2)), // val = 0xFB
            (new BitValue(0b1111100, 7), new BitValue(0b__11, 2)), // val = 0xFC
            (new BitValue(0b1111101, 7), new BitValue(0b__11, 2)), // val = 0xFD
            (new BitValue(0b1111110, 7), new BitValue(0b__11, 2)), // val = 0xFE
            (new BitValue(0b1111111, 7), new BitValue(0b__11, 2)), // val = 0xFF
        };

        static readonly PrefixDecoder<(int, byte[])> FlagDecoder;

        static TransformTable() {
            var flags = new Dictionary<BitValue, (int, byte[])>();

            for(int i = 0; i < EncodeData.Length; i++) {
                var (res, flag) = EncodeData[i];

                // Add residual group for current flag unless already added
                if(!flags.ContainsKey(flag)) {
                    flags.Add(flag, (res.Length, new byte[1 << res.Length]));
                }

                // Fill decoded value for current residual bits
                flags[flag].Item2[res.Value] = (byte)i;
            }

            FlagDecoder = new PrefixDecoder<(int, byte[])>(flags.Select(x => (x.Key, x.Value)));
        }

        public static (BitValue, BitValue) Encode(byte value) => EncodeData[value];

        public static (int, byte[]) DecodeFlag(BitReadStream brs) => FlagDecoder.Decode(brs);
    }
}
