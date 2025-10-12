using System;

namespace Internal.Runtime.Dialogues.Handlers
{
    public interface IDownInputHandler
    {
        void Init(Func<bool> downInputCallback);
    }
}