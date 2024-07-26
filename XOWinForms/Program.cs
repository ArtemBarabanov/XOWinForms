using XOSimpleToolkit.ClassicXO.Game;
using XOWinForms.Presenter;

namespace XOWinForms
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

            var xoForm = new XOForm();
            var xoSession = new ClassicXOSession();
            new XOPresenter(xoForm, xoSession);
            Application.Run(xoForm);
        }
    }
}