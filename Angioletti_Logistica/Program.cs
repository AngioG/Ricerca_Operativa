namespace Angioletti_Logistica
{
    internal static class Program
    {
        public static frm_main? Form { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form = new frm_main();
            Application.Run(Form);
            
        }
    }
}