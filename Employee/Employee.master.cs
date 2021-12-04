using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Employee : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {
                hdfEmpName.Value =Convert.ToString(Session["EmpName"]);
                lblEmpName.Text =": "+ hdfEmpName.Value;
            }
        }
    }
}
