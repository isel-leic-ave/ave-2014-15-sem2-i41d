class Handlers{
    
    public static void Dup(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]*=2;
    }   
    
    public void Inc(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]++;
    }

}