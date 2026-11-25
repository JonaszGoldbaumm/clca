using clacF;
using System;
using System.Collections.Generic;
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
}
