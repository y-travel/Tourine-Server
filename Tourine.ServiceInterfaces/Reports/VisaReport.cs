using System;
using DevExpress.XtraReports.UI;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    public partial class VisaReport : XtraReport
    {
        public VisaReport()
        {
            InitializeComponent();
        }

        private void BuyerNameCell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //@TODO we have to create a proper solution to access another data member from inside of current data member
            e.Value = ((VisaReportData)((object[])DataSource)[0]).BuyerNames[(Guid)e.Value];
        }

        private void ToPersianDate_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //@TODO improve: create ToPersian label component 
            e.Value = DateTime.Parse(e.Value.ToString()).ToPersianDate();
        }
    }
}
