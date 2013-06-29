using System;
using System.Collections.Generic;

namespace Domain
{
    public class Purchase
    {

        #region Attributes

        public User buyer { set; get; }

        public float totalAmount { set; get; }

        public string description { set; get; }

        public Group group { set; get; }

        public List<User> debtors { set; get; }

        #endregion

        #region Methods

        public float calculateAmountPerMember()
        {
            return totalAmount/(debtors.Count + 1);
        }

        #endregion

    }
}
