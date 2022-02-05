namespace DllCSharp

{
    public class CharFrequency
    {
        public static int[] CountCharFrequency(int[] file, int[] resultFrequency)
        {
            foreach(var bytes in file) {
                resultFrequency[bytes] += 1;
            }

            return resultFrequency;
        }
    }
}
