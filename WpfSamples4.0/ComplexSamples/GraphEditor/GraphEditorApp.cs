namespace WpfSamples40.ComplexSamples.GraphEditor
{
    public class GraphEditorApp
    {
        private static GraphEditorApp _instance;
        private int _currentElementIndex;

        private GraphEditorApp()
        {
            SelectionManager = new SelectionManager();
            _currentElementIndex = 1;
        }

        public static GraphEditorApp Instance
        {
            get
            {
                return _instance ?? (_instance = new GraphEditorApp());
            }
        }

        public SelectionManager SelectionManager { get; set; }

        public int GetNewElementIndex()
        {
            var newElementIndex = _currentElementIndex;
            _currentElementIndex++;
            return newElementIndex;
        }
    }
}