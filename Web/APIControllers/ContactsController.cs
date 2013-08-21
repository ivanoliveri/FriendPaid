using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class ContactsController : ApiController
    {
        private readonly IUserService userService;

        public ContactsController(IUserService userService)
        {
            this.userService = userService;
        }
       
        [HttpGet]
        public UsersViewModel SendContactRequest(string username,string usernameToInvite)
        {
            var user = userService.GetByUsername(username);
            var userToSendContactRequest = userService.GetByUsername(usernameToInvite);
            userService.SendContactRequest(user, userToSendContactRequest);
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            viewModel.message = "Se ha enviado la solicitud de contacto satisfactoriamente.";
            viewModel.users.Add(userToSendContactRequest);
            return viewModel;
        }

        [HttpGet]
        public UsersViewModel DeleteContactRequest(string username, string usernameToDeleteRequest)
        {
            var user = userService.GetByUsername(username);
            var userToDeleteRequest = userService.GetByUsername(usernameToDeleteRequest);
            var request = userToDeleteRequest.getContactPendingRequestFrom(user);
            userService.DeleteContactRequest(request);
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            viewModel.message = "Se ha eliminado la solicitud de contacto satisfactoriamente.";
            viewModel.users.Add(userToDeleteRequest);
            return viewModel;
        }

        [HttpGet]
        public NotificationsViewModel RejectContactRequest(string usernameReceiver, string usernameSender)
        {
            var user = userService.GetByUsername(usernameReceiver);
            var userToDeleteRequest = userService.GetByUsername(usernameSender);
            var request = user.getContactPendingRequestFrom(userToDeleteRequest);
            userService.RejectContactRequest(request);
            var viewModel = new NotificationsViewModel();
            viewModel.username = usernameReceiver;
            viewModel.message = "Se ha rechazado la solicitud de contacto satisfactoriamente.";
            return viewModel;
        }

        [HttpGet]
        public string AceptContactRequest(string usernameReceiver, string usernameSender)
        {
            var receiver = userService.GetByUsername(usernameReceiver);
            var sender = userService.GetByUsername(usernameSender);
            userService.AceptContactRequest(receiver.getContactPendingRequestFrom(sender));
            return usernameReceiver;
        }

        [HttpGet]
        public string DeleteContact(string username, string usernameToDelete)
        {
            var user = userService.GetByUsername(username);
            var userToDelete = userService.GetByUsername(usernameToDelete);
            userService.DeleteContact(user, userToDelete);
            return username;
        }

    }
}
