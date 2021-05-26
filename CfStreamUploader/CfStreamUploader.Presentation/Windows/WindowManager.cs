using CfStreamUploader.Presentation.ViewModels;

namespace CfStreamUploader.Presentation.Windows
{
    public static class WindowManager
    {
        public static EditRestrictionWindow EditRestrictionWindow { get; set; }

        public static void OpenEditRestrictionWindow()
        {
            EditRestrictionWindow = new EditRestrictionWindow()
            {
                DataContext = new EditRestrictionViewModel()
            };
            EditRestrictionWindow.ShowDialog();
        }

        public static void CloseEditWindow()
        {
            EditRestrictionWindow.Close();
        }
    }
}