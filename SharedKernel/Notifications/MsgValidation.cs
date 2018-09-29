namespace SharedKernel.Notifications
{
    public class MsgValidation : IMsg
    {
        public MsgValidation(string message, string property)
        {
            Message = message;
            Property = property;
        }

        public string Message { get; private set; }

        public string Property { get; private set; }
    }
}
