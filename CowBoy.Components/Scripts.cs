using System;
using System.Text;
using System.Web.UI;

namespace CowBoy.Components
{
    public static class Scripts
    {
        public static void GetAlert(this Control c, Type t, string Message)
        {
            StringBuilder sbAlert = new StringBuilder();
            sbAlert.Append("alert('");
            sbAlert.Append(Message.Replace("'", @"\'"));
            sbAlert.Append("')");
            ScriptManager.RegisterClientScriptBlock(c, t, "Alert", sbAlert.ToString(), true);
        }

        public static void GetAlertThanRedirect(this Control c, Type t, string Message, string url)
        {
            StringBuilder sbAlert = new StringBuilder();
            sbAlert.Append("alert('");
            sbAlert.Append(Message.Replace("'", @"\'"));
            sbAlert.Append("');");
            sbAlert.Append("window.location='");
            sbAlert.Append(url);
            sbAlert.Append("';");
            ScriptManager.RegisterClientScriptBlock(c, t, "Alert", sbAlert.ToString(), true);
        }
    }
}
