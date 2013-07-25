using System;
using System.Text;
using Domain.Utils;

namespace Domain.Notifications
{
    public class PurchaseNotification : Notification
    {
        public PurchaseNotification()
        {
             
        }

        #region Methods

        public PurchaseNotification(Purchase purchase)
        {
            StringBuilder unMensaje = new StringBuilder("El miembro ");
            unMensaje.Append(purchase.buyer.name).Append(" del grupo ")
                     .Append(purchase.group.name).Append(" ha registrado una compra de $")
                     .Append(purchase.totalAmount.ToString()).Append(" en relacion a ")
                     .Append(purchase.description).Append(". Se le deben abonar $")
                     .Append(purchase.calculateAmountPerMember().ToString());


            message = unMensaje.ToString();

            status = NotificationStatus.Unread;

            date = DateTime.Now.Date;

        }

        #endregion

    }
}
