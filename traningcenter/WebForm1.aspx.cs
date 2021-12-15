using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using traningcenter.Models;

namespace traningcenter
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime? searchText = null;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = DateTime.Parse(Request.QueryString["searchText"]);
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
                traningcenterEntities entities = new traningcenterEntities();
                var data = searchText == null ? entities.Instructors_View.ToList()
                : entities.Instructors_View.Where(
                    x => x.date.Value.Month==searchText.Value.Month
                    && x.date.Value.Year ==searchText.Value.Year
                     && x.date.Value.Day == searchText.Value.Day
                    ).ToList();
                ReportDataSource datasource = new ReportDataSource("DataSet1", data);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }

        }
    }
}