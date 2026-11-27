using clacF;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace library
{
    internal abstract class LibraryCaller;
    internal class ScalarCaller : LibraryCaller
    {
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_add",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Add(double a, double b, out short exitcode);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_sub",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Sub(double a, double b, out short exitcode); 
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_mul",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mul(double a, double b, out short exitcode); 
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_div",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Div(double a, double b, out short exitcode); 
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_mod",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mod(double a, double b, out short exitcode); 
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_modulo",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Modulo(double a, double b, out short exitcode); 
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_sqrt",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Sqrt(double a, out short exitcode);
    }
    internal class Array1DCaller : LibraryCaller
    {
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_sum_nobase",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double SumNoBase([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_sub_nobase",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double SubNoBase([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_mul_nobase",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double MulNoBase([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_div_nobase",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double DivNoBase([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_abs",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Abs([In] double[] array, int n, [Out] double[] res, out short status);
    }
}
