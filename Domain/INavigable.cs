namespace Domain
{
    public interface INavigable
    {
        bool First { get; set; }
        bool HasMore { get; }
        bool Last { get; set; }
    }
}