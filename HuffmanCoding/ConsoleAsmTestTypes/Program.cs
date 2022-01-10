using System;
using System.Runtime.InteropServices;

namespace ConsoleAsmTestTypes
{
    class Program
    {
        //Zadanie 1
        [DllImport("DllAsm.dll")]   
        private static unsafe extern uint zad1(uint* array, uint length); 

        //Zadanie 2
        [DllImport("DllAsm.dll")]
        private static unsafe extern void zad2(StructZad2* pStrZad2);

        public static StructZad2 structZad2 = new StructZad2();
        public static float[] tab1 = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f, 11.0f, 12.0f, 13.0f, 14.0f, 15.0f, 16.0f };
        public static float[] tab2 = new float[] { 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f };
        public static float[] tab3 = new float[16];

        public unsafe struct StructZad2
        {
            public float* ptrA1;
            public float* ptrA2;
            public float* ptrA3;
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Huffman compression");

			string str = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent cursus risus nec velit imperdiet facilisis. Nullam feugiat tincidunt commodo. Mauris sed sagittis enim. Quisque sed dictum eros, id porttitor augue. Aenean sit amet semper arcu. Donec et tristique erat, sit amet fermentum nunc. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent vehicula, tellus id scelerisque auctor, velit sapien blandit lacus, quis porttitor eros est id quam. Vestibulum sollicitudin nulla eu nisi sodales, eu pellentesque lacus mattis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam eu hendrerit quam. Quisque vestibulum ultricies purus. Suspendisse potenti. Sed condimentum elit ac urna dictum euismod. Nunc eget velit nec est porta imperdiet. " + 
				"Nam iaculis molestie quam in venenatis.Sed id tortor sed ipsum tincidunt laoreet at ut magna. Duis sit amet leo ipsum.Fusce turpis dui, porta rutrum iaculis in, varius in magna.Cras vitae sapien eget mauris scelerisque lobortis vitae quis quam. Interdum et malesuada fames ac ante ipsum primis in faucibus.Quisque ullamcorper risus vel dignissim tempus. Aenean pharetra tincidunt dui at aliquet. Etiam semper at tellus non porttitor. Mauris commodo ante ac nisl porta, in eleifend quam lacinia.Cras velit neque, semper in commodo ut, faucibus vel dui. Duis ac consectetur eros, ut congue nisl. Sed laoreet enim in lacus finibus tempus.Suspendisse sodales urna a leo ornare fringilla.Vestibulum interdum, tellus interdum suscipit rutrum, turpis purus varius lacus, ut sodales arcu augue at justo." + 
				"Praesent mattis ultricies malesuada. Nam pretium lectus a ultrices suscipit. Vestibulum nunc ligula, eleifend id finibus ac, euismod ut odio. Ut finibus, orci nec congue rutrum, lorem mi commodo neque, ut congue metus velit sed urna.Mauris ac neque elit. Mauris dui sapien, fermentum a sagittis eget, tincidunt in orci.Praesent tempus non felis sit amet pharetra.Donec nec orci magna. Etiam vulputate, felis vel efficitur facilisis, nisi dolor aliquet nibh, eget laoreet ligula enim vitae metus.Sed tempor ac tellus in sollicitudin.Mauris suscipit gravida elit a vulputate. Duis fringilla urna at volutpat cursus. Etiam quis libero nec lectus tristique congue.Phasellus quis odio quis metus scelerisque varius." + 
				"Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Ut laoreet libero mi, ac interdum nulla aliquam sit amet.Duis pellentesque dictum ligula ac condimentum. Pellentesque non neque congue, accumsan felis condimentum, aliquam mauris.Suspendisse potenti. Vivamus sagittis tellus et convallis aliquam. Nulla neque orci, tempus sit amet convallis ac, venenatis lobortis magna.Fusce dignissim, quam vel suscipit finibus, mi nisi tempus leo, eget blandit neque ante sit amet mi. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam fermentum gravida metus, eu auctor lacus gravida nec. Aliquam posuere cursus rhoncus." +
				"Cras sagittis elit placerat urna gravida hendrerit.Phasellus suscipit risus ante, sit amet ultricies ligula convallis sit amet. In efficitur, enim sit amet laoreet cursus, enim dolor lobortis dolor, quis congue ipsum magna sed metus. Praesent imperdiet massa at maximus finibus. Nunc aliquet quis mauris eu pellentesque. Sed est lectus, fringilla a faucibus vel, pretium in turpis.Sed non arcu nec lectus suscipit aliquam.Suspendisse potenti. Morbi auctor orci turpis, vel hendrerit augue porttitor at. Aenean consequat, lectus in fringilla facilisis, arcu augue egestas ex, ut interdum purus mi in urna.";
			//string str = "This is an example for Huffman coding";
			Console.WriteLine("Original string:");
			Console.WriteLine(str);


