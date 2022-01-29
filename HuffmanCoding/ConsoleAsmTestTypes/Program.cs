using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using static DllCSharp.ConvertBytes;

namespace ConsoleAsmTestTypes
{
    public static class Program
    {
        private const String COMPRESSED_FILENAME = "compression.txt";
        private const String DECOMPRESSED_FILENAME = "decompressed.txt";

        public static float[] aArg1 = { 1, 2, 3, 4 };
        public static float[] aArg2 = { 1, 1, 1, 1 };
        public static float[] aResults = { 0, 0, 0, 0 };

        [DllImport("DllAsm.dll")]
        private static unsafe extern uint ConvertBytes(float* a, float* b, float* result);

        static void Main(string[] args)
        {
            List<HuffmanNode> nodeList;
            nodeList = Huffman.getListFromFile("original.txt");

            Huffman.getTreeFromList(nodeList);
            Huffman.setCodeToTheTree("", nodeList[0]);

            Huffman.saveCompressedTree("compression.txt");

            byte[] compressedFile = File.ReadAllBytes("compression.txt");
            char[] result = new char[compressedFile.Length * 2];
            unsafe
            {
                fixed (float* aArg1Addr = &aArg1[0], aArg2Addr = &aArg2[0], aResultsAddr = &aResults[0])
                {
                    ConvertBytes(aArg1Addr, aArg2Addr, aResultsAddr);
                }

            }

            convertByteToBits(Huffman.decompressedBits, compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], "decompressed.txt");
        }

        public static void Run(string libSelected, string filepath)
        {
            // TODO: Wybor libki

            List<HuffmanNode> nodeList;
            nodeList = Huffman.getListFromFile(filepath);

            Huffman.getTreeFromList(nodeList);
            Huffman.setCodeToTheTree("", nodeList[0]);

            Huffman.saveCompressedTree(COMPRESSED_FILENAME);

            byte[] compressedFile = File.ReadAllBytes(COMPRESSED_FILENAME);
            char[] result = new char[compressedFile.Length * 2];

            convertByteToBits(Huffman.decompressedBits, compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], DECOMPRESSED_FILENAME);
        }
    }
}
