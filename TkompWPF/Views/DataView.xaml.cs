using System.Windows.Controls;
using TkompWPF.ViewModels;

namespace TkompWPF.Views
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : UserControl
    {

        DataViewModel viewModel;
        public DataView()
        {
            InitializeComponent();
            viewModel = new DataViewModel();
            //Data binding ViewModel to View
            this.DataContext = viewModel;
        }


    }
}
