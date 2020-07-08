using Alpaki.CrossCutting.Enums;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerRequestFake
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public string DreamUrl { get; set; }
    }
}
