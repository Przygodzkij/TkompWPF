using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TkompWPF.Models;
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
            this.DataContext = viewModel;
        }

        
    }
}
