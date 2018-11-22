using Microsoft.Win32;

namespace WpfSamples.Helpers
{
    public class FileDialogHelper
    {
        public static string GetSaveFileName(string title, string filter,
                                            string initialDirectory = null)
        {
            return GetFileName(false, title, filter, initialDirectory);
        }

        public static string GetOpenFileName(string title, string filter,
                                             string initialDirectory = null)
        {
            return GetFileName(true, title, filter, initialDirectory);
        }

        private static string GetFileName(bool isOpen, string title, string filter,
                                          string initialDirectory = null)
        {
            FileDialog fd;
            if (isOpen)
                fd = new OpenFileDialog();
            else
                fd = new SaveFileDialog();

            fd.Title = title;
            fd.Filter = filter;
            if (!string.IsNullOrEmpty(initialDirectory))
                fd.InitialDirectory = initialDirectory;

            if ((fd.ShowDialog() == true) && (fd.FileName != null))
                return fd.FileName;

            return null;
        }

        public static string[] GetOpenFileNames(string title, string filter,
                                                string initialDirectory = null)
        {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = title;
            ofd.Filter = filter;
            if (!string.IsNullOrEmpty(initialDirectory))
                ofd.InitialDirectory = initialDirectory;

            if ((ofd.ShowDialog() == true) && (ofd.FileNames != null))
                return ofd.FileNames;

            return null;
        }

        public static string GetFolderName(string title = null,
                                           string initialDirectoty = null,
                                           bool showNewFolderButton = true)
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (title != null) fbd.Description = title;
            if (initialDirectoty != null) fbd.SelectedPath = initialDirectoty;
            fbd.ShowNewFolderButton = showNewFolderButton;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return fbd.SelectedPath;

            return null;
        }

    }
}
