#include <stdio.h> 

using namespace System;

/*
 * a value type
 */
value struct Ponto{
private:
	int x, y;
public:
	Ponto(int x, int y){
		this->x = x;
		this->y = y;
	}
	int Print() {
		return printf("(%d, %d)\n", x, y);
	}

	void Address(){
		printf("address of x field: %d\n", &x);
		printf("address of this: %d\n", this);
	}
};

/*
 * reference to the managed heap
 */
void AddressOf(Object^ o){ 
	IntPtr* p = (IntPtr*)&o;
	printf("address of o: %d\n", *p);
};

/*
 * unmanaged pointer
 */
void AddressOf(void* o){
	printf("address of o: %d\n", o);
};

void main() {
	Ponto p2 = Ponto(5, 7); 
	AddressOf(&p2);
	p2.Address();

	Ponto^ p1 = p2;
	AddressOf(p1);
	p1->Address();

};