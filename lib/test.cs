using System;
using System.IO.Pipelines;
using System.Runtime.InteropServices;

double[] simple_double_array = {1.1, 2.2, 3.0, 4.0, 1251236.1251};

short exitcode = 0;

Console.WriteLine("r_add:\t" + MathBase.r_add(1.25, 3.24, out exitcode)+$"\tExitcode: {exitcode}");
Console.WriteLine("r_sub:\t" + MathBase.r_sub(2,2, out exitcode)+$"\tExitcode: {exitcode}");
System.Console.WriteLine($"r_mul:\t{MathBase.r_mul(5.1, 10, out exitcode)}\tExitcode: {exitcode}");
System.Console.WriteLine($"r_div:\t{MathBase.r_div(7.5, 2.5, out exitcode)}\tExitcode: {exitcode}");
System.Console.WriteLine($"r0div:\t{MathBase.r_div(10.1, 0, out exitcode)}\tExitcode: {exitcode}");
System.Console.WriteLine($"r_mod:\t{MathBase.i_mod(17, -5, out exitcode)}\tExitcode: {exitcode}");
System.Console.WriteLine($"r_mdl:\t{MathBase.i_modulo(17, -5, out exitcode)}\tExitcode: {exitcode}");
System.Console.WriteLine($"r_arr_1d_sum_nobase:\t{MathBase.r_arr_1d_sum_nobase(simple_double_array, simple_double_array.Length, out exitcode)}\tExitcode: {exitcode}");
double[,] simple_matrix =
{
    {1.0,2.0,3.0},
    {4.0,5.5,6.67}
};
var fortran_cmptbl = ToFortranArray(simple_matrix); 
double[] res_of_cols = new double[3];
MathBase.r_arr_2d_sub_cols(fortran_cmptbl, 2, 3, res_of_cols, out exitcode);
System.Console.WriteLine($"r_arr_2d_sub_cols[2]:\t{res_of_cols[2]}\tExitcode: {exitcode}");
static T[] ToFortranArray<T>(T[,] array)
{
    int rows = array.GetLength(0);
    int cols = array.GetLength(1);

    T[] result = new T[rows * cols];

    for (int j = 0; j < cols; j++)
    {
        for (int i = 0; i < rows; i++)
        {
            result[i + j * rows] = array[i, j];
        }
    }

    return result;
}
static T[,] FromFortranArray<T>(
    T[] array,
    int rows,
    int cols)
{
    if (array.Length != rows * cols)
        throw new ArgumentException("Array dimensions do not match.");

    T[,] result = new T[rows, cols];

    for (int j = 0; j < cols; j++)
    {
        for (int i = 0; i < rows; i++)
        {
            result[i, j] = array[i + j * rows];
        }
    }

    return result;
}
public static class MathBase {
    [DllImport("MathBase.dll",
        EntryPoint = "r_add",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_add(double a, double b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "i_add",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_add(int a, int b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "r_sub",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_sub(double a, double b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "r_mul",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_mul(double a, double b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "r_div",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_div(double a, double b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "i_mod",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_mod(int a, int b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "i_modulo",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern int i_modulo(int a, int b, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "r_arr_1d_sum_nobase",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern double r_arr_1d_sum_nobase([In] double[] array, int n, out short status);

    [DllImport("MathBase.dll",
        EntryPoint = "r_arr_2d_sub_cols",
        CallingConvention = CallingConvention.Cdecl
    )]
    public static extern void r_arr_2d_sub_cols([In] double[] arr, int a, int b, [Out] double[] res, out short status);
}

