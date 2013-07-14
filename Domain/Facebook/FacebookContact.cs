using System;

namespace Domain.Facebook
{
    public class FacebookContact
    {
        public virtual int id { set; get; }
        public virtual Int64 facebookId { set; get; }
        public virtual string name { set; get; }

        public FacebookContact()
        {
            
        }
    }
}
