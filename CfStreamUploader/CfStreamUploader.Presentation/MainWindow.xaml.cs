using System.IO;
using System.Windows;

namespace CfStreamUploader.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            var filename = Path.GetFileName((files[0]));

            this.VideoNameTextBlock.Text = filename;
        }
    }
}