using UnityEngine;
using Zenject;

namespace _Game.Scripts.Components
{
    public class GameBehaviour : MonoBehaviour
    {
        [Inject] protected DiContainer DiContainer;
        [Inject] protected SignalBus SignalBus;
    }
}