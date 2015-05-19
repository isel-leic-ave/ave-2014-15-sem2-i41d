using System;

namespace GCCollectIntExample
{
    class MyGCCollectClass
    {
        static void Main()
        {
            Version [] myGCCol = new Version[1];
        
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            // Determine the maximum number of generations the system 
            // garbage collector currently supports.
            Console.WriteLine("The highest generation is {0}", GC.MaxGeneration);
           
        }

        private const long maxGarbage = 1000;
        
        static Version[] MakeSomeGarbage()
        {
            Version[] vts = new Version[maxGarbage];

            for(int i = 0; i < maxGarbage; i++)
            {
                // Create objects and release them to fill up memory 
                // with unused objects.
                vts[i] = new Version();
            }
            return vts;
        }
    }
}