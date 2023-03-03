using System.Windows.Controls;
using TkompWPF.ViewModels;

namespace TkompWPF.Views
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : UserControl
    {

        dataViewModel viewModel;
        public DataView()
        {
            InitializeComponent();
            viewModel = new dataViewModel();
            //Data binding ViewModel to View
            this.DataContext = viewModel;
        }


    }
}
