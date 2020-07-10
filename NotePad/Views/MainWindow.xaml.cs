using System.Windows;
namespace NotePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

            int row = EditorBody.GetLineIndexFromCharacterIndex(EditorBody.CaretIndex);
            int col = EditorBody.CaretIndex - EditorBody.GetCharacterIndexFromLineIndex(row);
            CursorInfo.Text = "Line " + (row + 1) + ", Char " + (col + 1);
        }
    }
}
