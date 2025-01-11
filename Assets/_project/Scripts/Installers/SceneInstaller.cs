using _project.Scripts.Services.InputService;
using UnityEngine;
using Zenject;
using Input = _project.Scripts.Services.InputService.Input;

namespace _project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot uiRoot;
        public override void InstallBindings()
        {
            BindUI();
            BindInput();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle().NonLazy();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<Input>().
                AsSingle();
            Container.BindInterfacesAndSelfTo<CubeDragHandler>().
                AsSingle().NonLazy();
        }
    }
}
