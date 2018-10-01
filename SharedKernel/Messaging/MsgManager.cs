using System.Collections.Generic;

namespace SharedKernel.Messaging
{
    public class MsgManager : IMsgManager
    {
        public IList<IMsg> Errors { get; set; } = new List<IMsg>();

        public bool HasError => Errors.Count > 0;
    }
}
