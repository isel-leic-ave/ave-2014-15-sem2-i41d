using System;

namespace GCCollectIntExample
{
    class MyGCCollectClass
    {
        static void PrintGeneration(object obj){
            Console.WriteLine("Object in generation " + GC.GetGeneration(obj));
        }
    
        static void PrintRunningGC(){
            Console.WriteLine("Running GC...");
            GC.Collect();
        }
    
        static void PrintRunningGC(int n){
            Console.WriteLine("Running GC for generation " + n);
            GC.Collect(n);
        }
    
        static void Main()
        {
            Version [] myGCCol = new Version[1];
        
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            // Determine the maximum number of generations the system 
            // garbage collector currently supports.
            Console.WriteLine("The highest generation is {0}", GC.MaxGeneration);
            PrintGeneration(myGCCol); //geração 0
            
            PrintRunningGC();
            PrintGeneration(myGCCol); //geração 1
            
            PrintRunningGC();
            PrintGeneration(myGCCol); //geração 2
            
            myGCCol = MakeSomeGarbage();
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            PrintGeneration(myGCCol); //geração 0 -- myGCCol refere um novo objecto
            
            PrintRunningGC();
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            PrintRunningGC();
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            PrintGeneration(myGCCol); //geração 2
            
            myGCCol = new Version[1];  // Now Some garbage can be collected
            PrintRunningGC(0);
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            PrintRunningGC(1);
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
            PrintRunningGC(2);
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        }

        private const long maxGarbage = 1000;
        
        static Version[] MakeSomeGarbage()
        {
            Version[] vts = new Version[maxGarbage];

            for(int i = 0; i < maxGarbage; i++)
            {
                vts[i] = new Version();
            }
            return vts;
        }
    }
}