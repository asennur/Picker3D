using System;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<int> onSetNewLevelValue = delegate { };
        public UnityAction<int> onSetStageColor = delegate { };
        public Func<float> onGetScore = delegate { return 0; };
    }
}