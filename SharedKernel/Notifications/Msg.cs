namespace SharedKernel.Notifications
{
    public class Msg : IMsg
    {
        public Msg(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
