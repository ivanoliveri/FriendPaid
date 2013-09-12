using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Notifications
{
    public class CrossDebtsNotificationBuilder
    {
        #region Attributes
        public virtual User buyer { get; set; }
        public virtual User debtor { get; set; }
        public virtual IList<Payment> simplifiedPayments { set; get; }
        public virtual Payment finalPayment { get; set; }
        public virtual Payment initialPayment { get; set; }
        #endregion

        #region Methods

        public CrossDebtsNotificationBuilder(Payment payment)
        {
            simplifiedPayments = new List<Payment>();
            buyer = payment.buyer;
            debtor = payment.debtor;
            initialPayment = payment;
        }

        public void sendNotifications()
        {
            buyer.notifications.Add(new CrossDebtsNotification(buyer, debtor, simplifiedPayments, finalPayment, initialPayment, notificationMessageForBuyer()));
            debtor.notifications.Add(new CrossDebtsNotification(buyer, debtor, simplifiedPayments, finalPayment, initialPayment, notificationMessageForDebtor()));
        }


        public virtual string notificationMessage(User user)
        {
            if (user.id.Equals(initialPayment.buyer.id))
                return notificationMessageForBuyer();
            return notificationMessageForDebtor();

        }

        private string notificationMessageForDebtor()
        {

            /*El miembro %NOMBRE% ha registrado una compra  y te corresponde pagarle $%CANTIDAD%. 
             *Se le ha descontado $ %CANTIDAD2% por la deuda que tenia contigo en relacion a la compra de %DESCRIPCION1%, %DESCRIPCION2%... que ha quedado saldada. Actualmente 
	         *   1)han quedado saldadas todas sus deudas contigo y	
		     *       a-le debes $ %AMOUNTFINAL%  por la compra de %DESCRIPCIONCOMPRAINICIAL%
		     *       b-tambien tu nueva deuda con él/ella
	         *   2)te debe $ %AMOUNTFINAL% por la compra de %DESCRIPCIONX%
             */

            StringBuilder unMensaje = new StringBuilder("El miembro ");
            unMensaje.Append(buyer.name)
                .Append(" ha registrado una compra y te corresponde pagarle $").Append(initialPayment.originalAmount)
                .Append(". Se le ha descontado $" + calculateAmountDifference().ToString())
                .Append(" por la deuda que tenia contigo en relacion a la compra de ")
                .Append(purchasesListToString()).Append(". Actualmente ");
            if (finalPayment == null || initialPayment.buyer.id == finalPayment.buyer.id)
            {
                unMensaje.Append("han quedado saldadas todas sus deudas contigo y ").Append(finalPayment == null
                                                                                                ? "tambien tu nueva deuda con él/ella"
                                                                                                : "le debes $" +
                                                                                                  finalPayment.amount.
                                                                                                      ToString() + " por la compra de " + initialPayment.description);
            }
            else unMensaje.Append("te debe $" + finalPayment.amount + " por la compra de " + finalPayment.description);


            return unMensaje.ToString();
        }

        private string notificationMessageForBuyer()
        {
            /*El miembro %NOMBRE registra una deuda contigo por $%CANTIDAD% por la compra que has registrado.
             *Se te ha descontado $ %CANTIDAD2% por la deuda que tenias con él en relacion a la compra de %DESCRIPCION1%, %DESCRIPCION2%... que ha quedado saldada. Actualmente 
             *   1)han quedado saldadas todas tus deudas con él y	
             *       a-te debe $ %AMOUNTFINAL%  por la compra de %DESCRIPCIONCOMPRAINICIAL%
             *       b-tambien su nueva deuda contigo
             *   2)le debes $ %AMOUNTFINAL% por la compra de %DESCRIPCIONX%
             */
            StringBuilder unMensaje = new StringBuilder("El miembro ");
            unMensaje.Append(debtor.name)
                .Append(" tiene una deuda contigo por $").Append(initialPayment.originalAmount).Append(" por la compra que has registrado.")
                .Append(" Se te ha descontado $" + calculateAmountDifference().ToString())
                .Append(" por la deuda que tenias con él en relacion a la compra de ")
                .Append(purchasesListToString()).Append(". Actualmente ");
            if (finalPayment == null || initialPayment.buyer.id == finalPayment.buyer.id)
            {
                unMensaje.Append("han quedado saldadas todas tus deudas con él y ").Append(finalPayment == null
                                                                                                ? "tambien su nueva deuda contigo."
                                                                                                : "te debe $" +
                                                                                                  finalPayment.amount.
                                                                                                      ToString() + " por la compra de " + initialPayment.description);
            }
            else unMensaje.Append("le debes $" + finalPayment.amount + " por la compra de " + finalPayment.description);

            return unMensaje.ToString();

        }

        private float calculateAmountDifference()
        {
            float result = simplifiedPayments.Sum(payment => payment.originalAmount);

            if (finalPayment != null) result += initialPayment.Equals(finalPayment) ? 0 : finalPayment.originalAmount - finalPayment.amount;

            return result;
        }

        private string purchasesListToString()
        {
            string result = simplifiedPayments.Aggregate("", (current, payment) => current + (payment.description + ", "));
            if(finalPayment!=null)
                if (!initialPayment.buyer.Equals(finalPayment.buyer)) 
                    result += finalPayment.description + ", ";
            return result.Substring(0, result.Length - 2);
        }
        #endregion
    }
}
