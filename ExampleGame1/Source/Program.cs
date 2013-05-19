using System;
using System.Diagnostics;

namespace ExampleGame1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var factory = new MonoGame.Framework.GameFrameworkViewSource<GameMain>();
            Windows.ApplicationModel.Core.CoreApplication.Run(factory);
        }
    }
}