			byte[] originalData = System.Text.Encoding.Default.GetBytes(str);
			uint originalDataSize = (uint)str.Length;
			byte[] compressedData = new byte[originalDataSize * (101 / 100) + 320];

			int compressedDataSize = Compress(originalData, compressedData, originalDataSize);

			byte[] decompressedData = new byte[originalDataSize];

			Decompress(compressedData, decompressedData, (uint)compressedDataSize, originalDataSize);

			string decompressedString = System.Text.Encoding.UTF8.GetString(decompressedData);
			Console.WriteLine("Decompressed string:");
			Console.WriteLine(decompressedString);

			Console.WriteLine("Original data size: " + originalDataSize);
			Console.WriteLine("Compressed data size: " + compressedDataSize);

			Console.ReadLine();

			//int decompressedDataSize = (uint)decompressedData.Length;

			//unsafe
			//{
			//    fixed (uint* aAddress = &tab[0])
			//    {
			//        wynik = zad1(aAddress, 20);
			//        Console.WriteLine("Wynik obliczeń w assemblerze: " +wynik);
			//    }
			//}

			//unsafe
			//{
			//    fixed (StructZad2* pStrZad2 = &structZad2)
			//    {
			//        fixed (float* ptr1 = tab1, ptr2 = tab2, ptr3 = tab3)
			//        {
			//            structZad2.ptrA1 = ptr1;
			//            structZad2.ptrA2 = ptr2;
			//            structZad2.ptrA3 = ptr3;
			//            zad2(pStrZad2);  
			//        }
			//    }
			//}

		}

		private const int MAX_TREE_NODES = 511;

		public class BitStream
		{
			public byte[] BytePointer;
			public uint BitPosition;
			public uint Index;
		}

		public struct Symbol
		{
			public int Sym;
			public uint Count;
			public uint Code;
			public uint Bits;
		}

		public class EncodeNode
		{
			public EncodeNode ChildA;
			public EncodeNode ChildB;
			public int Count;
			public int Symbol;
		}

		private static void initBitstream(ref BitStream stream, byte[] buffer)
		{
			stream.BytePointer = buffer;
			stream.BitPosition = 0;
		}

		private static void writeBits(ref BitStream stream, uint x, uint bits)
		{
			byte[] buffer = stream.BytePointer;
			uint bit = stream.BitPosition;
			uint mask = (uint)(1 << (int)(bits - 1));

			for (uint count = 0; count < bits; ++count)
			{
				buffer[stream.Index] = (byte)((buffer[stream.Index] & (0xff ^ (1 << (int)(7 - bit)))) + ((Convert.ToBoolean(x & mask) ? 1 : 0) << (int)(7 - bit)));
				x <<= 1;
				bit = (bit + 1) & 7;

				if (!Convert.ToBoolean(bit))
				{
					++stream.Index;
				}
			}

			stream.BytePointer = buffer;
			stream.BitPosition = bit;
		}

		private static void histogram(byte[] input, Symbol[] sym, uint size)
		{
			int i;
			int index = 0;

			for (i = 0; i < 256; ++i)
			{
				sym[i].Sym = i;
				sym[i].Count = 0;
				sym[i].Code = 0;
				sym[i].Bits = 0;
			}

			for (i = (int)size; Convert.ToBoolean(i); --i, ++index)
			{
				sym[input[index]].Count++;
			}
		}

		private static void storeTree(ref EncodeNode node, Symbol[] sym, ref BitStream stream, uint code, uint bits)
		{
			uint symbolIndex;

			if (node.Symbol >= 0)
			{
				writeBits(ref stream, 1, 1);
				writeBits(ref stream, (uint)node.Symbol, 8);

				for (symbolIndex = 0; symbolIndex < 256; ++symbolIndex)
				{
					if (sym[symbolIndex].Sym == node.Symbol)
						break;
				}

				sym[symbolIndex].Code = code;
				sym[symbolIndex].Bits = bits;
				return;
			}
			else
			{
				writeBits(ref stream, 0, 1);
			}

			storeTree(ref node.ChildA, sym, ref stream, (code << 1) + 0, bits + 1);
			storeTree(ref node.ChildB, sym, ref stream, (code << 1) + 1, bits + 1);
		}

