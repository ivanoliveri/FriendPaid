﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Notifications;
using Domain.Utils;

namespace Domain
{
    public class Group
    {

        #region Attributes

        public string name { set; get; }

        public User administrator { set; get; }

        public List<User> members { set; get; }

        #endregion

        #region Methods

        public Group()
        {
            members = new List<User>();
        }

        public void createPurchaseNotifications(Purchase purchase)
        {
            foreach (var debtor in purchase.debtors)
            {
                var newPurchaseNotification = new PurchaseNotification(purchase);
                
                debtor.notifications.Add(newPurchaseNotification);
            }
        }

        public void createPaymentNotification(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void createPaymentsAfterPurchase(Purchase purchase)
        {

            var debtorsAndBuyer = new List<User>();

            purchase.debtors.ForEach(user=> debtorsAndBuyer.Add(user));
           
            debtorsAndBuyer.Add(purchase.buyer);

            foreach (var member in debtorsAndBuyer)
            {
                var newPayment = new Payment()
                                     {
                                         amount = purchase.calculateAmountPerMember(),
                                         buyer = purchase.buyer,
                                         debtor = member,
                                         description = purchase.description,
                                         group = this,
                                         status = PaymentStatus.Unpaid
                                     };

                member.payments.Add(newPayment);

            }

            debtorsAndBuyer.Find(member => member.Equals(purchase.buyer)).payments.ElementAt(0).status= PaymentStatus.Paid;
       
        }

        #endregion

    }
}
