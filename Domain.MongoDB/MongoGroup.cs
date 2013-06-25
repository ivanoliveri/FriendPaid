using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.MongoDB
{
    public class MongoGroup
    {
        #region Attributes

        public string name { set; get; }

        public string administratorName { set; get; }

        public List<string> membersNames { set; get; }

        #endregion
    }
}
