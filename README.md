# CLCA
`CLCA` is a simple calculator application for testing inoperability between Fortran standard library capabilities and C# WinForm UI framework.

[**lib**](lib) contains the fortran source files and build script compiling the `.dll` library. 
[**clcaF**](clcaF) contains the WinForms frontend which depends on the `BaseMath.dll` based on fortran.