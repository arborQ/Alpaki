using System.Threading.Tasks;
using MimeKit;

namespace Alpaki.Logic.Mails
{
    public interface IMailService
    {
        Task Send(MimeMessage message);
    }
}