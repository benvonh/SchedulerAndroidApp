using Scheduler.View;
using Scheduler.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scheduler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterCommandPage : BasePage
    {
        public MasterCommandPage()
        {
            InitializeComponent();
            BindingContext = new MasterCommandViewModel();
        }
    }
}