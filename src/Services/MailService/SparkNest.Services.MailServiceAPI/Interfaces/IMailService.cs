using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.MailServiceAPI.Interfaces
{
    public interface IMailService
    {
        void SendMail(string to, string subject, string body, string? url = null);

        // E-posta gönderme metodunu asenkron olarak tanımlar
        Task SendMailAsync(string to, string subject, string body);

        // Ek dosya ekleyerek e-posta gönderme metodunu tanımlar
        void SendMailWithAttachment(string to, string subject, string body, Stream attachmentStream, string attachmentFileName);

        // Ek dosya ekleyerek e-posta gönderme metodunu asenkron olarak tanımlar
        Task SendMailWithAttachmentAsync(string to, string subject, string body, Stream attachmentStream, string attachmentFileName);
    }
}
