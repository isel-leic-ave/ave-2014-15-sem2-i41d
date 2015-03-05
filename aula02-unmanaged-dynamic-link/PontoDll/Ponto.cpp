// Ponto.cpp : Defines the exported functions by Ponto.dll
//

#include "Ponto.h"
#include <math.h>
#include <iostream>

/*
 * This is the constructor of a class that has been exported.
 * see Ponto.h for the class definition
 */
Ponto::Ponto(int x, int y){
	this->_x = x;
	this->_y = y;
}

double Ponto::getModule() {	 
	return sqrt((double)_x*_x + _y*_y);
}

void Ponto::print(char * label){
	printf("v2 %s: (x = %d, y = %d)\n", label, _x, _y);
}

