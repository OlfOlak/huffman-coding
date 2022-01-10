using System;
using System.Runtime.InteropServices;
using static DllCSharp.HuffmanCoding;

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
