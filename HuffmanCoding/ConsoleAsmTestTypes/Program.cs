using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static DllCSharp.HuffmanCoding;

namespace ConsoleAsmTestTypes
{
    class Program
    {
        //Zadanie 1
        [DllImport("DllAsm.dll")]   
        private static unsafe extern uint encode(); 

        //Zadanie 2
        [DllImport("DllAsm.dll")]
        private static unsafe extern void decode(StructZad2* pStrZad2);

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
            encode();

			byte[] originalData = File.ReadAllBytes("original.txt");
			uint originalDataSize = (uint)originalData.Length;
			byte[] compressedData = new byte[originalDataSize * (101 / 100) + 320];

			int compressedDataSize = Compress(originalData, compressedData, originalDataSize);

			File.WriteAllBytes("compressed.txt", compressedData.Take(compressedDataSize).ToArray());

			//byte[] decompressedData = new byte[originalDataSize];
            List<byte> decompressedData = new List<byte>();

            byte[] compressedFile = File.ReadAllBytes("compressed.txt");

            Decompress(compressedFile, decompressedData, originalDataSize);

            File.WriteAllBytes("decompressed.txt", decompressedData.ToArray());

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

	}
}