using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using NotePad.Models;

namespace NotePad.ViewModels
{
    public class FileViewModel
    {
        public DocumentModel Document { get; private set; }

        //Toolbar commands
        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }

        public FileViewModel(DocumentModel document)
        {
            Document = document;
            NewCommand = new RelayCommand(NewFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            SaveCommand = new RelayCommand(SaveFile);
            OpenCommand = new RelayCommand(OpenFile);
        }
        public void NewFile()
        {
            Document.FileName = string.Empty;
            Document.FilePath = string.Empty;
            Document.Text = string.Empty;
        }

        private void SaveFile()
        {
            File.WriteAllText(Document.FilePath, Document.Text);
        }

        private void SaveFileAs()
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Text File (*.txt)|*.txt" };
            if (saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(Document.FilePath, Document.Text);
            }
        }

        /// <summary>
        /// Opens File. 
        /// </summary>
        private void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                Document.Text = File.ReadAllText(openFileDialog.FileName);

            }
        }

        /// <summary>
        /// Assigns properties from dialog. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dialog"></param>
        private void DockFile<T>(T dialog) where T : FileDialog
        {
            Document.FilePath = dialog.FileName;
            Document.FileName = dialog.SafeFileName;
        }

    }
}
