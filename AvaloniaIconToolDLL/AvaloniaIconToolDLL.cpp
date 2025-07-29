#include "CommandLineArguments.h"

#ifdef __cplusplus

extern "C"
{
	__declspec(dllexport) int __stdcall WriteIconFile(int argc, char** argv)
	{
		CommandLineArguments obj(argc, argv);
		return obj.generateData();
	}
}

#endif