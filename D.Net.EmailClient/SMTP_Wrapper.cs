using D.Net.EmailInterfaces;
using LumiSoft.Net.AUTH;
using LumiSoft.Net.Mail;
using LumiSoft.Net.MIME;
using LumiSoft.Net.SMTP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;
using System.Net;

namespace D.Net.EmailClient
{
    public class SMTP_Wrapper
    {
        private SMTP_Client Client = null;

        private bool _IsConnected = false;

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; }
        }


        public void Alt()
        {
            string fromEmail = "rajat.sharma@maersk.com";
            
            MailMessage mailMessage = new MailMessage(fromEmail, "ghalib.saleem@tcs.com", "Subject", "Body");
            SmtpClient smtpClient = new SmtpClient("webmail.maersk.net",587);
            //smtpClient.Timeout = 100
            
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromEmail, "Mar@2017","webmail.maersk.net");
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //Error
                Console.WriteLine(ex.Message);
                //Response.Write(ex.Message);
            }
        }

        public void Connect(String server, String User, String pass, int port, bool useSSl)
        {
            try
            {
                Client = new SMTP_Client();
                
                //Client.StartTLS();
                
                Client.Connect(server, port, useSSl);
                Client.EhloHelo("webmail.maersk.net");
                AUTH_SASL_Client_Login authlog = new AUTH_SASL_Client_Login(User, pass);
                
                Client.Auth(new AUTH_SASL_Client_Login(User, pass));
                _IsConnected = true;
            }
            catch (Exception exe)
            {
                throw new EMailException { ExceptionType = EMAIL_EXCEPTION_TYPE.ERROR_ON_CONNECTION, InnerException = exe };
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                Client.Disconnect();
            }
        }

        public bool SendMessage(List<String> to, List<String> cc, List<String> bcc,string from ,string subject,string body)
        {
            //if (!_IsConnected) throw new EMailException { ExceptionType = EMAIL_EXCEPTION_TYPE.NOT_CONNECTED };
            SMTP_Message_Wrapper wr = new SMTP_Message_Wrapper()
            {
                To = to,
                Cc = cc,
                Bcc = bcc,
                From = from,
                Subject = subject,
                TextBody = body,
                Client = this.Client
            };

            try
            {
                
                
                Mail_Message mail = wr.GetMail();
                SMTP_Client.QuickSendSmartHost("webmail.maersk.net",587, false, "rajat.sharma@maersk.com","Mar@2017",  mail);
                
            }
            catch (Exception ex)
            {

                
            }
            //Client.QuickSend(wr.GetMail());
            return false;
        }

    }

    public class SMTP_Message_Wrapper
    {
        private SMTP_Client _Client;
        private string _Subject = null;
        private DateTime _Date;
        private long _Size = 0;
        private string _UID = null;
        private string _TextBody = null;
        private List<String> _To = new List<string>();
        private List<String> _Cc = new List<string>();
        private List<String> _Bcc = new List<string>();

        public string From { get; set; }

        public List<String> Cc
        {
            get { return _Cc; }
            set { _Cc = value; }
        }

        public List<String> Bcc
        {
            get { return _Bcc; }
            set { _Bcc = value; }
        }

        public List<String> To
        {
            get { return _To; }
            set { _To = value; }
        }


        public SMTP_Client Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        public string TextBody
        {
            get { return _TextBody; }
            set { _TextBody = value; }
        }

        public string UID
        {
            get { return _UID; }
            set { _UID = value; }
        }

        public long Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public Mail_Message GetMail()
        {
            Mail_Message mail = new Mail_Message();
            mail.To = new Mail_t_AddressList();
            foreach (var item in _To)
            {
                Mail_t_Mailbox mailbox = new Mail_t_Mailbox("", item);
                mail.To.Add(mailbox);
            }

            if (_Cc != null)
            {
                mail.Cc = new Mail_t_AddressList();
                foreach (var item in _Cc)
                {
                    Mail_t_Mailbox mailbox = new Mail_t_Mailbox("", item);
                    mail.Cc.Add(mailbox);
                }
            }


            if (_Bcc!=null)
            {
                mail.Bcc = new Mail_t_AddressList();
                foreach (var item in _Bcc)
                {
                    Mail_t_Mailbox mailbox = new Mail_t_Mailbox("", item);
                    mail.Bcc.Add(mailbox);
                } 
            }
            
            mail.From = new Mail_t_MailboxList();
            Mail_t_Mailbox frommailbox = new Mail_t_Mailbox("", From);
            mail.From.Add(frommailbox);
            mail.Subject = _Subject;
            mail.Date = DateTime.Now;

            MIME_h_ContentType contType = new MIME_h_ContentType(MIME_MediaTypes.Text.html);
            


            MIME_Entity entity_text_html = new MIME_Entity();
            MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
            entity_text_html.Body = text_html;
            text_html.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, TextBody);


            //MIME_b_Text text = new MIME_b_Text("text/html");
            //text.SetText("base64", Encoding.UTF8, _TextBody);
            
            mail.Body = text_html;
            return mail;
        }


        public Int32 SequenceNumber
        {
            get;
            set;
        }

       
    }

}
