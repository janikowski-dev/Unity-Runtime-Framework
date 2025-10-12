using System;

namespace Internal.Runtime.Dialogues.Handlers
{
    public interface IUpInputHandler
    {
        void Init(Func<bool> upInputCallback);
    }
}