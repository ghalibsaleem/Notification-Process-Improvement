using Microsoft.Exchange.WebServices.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.NetHelper
{
    public class EWSClient
    {
        public bool IsConnected { get; set; }

        private string _email;

        public string Email
        {
            get { return _email; }
        }


        private ExchangeService service;

        public async Task<bool> ConnectAsync(string email, string password)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {

                IsConnected = false;
                _email = email;
                service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                service.TraceEnabled = true;
                service.TraceFlags = TraceFlags.All;

                service.Url = new Uri("https://webmail.maersk.net");
                ExchangeCredentials credentials = new WebCredentials(email, password);
                service.Credentials = credentials;
                service.AutodiscoverUrl(email, RedirectionUrlValidationCallback);
                service.KeepAlive = true;
                if(service != null) 
                    IsConnected = true;
                return IsConnected;
            });
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            if (redirectionUri.Scheme == "https")
                result = true;
            return result;
        }


        public async Task<List<EmailMessageEntity>> ReadMailAsync(DateTime pastDays, bool isRead = false, string subject = "Alert Notification PI - Item ID")
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                if (IsConnected == true)
                {
                    FindItemsResults<Item> findResults = service.FindItems(
                        new FolderId(WellKnownFolderName.Inbox, new Mailbox(_email)),
                        new SearchFilter.SearchFilterCollection(LogicalOperator.And, new SearchFilter[] {
                            new SearchFilter.ContainsSubstring(ItemSchema.Subject, subject,ContainmentMode.Substring,ComparisonMode.IgnoreCase),
                            new SearchFilter.ContainsSubstring(EmailMessageSchema.Sender, "noreply@maersk.com",ContainmentMode.Substring,ComparisonMode.IgnoreCase)
                            }),
                        new ItemView(10));
                    List<EmailMessageEntity> messages = new List<EmailMessageEntity>();
                    foreach (Item item in findResults)
                    {
                        item.Load(new PropertySet(ItemSchema.Body,ItemSchema.Subject, ItemSchema.DateTimeReceived));
                        messages.Add(new EmailMessageEntity(item.Subject,item.Body.Text,item.DateTimeReceived));
                    }
                    return messages;
                }
                else
                {
                    throw new Exception("Please Connect first");
                }
            });
        }

        public async Task<bool> SendMailAsync(List<String> toMails, List<String> ccMails, List<String> bccMails, string subject, string body)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                if (IsConnected == true)
                {
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = subject;
                    message.Body = new MessageBody();
                    message.Body.BodyType = BodyType.HTML;
                    message.Body.Text = body;
                    toMails.ForEach(x => message.ToRecipients.Add(x));
                    if(ccMails!=null)
                        ccMails.ForEach(x => message.CcRecipients.Add(x));
                    if (bccMails != null)
                        bccMails.ForEach(x => message.BccRecipients.Add(x));
                    message.SendAndSaveCopy();
                    return true;
                }
                else
                {
                    throw new Exception("Please Connect first");
                }
            });
        }

    }
}

