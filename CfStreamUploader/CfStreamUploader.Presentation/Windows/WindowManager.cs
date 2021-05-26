using CfStreamUploader.Presentation.ViewModels;

namespace CfStreamUploader.Presentation.Windows
{
    public static class WindowManager
    {
        public static EditWindow EditWindow { get; set; }

        public static void OpenEditWindow()
        {
            EditWindow = new EditWindow
            {
                DataContext = new EditViewModel()
            };
            EditWindow.ShowDialog();
        }

        public static void CloseEditWindow()
        {
            EditWindow.Close();
        }
    }
}