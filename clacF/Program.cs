namespace clacF
{
    
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            ApplicationConfiguration.Initialize();
            string dllPath = Path.Combine(AppContext.BaseDirectory, Constants.CalculatorDllName);    
            
            if(!File.Exists(dllPath)) {
                MessageBox.Show(
                    text: $"Required library '{Constants.CalculatorDllName}' not found in executable directory!",
                    caption: "Missing Dependency",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                    );
                return;
            }
            Application.Run(new MainForm());
        }
    }
}