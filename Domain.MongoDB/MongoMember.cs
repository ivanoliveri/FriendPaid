using System.Collections.Generic;
using Domain.MongoDB.Facebook;
using Domain.Notifications;
using Domain.Requests;

namespace Domain.MongoDB
{
    public class MongoMember
    {

        #region Attributes

        public string username { set; get; }

        public string password { set; get; }

        public string name { set; get; }

        public string lastName { set; get; }

        public string email { set; get; }

        public List<string> contactsNames { set; get; }

        public List<Notification> notifications { set; get; }

        public List<ContactRequest> contactRequests { set; get; }

        public List<string> groupsNames { set; get; }

        public List<Payment> payments { set; get; }

        public List<Purchase> purchases { set; get; }

        public List<FacebookContact> facebookContacts { set; get; }

        #endregion
    }
}
