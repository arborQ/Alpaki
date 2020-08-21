namespace Alpaki.Logic.Mails.Templates
{
    public static class MessageTemplates
    {
        public static class Invitation
        {
            public static readonly string Subject = "Zaproszenie";
            public static readonly string Body = @"
Zostałeś zaproszony do grona wolontariuszy.
Aby dołączyć do grona wolontariuszy użyj poniższego kodu przy rejestracji.
Kod: {0}";
        }
    }
}