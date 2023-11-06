// LFInteractive LLC. (c) 2020-2024 All Rights Reserved
#include "pch.h"
#include <stdio.h>

extern "C" {
	__declspec(dllexport) int __stdcall get_version()
	{
		printf("hello world\n");
		return 69;
	}
}