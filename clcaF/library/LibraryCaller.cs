using clacF;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace library
{
    internal static class UhOh
    {
        internal static void CrashWithNoSurvivorsFromDll(Form self, Exception ex)
        {
            string msg = ex is DllNotFoundException
                ? $"Core library missing:\n{ex.Message}"
                : $"Core library possibly broken:\n{ex.Message}";
            MessageBox.Show(
                self,
                msg,
                "Fatal Library Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }
    
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
        public static extern void Abs([In] double[] array, int n, [Out] double[] res, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_antiabs",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void AntiAbs([In] double[] array, int n, [Out] double[] res, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_min",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Min([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_max",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Max([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_sqrt_sum",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double SqrtOfSum([In] double[] array, int n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_nthroot_sum",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double NthrtofSum([In] double[] array, int n, int root_n, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_floor",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void Floor([In] double[] array, int n, [Out] double[] res, out short status);
        [DllImport($"{Constants.CalculatorDllName}",
            EntryPoint = "r_arr_1d_ceiling",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void Ceiling([In] double[] array, int n, [Out] double[] res, out short status);
    }
}
