using _project.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _project.Scripts.VFX
{
    public class ExplosionSystem : MonoBehaviour
    {
        private Signal _signal;
        private VfxElement _vfxElement;

        [Inject]
        public void Construct(Signal signal, VfxElement vfxElement)
        {
            _signal = signal;
            _vfxElement = vfxElement;
            _signal.Subscribe<Signals.OnExplosion>(OnExplode);
        }

        private void OnExplode(Signals.OnExplosion data)
        {
            _vfxElement.PlayAnimation(data.Position);
        }


        private void OnDisable()
        {
            _signal.Unsubscribe<Signals.OnExplosion>(OnExplode);
        }
    }
}