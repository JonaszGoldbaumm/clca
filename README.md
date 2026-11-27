# CLCA
`CLCA` is a simple calculator application for testing inoperability between Fortran standard library capabilities and C# WinForm UI framework.

[**lib**](lib) contains the fortran source files and build script compiling the `.dll` library. Build is based on gfortran (ver. `16.2+`) provided by MinGW, compiling into a single shared `.dll`.

[**clcaF**](clcaF) contains the WinForms frontend which depends on the `BaseMath.dll` based on fortran. Application is based on .NET 10 and runs on Windows 7+ based machines.
