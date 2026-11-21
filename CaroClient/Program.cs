namespace CaroClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[ThreadException] {e.Exception}");
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[UnhandledException] {e.ExceptionObject}");
            };

            Application.Run(new Form1());
        }
    }
}
