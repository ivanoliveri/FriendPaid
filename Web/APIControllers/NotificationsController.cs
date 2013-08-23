using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Domain.Utils;
using Services;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class NotificationsController : ApiController
    {
        private readonly IUserService userService;

        public NotificationsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public NotificationsViewModel Index(string username)
        {
            var notificationsViewModel = new NotificationsViewModel();
            var user = userService.GetByUsername(username);
            notificationsViewModel.username = username;
            notificationsViewModel.unreadNotifications = user.notifications.Where(oneNotification =>
                                                         oneNotification.status.Equals(NotificationStatus.Unread)).ToList();
            notificationsViewModel.pendingContactRequest = user.contactRequests.Where(oneContactRequest => oneContactRequest.status.Equals(RequestStatus.Pending)).ToList();
            return notificationsViewModel;
        }



    }
}
