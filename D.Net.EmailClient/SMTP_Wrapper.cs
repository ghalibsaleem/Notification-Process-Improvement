using LumiSoft.Net.Mail;
using LumiSoft.Net.MIME;
using LumiSoft.Net.SMTP.Client;
using System;
using System.Collections.Generic;
using System.Text;



namespace D.Net.EmailClient
{
    public class SMTP_Wrapper
    {
        public bool SendMessage(string server,int port,List<String> to, List<String> cc, List<String> bcc,UserClient user ,string subject,string body)
        {
            SMTP_Message_Wrapper wr = new SMTP_Message_Wrapper()
            {
                To = to,
                Cc = cc,
                Bcc = bcc,
                From = user,
                Subject = subject,
                TextBody = body
            };

            try
            {
                Mail_Message mail = wr.GetMail();
                SMTP_Client.QuickSendSmartHost(server, port, false, user.Email,user.Password,  mail);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }

    public class SMTP_Message_Wrapper
    {
        
        private string _Subject = null;
        private DateTime _Date;
        private long _Size = 0;
        private string _UID = null;
        private string _TextBody = null;
        private List<String> _To = new List<string>();
        private List<String> _Cc = new List<string>();
        private List<String> _Bcc = new List<string>();

        public UserClient From { get; set; }

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
            Mail_t_Mailbox frommailbox = new Mail_t_Mailbox(From.Name, From.Email);
            mail.From.Add(frommailbox);
            mail.Subject = _Subject;
            mail.Date = DateTime.Now;

            MIME_h_ContentType contType = new MIME_h_ContentType(MIME_MediaTypes.Text.html);


            MIME_Entity entity_text_html = new MIME_Entity();
            MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
            entity_text_html.Body = text_html;
            text_html.SetText(MIME_TransferEncodings.EightBit, Encoding.UTF8, TextBody);

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
