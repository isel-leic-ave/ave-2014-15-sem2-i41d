using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

class Program{

	private static void ValueTypePerformance(){
		const int count = 30000000;
		Stopwatch w = Stopwatch.StartNew();
		List<int> l = new List<int>(count);
		for(int i=0; i<count; i++){
			l.Add(i);
			int x = l[i];
		}
		Console.WriteLine("Generic    List<int>: " + w.Elapsed);
		
		w = Stopwatch.StartNew(); 
		ArrayList a = new ArrayList(count); // - expressividade
		for(int i=0; i<count; i++){
			a.Add(i); // Box implícito, pq o parametro é de tipo Object
			int x = (int) a[i]; // Unbox
		}
		Console.WriteLine("NonGeneric ArrayList: " + w.Elapsed);
	}
    
	private static void ReferenceTypePerformance(){
		const int count = 100000000;
		Stopwatch w = Stopwatch.StartNew();
		List<String> l = new List<String>(count);
		for(int i=0; i<count; i++){
			l.Add("X");
			String x = l[i];
		}
		Console.WriteLine("Generic List<String>: " + w.Elapsed);
				
		w = Stopwatch.StartNew(); 
		ArrayList a = new ArrayList(count); // - expressividade
		for(int i=0; i<count; i++){
			a.Add("X");
			String x = (String) a[i]; // - desempenho - robustez;
		}
		Console.WriteLine("NonGeneric ArrayList: " + w.Elapsed);
	}
	static void Main(){
		ValueTypePerformance();
        ReferenceTypePerformance();
	}
}