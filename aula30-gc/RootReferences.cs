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
            
            // MakeSomeGarbage();
            myGCCol = MakeSomeGarbage(); // Store a root reference

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));

            // Determine the best available approximation of the number  
	    // of bytes currently allocated in managed memory.
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            // Perform a collection of generation 0 only.
            GC.Collect(0);

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            // Perform a collection of all generations up to and including 2.
            GC.Collect(2);

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            
            // Make SomeGarbage colectable
            myGCCol = new Version[1];           
            
            GC.Collect(0);
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            
            GC.Collect(1);
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            
            GC.Collect(2);
            Console.WriteLine("Generation: {0}", GC.GetGeneration(myGCCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            
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