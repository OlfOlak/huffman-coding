using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using static DllCSharp.CharFrequency;

namespace ConsoleAsmTestTypes
{
    public static class Program
    {
        private const String COMPRESSED_FILENAME = "compression.txt";
        private const String DECOMPRESSED_FILENAME = "decompressed.txt";

        public static int[] flag = { 1, 1, 1, 1};
        public static int[] resultData = new int[255];
        
        [DllImport("DllAsm.dll")]
        private static unsafe extern uint CountCharFrequencyAsm(int* a, int* b, int* c, long* d);

        static void Main(string[] args)
        {
            List<HuffmanNode> nodeList;
            int[] readFile = Huffman.getIntsArrayFromFile("original.txt");
            int length = readFile.Length;

            long longVal = Convert.ToInt64(length);
            //unsafe
            //{
            //    long* lengthPtr = &longVal;
            //    fixed (int* aArg1Addr = &readFile[0], aArg2Addr = &flag[0], aResultsAddr = &resultData[0])
            //    {
            //        CountCharFrequencyAsm(aArg1Addr, aArg2Addr, aResultsAddr, lengthPtr);
            //    }

            //}

            // Dla assemblera Huffman.charFrequency = resultData;

            // Dla c#

            Huffman.charFrequency = CountCharFrequency(readFile, resultData);

            nodeList = Huffman.getListFromFile(readFile);

            Huffman.getTreeFromList(nodeList);

            Huffman.setCodeToTheTree("", nodeList[0]);

            Huffman.saveCompressedTree(COMPRESSED_FILENAME);

            byte[] compressedFile = File.ReadAllBytes(COMPRESSED_FILENAME);

            Huffman.convertByteToBits(compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], DECOMPRESSED_FILENAME);
        }

        public static void Run(string libSelected, string filepath)
        {
            // TODO: Wybor libki

            //List<HuffmanNode> nodeList;
            //nodeList = Huffman.getListFromFile(filepath);

            //Huffman.getTreeFromList(nodeList);
            //Huffman.setCodeToTheTree("", nodeList[0]); CountCharFrequency

            //Huffman.saveCompressedTree(COMPRESSED_FILENAME);

            //byte[] compressedFile = File.ReadAllBytes(COMPRESSED_FILENAME);
            //char[] result = new char[compressedFile.Length * 2];

            //CountCharFrequency(Huffman.decompressedBits, compressedFile);

            //Huffman.saveDecompressedTree(nodeList[0], DECOMPRESSED_FILENAME);
        }
    }
}
