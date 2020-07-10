using System.Windows.Input;
using NotePad.Models;
using NotePad.Behaviors;

namespace NotePad.ViewModels
{
    public class EditorViewModel
    {
        public ICommand FormatCommand { get; }
        public ICommand WrapCommand { get; }
        public FormatModel Format { get; set; }
        public DocumentModel Document { get; set; }

        public EditorViewModel(DocumentModel document)
        {
            Document = document;
            Format = new FormatModel();
            FormatCommand = new RelayCommand(OpenStyleDialog);
            WrapCommand = new RelayCommand(ToggleWrap);

            if (App._formatModel != null)
                Format = App._formatModel;

            App._documentModel = document;
            App._formatModel = Format;
        }

        private void OpenStyleDialog()
        {
            var fontDialog = new FontDialog { DataContext = Format };
            fontDialog.ShowDialog();
        }

        private void ToggleWrap()
        {
            if (Format.Wrap == System.Windows.TextWrapping.Wrap)
                Format.Wrap = System.Windows.TextWrapping.NoWrap;
            else
                Format.Wrap = System.Windows.TextWrapping.Wrap;
        }
       
    }

}
