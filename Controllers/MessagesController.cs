using Microsoft.AspNetCore.Mvc;

namespace MessagesApi.Controllers;

[ApiController]
[Route("messages")]
public class MessagesController : Controller
{
    [HttpGet(Name = "GetMessages")]
    public IEnumerable<Message> Get()
    {
        return [new Message("1234", "Hello World!", MessageStatus.Unread)];
    }
}

public readonly record struct Message(string id, string messageText, MessageStatus messageStatus);

public enum MessageStatus
{
    Unread,
    Read
}