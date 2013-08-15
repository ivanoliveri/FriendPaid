using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Notifications;
using Domain.Utils;

namespace Domain
{
    public class Group
    {

        #region Attributes

        public virtual int id { set; get; }

        public virtual string name { set; get; }

        public virtual User administrator { set; get; }

        public virtual IList<User> members { set; get; }

        #endregion

        #region Methods

        public Group()
        {
            members = new List<User>();
        }

        public virtual void createPurchaseNotifications(Purchase purchase)
        {
            foreach (var debtor in purchase.debtors)
            {
                var newPurchaseNotification = new PurchaseNotification(purchase);
                
                debtor.notifications.Add(newPurchaseNotification);
            }
        }

        public virtual void createPaymentNotification(Payment payment)
        {
            var buyer = payment.buyer;
            var newPaymentNotification = new PaymentNotification(payment);
            buyer.notifications.Add(newPaymentNotification);
        }

        public virtual void createPaymentsAfterPurchase(Purchase purchase)
        {

            var debtorsAndBuyer = new List<User>();

            purchase.debtors.ToList().ForEach(user => debtorsAndBuyer.Add(user));

            debtorsAndBuyer.Add(purchase.buyer);

            foreach (var member in debtorsAndBuyer)
            {
                var newPayment = new Payment()
                {
                    amount = purchase.calculateAmountPerMember(),
                    originalAmount = purchase.calculateAmountPerMember(),
                    buyer = purchase.buyer,
                    debtor = member,
                    description = purchase.description,
                    group = this,
                    status = PaymentStatus.Unpaid
                };

                member.addDebt(newPayment);

            }

            purchase.buyer.payments.ElementAt(purchase.buyer.payments.Count - 1).status = PaymentStatus.Paid;

        }
        #endregion

    }
}
