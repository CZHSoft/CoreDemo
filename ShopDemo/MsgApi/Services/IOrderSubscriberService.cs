using MsgApi.Models;


namespace MsgApi.Services
{
    public interface IOrderSubscriberService
    {
        void ConsumeOrderMessage(Order message);
    }
}
