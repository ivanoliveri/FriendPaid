using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Purchase
    {

        #region Attributes

        public Member buyer { set; get; }

        public float totalAmount { set; get; }

        public string description { set; get; }

        public Group group { set; get; }

        public List<Member> debtors { set; get; }

        #endregion

        #region Methods

        public float calculateAmountPerMember()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
