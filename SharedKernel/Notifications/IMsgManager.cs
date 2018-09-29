using System.Collections.Generic;

namespace SharedKernel.Notifications
{
    public interface IMsgManager
    {
        IList<IMsg> Errors { get; set; }

        bool HasError { get;  }
    }
}
