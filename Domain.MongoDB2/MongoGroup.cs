using System.Collections.Generic;

namespace Domain.MongoDB2
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
