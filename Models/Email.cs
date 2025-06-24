using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DevFunda_Lib 
{
    

    public static class Email
    {
    public static string To {get;set;}
    public static string Subject {get;set;}
    public static string From {get;set;}
    public static string Body { get; set; }
  

        public static bool Send_Email(string SentFor, string To, string From, string SMTP)
        {
            bool Emailsent = true;
            MailMessage message = new MailMessage();

            if (SentFor=="self")
            {


                    message.From = new MailAddress(From);
                    message.To.Add(new MailAddress(To));
                    message.Subject = Subject;
                    message.IsBodyHtml = true;

                    message.Body = Body;
            }
            else
            {           message.From = new MailAddress(From);
                    message.To.Add(new MailAddress(From));
                    message.Subject = Subject;
                    message.IsBodyHtml = true;

                    message.Body = Body;
                    

        }

             
                    SmtpClient client = new SmtpClient(SMTP);
                    {
                        client.UseDefaultCredentials = true;

                        // client.EnableSsl = false;
                        //  client.Port = 80;

                    //    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTP_Email"], ConfigurationManager.AppSettings["SMTP_Pwd"]);

                    }

                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {

                        Emailsent = false;
                    }
                 
            
            return Emailsent;
        
        
        }
    }
}