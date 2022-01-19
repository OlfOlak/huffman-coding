using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAsmTestTypes
{
    public static class Huffman
    {
        public static List<byte> compressedBytes = new List<byte>();
        public static List<string> decompressedBits = new List<string>();
        public static long fileSize = 0;

        public static List<HuffmanNode> getListFromFile(string path)
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>();
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                for (int i = 0; i < stream.Length; i++)
                {
                    string read = Convert.ToChar(stream.ReadByte()).ToString();
                    if (nodeList.Exists(x => x.symbol == read))
                    {
                        int index = nodeList.FindIndex(y => y.symbol == read);
                        nodeList[index].frequencyIncrease();
                        nodeList[index].indexes.Add(i);
                    }
                    else
                    {
                        HuffmanNode newItem = new HuffmanNode(read);
                        newItem.indexes.Add(i);
                        nodeList.Add(newItem);
                    }
                }
                fileSize = stream.Length;
                nodeList.Sort();
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static void getTreeFromList(List<HuffmanNode> nodeList)
        {
            while (nodeList.Count > 1)
            {
                HuffmanNode node1 = nodeList[0];
                nodeList.RemoveAt(0); 
                HuffmanNode node2 = nodeList[0]; 
                nodeList.RemoveAt(0);    
                nodeList.Add(new HuffmanNode(node1, node2)); 
                nodeList.Sort();   
            }
        }

        public static void setCodeToTheTree(string code, HuffmanNode Nodes)
        {
            if (Nodes == null)
                return;
            if (Nodes.leftTree == null && Nodes.rightTree == null)
            {
                Nodes.code = code;
                convertBitToBytes(code);
                return;
            }
            setCodeToTheTree(code + "0", Nodes.leftTree);
            setCodeToTheTree(code + "1", Nodes.rightTree);
        }

        private static void convertBitToBytes(string bits)
        {
            var bitsToArray = new BitArray(bits.Select(s => s == '1').ToArray());
            int numBytes = bitsToArray.Length / 8;

            if (bitsToArray.Length % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bitsToArray.Length; i++)
            {
                if (bitsToArray[i])
                {
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));
                }

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            compressedBytes.Add(bytes[0]);
        }

        public static void convertByteToBits(byte[] bytes)
        {
            foreach (byte element in bytes)
            {
                decompressedBits.Add(Convert.ToString(element, 2).PadLeft(8, '0'));
            }
        }

        public static HuffmanNode getNodeFromBits(HuffmanNode nodeList, string searchBitsState)
        {
            if (searchBitsState.Length == 0 || nodeList.isLeaf == true)
            {
                return nodeList;
            }

            if (searchBitsState.Substring(0, 1) == "0")
            {
                return getNodeFromBits(nodeList.leftTree, searchBitsState.Remove(0, 1));
            }
            else
            {
                return getNodeFromBits(nodeList.rightTree, searchBitsState.Remove(0, 1));
            }
        }

        public static void saveCompressedTree(string fileName)
        {
            File.WriteAllBytes(fileName, compressedBytes.ToArray());
        }

        public static void saveDecompressedTree(HuffmanNode nodeList, string fileName)
        {
            string[] resultText = new string[fileSize];
            HuffmanNode searchedNode;
            foreach (var bits in decompressedBits)
            {
                searchedNode = getNodeFromBits(nodeList, bits);
                if (searchedNode != null)
                {
                    foreach (var index in searchedNode.indexes)
                    {
                        resultText[index] = searchedNode.symbol;
                    }
                }
            }

            File.WriteAllText(fileName, String.Join("", resultText.Select(p => p != null ? p.ToString() : "").ToArray()));
        }
    }
}
