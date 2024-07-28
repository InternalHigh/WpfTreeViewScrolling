using System.ComponentModel;

namespace TreeViewScrolling
{
    internal class ItemViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public ItemViewModel[] Children { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
