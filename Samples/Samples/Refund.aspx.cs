using System;
using MimoAPI;

namespace Samples
{
    public partial class Refund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Take Refund for amount given by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAmount_Click(object sender, EventArgs e)
        {
            lblmessage.Text = MimoRestClient.Refund(txtAmount.Text.Trim(), txtnote.Text.Trim(),txtTransactionId.Text.Trim());
        }
    }
}