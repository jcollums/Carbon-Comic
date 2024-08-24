using System;
using System.Windows.Forms;
using System.IO;

namespace CarbonComic
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            if (!File.Exists("CarbonComic\\unrar.dll"))
            {
                MessageBox.Show("unrar.dll was not found; Carbon Comic will be unable to extract cover art for CBR files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (!File.Exists("ICSharpCode.SharpZipLib.dll"))
            {
                MessageBox.Show("ICSharpCode.SharpZipLib.dll was not found; Carbon Comic will be unable to extract cover art for CBZ files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

    }
}
