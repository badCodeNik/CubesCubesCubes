using System.ComponentModel;
using _project.Scripts.GameEntities;
using _project.Scripts.Services.Factories;
using _project.Scripts.Services.InputService;
using _project.Scripts.Services.SavingService;
using _project.Scripts.Tools;
using _project.Scripts.UI;
using _project.Scripts.VFX;
using UnityEngine;
using Zenject;
using Input = _project.Scripts.Services.InputService.Input;

namespace _project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot uiRoot;
        [SerializeField] private VfxElement vfxElement;
        [SerializeField] private Tower tower;

        public override void InstallBindings()
        {
            Container.Bind<Signal>().AsSingle();
            BindUI();
            BindInput();
            BindSaving();
        }

        private void BindSaving()
        {
            Container.Bind<Tower>().FromInstance(tower).WhenInjectedInto<PlayerPrefsManager>();
            Container.Bind<PlayerPrefsManager>().AsSingle();
        }


        private void BindUI()
        {
            Container.Bind<VfxElement>().FromInstance(vfxElement).AsSingle();
            Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle().NonLazy();
            Container.Bind<UIInteraction>().AsSingle();
            Container.Bind<UIActionLog>().FromInstance(uiRoot.UIActionLog).AsSingle().NonLazy();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<Input>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        }
    }
}