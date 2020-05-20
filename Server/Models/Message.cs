namespace Server.Models
{

    public enum Action
    {
        Generate,
        Use
    }
    public class Message
    {
        public Action Action { get; set;}
        public string Text { get; set; }
    }
}
