using NotePad.Models;

namespace NotePad.ViewModels
{
    public class MainViewModel
    {
        /// <summary>
        /// The main document object.
        /// </summary>
        private readonly DocumentModel _document;

        /// <summary>
        /// Editor ViewModel.
        /// </summary>
        public EditorViewModel Editor { get; set; }

        /// <summary>
        /// Manages File.
        /// </summary>
        public FileViewModel File { get; set; }
             
        /// <summary>
        /// Helper ViewModel
        /// </summary>
        public HelpViewModel Help { get; set; }

        /// <summary>
        /// Main View Model Constructor. 
        /// </summary>
        public MainViewModel()
        {
            _document = new DocumentModel();
            Help = new HelpViewModel();
            Editor = new EditorViewModel(_document);
            File = new FileViewModel(_document);
        }
    }
}
