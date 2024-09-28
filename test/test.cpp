#include <windows.h>

void foo(void) {
	const char *foo = "asdasdasdasd \n";
	OutputDebugStringA(foo);
	OutputDebugStringA("aaaaa \n aa \n");
}
int CALLBACK WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
	char unsigned a = 1;
	char unsigned *ap;
	ap = &a;
	foo();
}
