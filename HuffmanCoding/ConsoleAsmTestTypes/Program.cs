using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
