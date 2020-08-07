using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Requests;
using MediatR;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamRequest : IRequest<GetDreamResponse>
    {
       public long DreamId { get; set; }
    }
}
