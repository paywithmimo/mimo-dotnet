using System;
using MimoAPI;

namespace Samples
{
    public partial class MoneyTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Take Money Transfer for amount given by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAmount_Click(object sender, EventArgs e)
        {
            string sTransferParaMeter = "";
            if (txtnote.Text.Trim() != "" || txtnote.Text.Trim() != null)
            {
                sTransferParaMeter += "&notes=" + txtnote.Text.Trim();
            }
            sTransferParaMeter += "&amount=" + txtAmount.Text.Trim();

            lblmessage.Text = MimoOAuth.MoneyTransfer(sTransferParaMeter);
        }
    }
}