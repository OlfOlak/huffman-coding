using System.Collections.Generic;
using System.IO;

namespace ConsoleAsmTestTypes
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<HuffmanNode> nodeList;
            nodeList = Huffman.getListFromFile("original.txt");

            Huffman.getTreeFromList(nodeList);
            Huffman.setCodeToTheTree("", nodeList[0]);

            Huffman.saveCompressedTree("compression.txt");

            byte[] compressedFile = File.ReadAllBytes("compression.txt");
            Huffman.convertByteToBits(compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], "decompressed.txt");
        }
    }
}
