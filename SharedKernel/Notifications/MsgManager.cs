using System.Collections.Generic;

namespace SharedKernel.Notifications
{
    public class MsgManager : IMsgManager
    {
        public IList<IMsg> Errors { get; set; } = new List<IMsg>();

        public bool HasError => Errors.Count > 0;
    }
}
