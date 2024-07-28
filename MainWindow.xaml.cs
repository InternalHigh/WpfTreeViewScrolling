using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace TreeViewScrolling
{
    public partial class MainWindow : Window
    {
        private List<ItemViewModel> _itemViewModels;

        public MainWindow()
        {
            InitializeComponent();

            _itemViewModels = new List<ItemViewModel>();

            for (int i = 0; i < 10; i++)
            {
                var children = new List<ItemViewModel>();

                for (var j = 0; j < 20; j++)
                {
                    var children2 = new List<ItemViewModel>();
                    for (var x = 0; x < 5; x++)
                    {
                        children2.Add(new ItemViewModel { Id = x });
                    }

                    children.Add(new ItemViewModel { Id = j, Children = children2.ToArray() });
                }

                _itemViewModels.Add(new ItemViewModel { Id = i, Children = children.ToArray() });
            }

            DataContext = _itemViewModels;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BringIndexPathIntoView(tv, new[] { 9, 15, 4 });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BringIndexPathIntoView(tv, new[] { 1, 2, 3 });
        }

        private void BringIndexPathIntoView(TreeView treeView, int[] indexPath)
        {
            ItemsControl currentItemsControl = treeView;

            foreach (var index in indexPath)
            {
                if (currentItemsControl is TreeViewItem treeViewItem && !treeViewItem.IsExpanded)
                {
                    treeViewItem.IsExpanded = true;
                    treeViewItem.UpdateLayout();
                }

                var virtualizingPanel = GetVirtualizingPanel(currentItemsControl);
                virtualizingPanel.BringIndexIntoViewPublic(index);

                currentItemsControl = (ItemsControl)currentItemsControl.ItemContainerGenerator.ContainerFromIndex(index);
            }
        }

        private static VirtualizingPanel GetVirtualizingPanel(ItemsControl itemsControl)
        {
            return (VirtualizingPanel)itemsControl.GetType().InvokeMember("ItemsHost", BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, itemsControl, null);
        }
    }
}