// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PONTO_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PONTO_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.

#ifdef PONTO_EXPORTS
#define PONTO_API __declspec(dllexport)
#else
#define PONTO_API __declspec(dllimport)
#endif

/*
 * This class is exported from the Ponto.dll
 */
class PONTO_API Ponto {
public:
	int _x, _y;
	Ponto(int x, int y);
	double Ponto::getModule();
	void Ponto::print(char * label);
};
