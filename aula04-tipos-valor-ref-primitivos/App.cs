class A{}

struct B{}


class App{
	static void Main(){
		object o1 = null; // utilização do tipo primitivo object
		
		string s1 = "ola"; // utilização do tipo primitivo string
		
		// casting implícito -- cópia de uma variável para outra
		System.Object o2 = s1; // utilização do tipo NÃO primitivo
		
		int n = 5;
		
		// coerção implícita --> gera sempre em IL conv.... (e.g. conv.i8)
		long l = n;
		
		// casting explícito --> gera em IL um castclass
		s1 = (string) o2;
		
		// Faz 2 verificações do tipo
		s1 = null;
		if(o2 is string){ // <=> instanceof do Java --> gera em IL o isinst
			s1 = (string) o2; // --> gera em IL o castclass
		}
		
		// Equivalente às linhas anteriores
		s1 = o2 as string; // gera em IL o isinst
		
		
	}
}