		private static void makeTree(Symbol[] sym, ref BitStream stream)
		{
			EncodeNode[] nodes = new EncodeNode[MAX_TREE_NODES];

			for (int counter = 0; counter < nodes.Length; ++counter)
			{
				nodes[counter] = new EncodeNode();
			}

			EncodeNode node1, node2, root;
			uint i, numSymbols = 0, nodesLeft, nextIndex;

			for (i = 0; i < 256; ++i)
			{
				if (sym[i].Count > 0)
				{
					nodes[numSymbols].Symbol = sym[i].Sym;
					nodes[numSymbols].Count = (int)sym[i].Count;
					nodes[numSymbols].ChildA = null;
					nodes[numSymbols].ChildB = null;
					++numSymbols;
				}
			}

			root = null;
			nodesLeft = numSymbols;
			nextIndex = numSymbols;

			while (nodesLeft > 1)
			{
				node1 = null;
				node2 = null;

				for (i = 0; i < nextIndex; ++i)
				{
					if (nodes[i].Count > 0)
					{
						if (node1 == null || (nodes[i].Count <= node1.Count))
						{
							node2 = node1;
							node1 = nodes[i];
						}
						else if (node2 == null || (nodes[i].Count <= node2.Count))
						{
							node2 = nodes[i];
						}
					}
				}

				root = nodes[nextIndex];
				root.ChildA = node1;
				root.ChildB = node2;
				root.Count = node1.Count + node2.Count;
				root.Symbol = -1;
				node1.Count = 0;
				node2.Count = 0;
				++nextIndex;
				--nodesLeft;
			}

			if (root != null)
			{
				storeTree(ref root, sym, ref stream, 0, 0);
			}
			else
			{
				root = nodes[0];
				storeTree(ref root, sym, ref stream, 0, 1);
			}
		}

		public static int Compress(byte[] input, byte[] output, uint inputSize)
		{
			Symbol[] sym = new Symbol[256];
			Symbol temp;
			BitStream stream = new BitStream();
			uint i, totalBytes, swaps, symbol;

			if (inputSize < 1)
				return 0;

			initBitstream(ref stream, output);
			histogram(input, sym, inputSize);
			makeTree(sym, ref stream);

			do
			{
				swaps = 0;

				for (i = 0; i < 255; ++i)
				{
					if (sym[i].Sym > sym[i + 1].Sym)
					{
						temp = sym[i];
						sym[i] = sym[i + 1];
						sym[i + 1] = temp;
						swaps = 1;
					}
				}
			} while (Convert.ToBoolean(swaps));

			for (i = 0; i < inputSize; ++i)
			{
				symbol = input[i];
				writeBits(ref stream, sym[symbol].Code, sym[symbol].Bits);
			}

			totalBytes = stream.Index;


			if (stream.BitPosition > 0)
			{
				++totalBytes;
			}

			return (int)totalBytes;
		}


		// DECOMPRESSION
		///////////////////////////////////////////////////////////////////////////

		public class DecodeNode
		{
			public DecodeNode ChildA;
			public DecodeNode ChildB;
			public int Symbol;
		}

		private static uint readBit(ref BitStream stream)
		{
			byte[] buffer = stream.BytePointer;
			uint bit = stream.BitPosition;

			uint x = (uint)(Convert.ToBoolean((buffer[stream.Index] & (1 << (int)(7 - bit)))) ? 1 : 0);
			bit = (bit + 1) & 7;

			if (!Convert.ToBoolean(bit))
			{
				++stream.Index;
			}

			stream.BitPosition = bit;

			return x;
		}

		private static uint read8Bits(ref BitStream stream)
		{
			byte[] buffer = stream.BytePointer;
			uint bit = stream.BitPosition;
			uint x = (uint)((buffer[stream.Index] << (int)bit) | (buffer[stream.Index + 1] >> (int)(8 - bit)));
			++stream.Index;

			return x;
		}

		private static DecodeNode recoverTree(DecodeNode[] nodes, ref BitStream stream, ref uint nodenum)
		{
			DecodeNode thisNode;

			thisNode = nodes[nodenum];
			nodenum = nodenum + 1;
			thisNode.Symbol = -1;
			thisNode.ChildA = null;
			thisNode.ChildB = null;

			if (Convert.ToBoolean(readBit(ref stream)))
			{
				thisNode.Symbol = (int)read8Bits(ref stream);
				return thisNode;
			}

			thisNode.ChildA = recoverTree(nodes, ref stream, ref nodenum);
			thisNode.ChildB = recoverTree(nodes, ref stream, ref nodenum);

			return thisNode;
		}

		public static void Decompress(byte[] input, byte[] output, uint inputSize, uint outputSize)
		{
			DecodeNode[] nodes = new DecodeNode[MAX_TREE_NODES];

			for (int counter = 0; counter < nodes.Length; ++counter)
			{
				nodes[counter] = new DecodeNode();
			}

			DecodeNode root, node;
			BitStream stream = new BitStream();
			uint i, nodeCount;
			byte[] buffer;

			if (inputSize < 1)
				return;

			initBitstream(ref stream, input);
			nodeCount = 0;
			root = recoverTree(nodes, ref stream, ref nodeCount);
			buffer = output;

			for (i = 0; i < outputSize; ++i)
			{
				node = root;

				while (node.Symbol < 0)
				{
					if (Convert.ToBoolean(readBit(ref stream)))
						node = node.ChildB;
					else
						node = node.ChildA;
				}

				buffer[i] = (byte)node.Symbol;
			}
		}


	}
}
