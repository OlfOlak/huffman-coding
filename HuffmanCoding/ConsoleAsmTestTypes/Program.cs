using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using static DllCSharp.ConvertBytes;

namespace ConsoleAsmTestTypes
{
    class Program
    {
        public static float[] aArg1 = { 1, 2, 2, 1 };
        public static float[] aArg2 = { 1, 2, 4, 6 };
        public static float[] aResults = { 0, 0, 0, 0 };

        [DllImport("DllAsm.dll")]
        private static unsafe extern uint ConvertBytes(byte* result, char* result2);

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
                fixed (byte* aArg1Addr = &compressedFile[0])
                {
                    fixed (char* aArg2Addr = &result[0])
                    {
                        ConvertBytes(aArg1Addr, aArg2Addr);
                    }
                }
               
            }

            convertByteToBits(Huffman.decompressedBits, compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], "decompressed.txt");
        }
    }
}
