1. Falta ldarg.0 antes da chamada a "callvirt   instance object FieldInfo::GetValue(object)",
porque o único valor em stack que existe é uma instancia de FieldInfo, faltando o 
argumento que é passado ao GetValue que é do tipo object.

2. a) Falso. Sempre alojado no heap

2. b) False. class A {object f; void SetF(int n){ f = n;} } // o valor de n é boxed e permanece após o fim da execução do método SetF

3.

delegate void NewsworldHandler(String title, String desc, DateTime when);

class NewsworldWrapper {

    Newsworld src = new Newsworld();
    
    public event NewsworldHandler NewsworldEvent{
        add{
            // src.AddSubscriber(value); // value não é compatível com Subscriber
            src.AddSubscriber(new NewsworldHandlerSubscriber(value));
        }
        remove {
            src.RemoveSubscriber(new NewsworldHandlerSubscriber(value));
        }
    }
}

class NewsworldHandlerSubscriber : Subscriber{
    NewsworldHandler handler;
    public NewsworldHandlerSubscriber(NewsworldHandler h){ handler = h; }
    public void occurrence( String title, String desc, DateTime when) {
        handler(title, desc, when);
    }
    public override bool Equals(object other){
        NewsworldHandlerSubscriber otherHandler = other as NewsworldHandlerSubscriber;
        if(otherHandler == null) return false;
        return handler.Equals(otherHandler.handler);
    }
    public override int GetHashCode(){
        return handler.GetHashCode();
    }
}

4.a 

class ProfileAttribute : Attribute { 
   public readonly long maxTicks; 
   public ProfileAttribute(long t) { maxTicks = t; }
}

4.b.

public static void Measure<T>(T obj, Action<MethodInfo, long, bool> result) {
    IEnumerable<MethodInfo> ms = obj.GetType()
        .GetMethods() // Alternativa usar Binding Flags
        .Where(m => !m.IsStatic && m.GetParameters().Length == 0);
    foreach(MethodInfo m in ms){
        ProfileAttribute[] attrs = (ProfileAttribute[]) m.GetCustomAttributes(typeof(ProfileAttribute));
        if(attrs.Length != 0){
            ProfileAttribute a = attrs[0];
            long current = DateTime.Now.Ticks;
            m.invoke(obj, new object[0]);
            long duration = DateTime.Now.Ticks - current;
            result(m, duration, duration > a.maxTicks);
        }
    }
}

4.c.

public static void ShowMeasure<T>(T obj) {
    Measure(obj, 
                (m, duration, exceed) => Console.WriteLine(
                    "Method={0}, ExecutionTime={1}, TimeExceeded={2}",
                    m.Name, duration, exceed)
            );
}

5.a

bool Exists<T>(this IEnumerable<T> source, Func<T, bool> predicate){
    foreach(T item in source) if(predicate(item)) return true;
    return false;
}

5.b

IEnumerable<T> Distinct<T>(this IEnumerable<T> source) {
    HashSet<T> returned = new HashSet<T>();
    foreach(T item in source) {
        if(returned.Add(item))
            yield return item;
    }
}

5.c

IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second) {
    List<T> selected = new List<T>();
    foreach(T item in first) {
        if(second.Exists(other => other.Equals(item)))
            selecetd.Add(item);
    }
    return selected.Distinct();
}

IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second) {
    IEnumerable<T> f = first.Distinct();
    IEnumerable<T> s = second.Distinct();
    foreach(T item in f) {
        if(s.Exists(other => other.Equals(item)))
            yield return item;
    }
}

6....

7.

a) A, B, C, A

b) A, C, C, A

b) A, C, C, C
