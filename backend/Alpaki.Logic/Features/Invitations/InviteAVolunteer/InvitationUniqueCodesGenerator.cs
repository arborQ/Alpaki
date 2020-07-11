using System.Linq;
using System.Security.Cryptography;

namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public class InvitationUniqueCodesGenerator
    {
        public string Generate(int length)
        {
            const string charset = "012345678910ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var code = string.Concat(Enumerable.Range(0, length)
                .Select(_ => RandomNumberGenerator.GetInt32(0, charset.Length))
                .Select(x => charset[x]));
            return code;
        }
    }
}