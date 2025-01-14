using _project.Data;
using _project.Scripts.Services.Factories;
using _project.Scripts.Spawners;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Installers
{
    public class UICubeFactoryInstaller : MonoInstaller
    {
        [SerializeField] private GameObject uiCubeFactoryPrefab;
        [SerializeField] private GameConfiguration gameConfiguration;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameConfiguration);

            Container.Bind<UICubeFactory>()
                .AsSingle()
                .WithArguments(uiCubeFactoryPrefab);

            Container.Bind<UICubeSpawner>()
                .AsSingle()
                .NonLazy();
        }
    }
}