using _project.Scripts.Services.InputService;
using Zenject;

namespace _project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<Input>().
                AsSingle();
            Container.Bind<CubeDragHandler>().
                AsSingle().NonLazy();
        }
    }
}
