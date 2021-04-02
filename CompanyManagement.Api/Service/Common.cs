using CompanyManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CompanyManagement.Api.Service
{
    public class Common
    {
        public static ResponseMail SendEmail(RequestMail requestMail)
        {
            var responseMail =new ResponseMail();
            responseMail.Status = false;
            try
            {
                if (requestMail.To != null)
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(requestMail.From, requestMail.DisplayName);
                    if (requestMail.To != null)
                    {
                        message.To.Add(new MailAddress(requestMail.To));
                    }
                    if (!string.IsNullOrEmpty(requestMail.CC))
                    {
                        foreach (var address in requestMail.CC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            message.CC.Add(new MailAddress(address));
                        }
                    }

                    message.Subject = requestMail.Subject;
                    message.IsBodyHtml = true;
                    message.Body = requestMail.Body;
                    smtp.Port = requestMail.port;
                    smtp.Host = requestMail.host;
                    smtp.EnableSsl = requestMail.EnableSsl;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(requestMail.From, requestMail.password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    responseMail.Message = "Mail sent successfully";
                    responseMail.Status = true;
                }
                else
                {
                    responseMail.Message = "To address not available.";
                }
            }
            catch (Exception ex)
            {
                responseMail.Message = "Exception : " + ex.Message + " Inner Exception : " + ex.InnerException;
            }
            return responseMail;
        }

    }
}
