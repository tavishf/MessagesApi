using MessagesApi.Controllers;

namespace MessagesApi.Repositories;

public interface IMessagesRepository
{ 
    internal IEnumerable<Message> GetMessages();
    
    internal void AddMessage(Message message);
}