using System.Collections.Generic;
using Prism.Events;

namespace WordlieSolver.Shared
{
    public class WordSelectedEvent : PubSubEvent<ILetter[]>
    {
    }

    public class WordAppliedEvent : PubSubEvent<IEnumerable<ILetter>>
    {
    }

    public class RestartEvent : PubSubEvent
    {
    }
}
