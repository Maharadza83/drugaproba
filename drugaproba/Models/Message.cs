namespace drugaproba.Models;

public class Message
{
    public required string SenderName { get; set; }
    public required string MessageText { get; set; }
    public required DateTime Timestamp { get; set; }
}
