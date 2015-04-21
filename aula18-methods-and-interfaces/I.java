interface I{ 
    public void m(); // método abstracto e virtual e public
}


class A {
    protected void m(){
    }
}

class B extends A{
    private void m(){
    }
}