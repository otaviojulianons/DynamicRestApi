using System.Collections.Generic;

namespace SharedKernel.Messaging
{
    public interface IMsgManager
    {
        IList<IMsg> Errors { get; set; }

        bool HasError { get;  }
    }
}
