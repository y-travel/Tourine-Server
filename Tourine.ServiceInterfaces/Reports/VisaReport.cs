using System;
using DevExpress.XtraReports.UI;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    public partial class VisaReport : XtraReport
    {
        public VisaReport()
        {
            InitializeComponent();
        }

        private void xrTableCell9_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //@TODO we have to create a proper solution to access another data member from inside of current data member
            e.Value = ((VisaReportData)((object[])DataSource)[0]).BuyerNames[(Guid)e.Value];
        }
    }
}
