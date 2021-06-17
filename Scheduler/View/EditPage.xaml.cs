using Scheduler.Model;
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
    public partial class EditPage : BasePage
    {
        public EditPage(int dayIndex, Day dayItem)
        {
            InitializeComponent();
            BindingContext = new EditViewModel(dayIndex, dayItem);
        }
    }
}