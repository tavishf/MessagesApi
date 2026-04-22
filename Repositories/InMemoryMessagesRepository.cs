using MessagesApi.Controllers;
using Microsoft.Data.Sqlite;

namespace MessagesApi.Repositories;

public class InMemoryMessagesRepository : IMessagesRepository
{
    private const string ConnectionString = "Data Source=:memory:";
    private SqliteConnection _connection;

    public InMemoryMessagesRepository()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();
        using var command = _connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE IF NOT EXISTS Messages (
                                  Id STRING PRIMARY KEY,
                                  MessageText STRING,
                                  MessageStatus STRING CHECK( MessageStatus IN ('0','1') ) 
                              )
                              """;
        command.ExecuteNonQuery();
    }

    public IEnumerable<Message> GetMessages()
    {
        _connection.Open();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Messages";
        using var reader = command.ExecuteReader();
        var messages = new List<Message>();
        while (reader.Read())
        {
            var id = reader.GetString(0);
            var messageText = reader.GetString(1);
            var messageStatusString = reader.GetString(2);
            Enum.TryParse(messageStatusString, out MessageStatus messageStatus);
            messages.Add(new Message(id, messageText, messageStatus));
        }
        return messages;
    }

    public void AddMessage(Message message)
    {
        _connection.Open();
        using var command = _connection.CreateCommand();
        command.Parameters.AddWithValue("@Id", message.Id);
        command.Parameters.AddWithValue("@MessageText", message.messageText);
        command.Parameters.AddWithValue("@MessageStatus", message.messageStatus);
        command.CommandText = "INSERT INTO Messages (Id, MessageText, MessageStatus) VALUES (@Id, @MessageText, @MessageStatus)";
        command.ExecuteNonQuery();
    }
}