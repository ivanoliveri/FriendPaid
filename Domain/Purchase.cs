using System;
using System.Collections.Generic;

namespace Domain
{
    public class Purchase
    {

        #region Attributes

        public virtual int id { set; get; }
        
        public virtual User buyer { set; get; }

        public virtual float totalAmount { set; get; }

        public virtual string description { set; get; }

        public virtual Group group { set; get; }

        public virtual List<User> debtors { set; get; }

        #endregion

        #region Methods

        public Purchase()
        {
            debtors = new List<User>();
        }
        
        public virtual float calculateAmountPerMember()
        {
            return totalAmount/(debtors.Count + 1);
        }

        #endregion

    }
}
