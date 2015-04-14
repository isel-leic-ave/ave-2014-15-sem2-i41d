using System;
using System.Collections.Generic;

public class WordsOccurrences{
    
    private Dictionary<String, int> cache = new Dictionary<String, int>();
    
    public int this[String word]{
        get{
            return cache[word]; 
        }
        set{
            cache[word] = value;
        }
    }
    
    static void Main(){
        WordsOccurrences wc = new WordsOccurrences();
        
        int res = wc["xpto"]; // callvirt WordsOccurrences::get_Item(String)
    }
}