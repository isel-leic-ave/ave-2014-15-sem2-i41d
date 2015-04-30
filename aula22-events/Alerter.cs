

delegate string AlertHandler(int id, string label);

class A{

    event AlertHandler Alerter;
}

class App{
    static void Main(){}
}