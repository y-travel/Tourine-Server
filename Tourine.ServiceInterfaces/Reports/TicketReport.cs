using System;
using DevExpress.XtraReports.UI;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Reports
{
    public partial class TicketReport : XtraReport
    {
        public TicketReport()
        {
            InitializeComponent();
        }

        private void ToPersianDate_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //@TODO improve: create ToPersian label component 
            e.Value = DateTime.Parse(e.Value.ToString()).ToPersianDate();
        }
    }
}
