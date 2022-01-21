using System;
using System.Collections.Generic;

namespace DllCSharp

{
    public class ConvertBytes
    {

        public static void convertByteToBits(List<string> decompressedBits, byte[] bytes)
        {
            foreach (byte element in bytes)
            {
                decompressedBits.Add(Convert.ToString(element, 2).PadLeft(8, '0'));
            }
        }
    }
}
