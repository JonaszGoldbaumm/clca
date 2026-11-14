using System;
using System.Runtime.InteropServices;

Console.WriteLine("r_add:\t" + MathBase.r_add(1.25, 3.24));
Console.WriteLine("r_sub:\t" + MathBase.r_sub(2,2));
System.Console.WriteLine($"r_mul:\t{MathBase.r_mul(5.1, 10)}");
System.Console.WriteLine($"r_div:\t{MathBase.r_div(7.5, 2.5)}");
System.Console.WriteLine($"r0div:\t{MathBase.r_div(10.1, 0)}");
System.Console.WriteLine($"r_mod:\t{MathBase.i_mod(17, -5)}");
System.Console.WriteLine($"r_mdl:\t{MathBase.i_modulo(17, -5)}");

public static class MathBase {
    [DllImport("MathBase.dll",
        EntryPoint = "r_add",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_add(double a, double b);

    [DllImport("MathBase.dll",
        EntryPoint = "i_add",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_add(int a, int b);

    [DllImport("MathBase.dll",
        EntryPoint = "r_sub",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_sub(double a, double b);

    [DllImport("MathBase.dll",
        EntryPoint = "r_mul",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_mul(double a, double b);

    [DllImport("MathBase.dll",
        EntryPoint = "r_div",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_div(double a, double b);

    [DllImport("MathBase.dll",
        EntryPoint = "i_mod",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_mod(int a, int b);

    [DllImport("MathBase.dll",
        EntryPoint = "i_modulo",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_modulo(int a, int b);
}


