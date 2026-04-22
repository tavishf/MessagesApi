using MessagesApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MessagesApi.Controllers;

[ApiController]
[Route("messages")]
public class MessagesController : Controller
{
    private IMessagesRepository _messagesRepository;
    
    public MessagesController(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }
    
    [HttpGet(Name = "GetMessages")]
    public IEnumerable<Message> Get()
    {
        return _messagesRepository.GetMessages();
    }
    
    [HttpPost(Name = "AddMessage")]
    public void Post([FromBody] Message message)
    {
        _messagesRepository.AddMessage(message);
    }
    
}

public readonly record struct Message(string Id, string messageText, MessageStatus messageStatus);

public enum MessageStatus
{
    Unread,
    Read
}