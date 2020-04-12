using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Literatura:
 * https://www.codeproject.com/Articles/30180/Simple-Signal-Generator
 * https://stackoverflow.com/questions/12611982/generate-audio-tone-to-sound-card-in-c-or-c-sharp
 */

namespace Synthesizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainLayout());
        }
    }
}
