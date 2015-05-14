class Handlers{
    
    [OperationOrder(2)]
    public static void Dup(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]*=2;
    }   
    [OperationOrder(1)]
    public void Inc(double[] a){
        for(int i = 0; i < a.Length; i++)
            a[i]++;
    }

}