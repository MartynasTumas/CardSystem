namespace Server.Models
{
    public enum CodeStatus
    {
        New,
        Used
    }
    public class Code
    {
        public CodeStatus Status { get; set; }
        public string Value { get; set; }
        public string[] Products { get; set; }
    }
}
