using _project.Scripts.GameEntities;
using _project.Scripts.Services.Factories;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Installers
{
    public class CubeLogicInstaller : MonoInstaller
    {
        [SerializeField] private Cube cubePrefab;

        public override void InstallBindings()
        {
            Container.Bind<Cube>().FromInstance(cubePrefab).WhenInjectedInto<CubeFactory>();
            Container.Bind<ICubeFactory>().To<CubeFactory>().AsSingle();
            Container.Bind<CubeManager>().AsSingle();

        }
    }
}