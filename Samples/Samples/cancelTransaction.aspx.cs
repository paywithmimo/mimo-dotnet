using System;
using MimoAPI;

namespace Samples
{
    public partial class cancelTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Take cancelation for transaction id given by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAmount_Click(object sender, EventArgs e)
        {
            lblmessage.Text = MimoRestClient.cancelTransaction(txtTransactionId.Text.Trim());
        }
    }
}