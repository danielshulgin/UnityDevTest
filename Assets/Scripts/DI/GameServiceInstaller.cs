using UnityEngine;
using Zenject;

namespace DI
{
    public class GameServiceInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(mainCamera);
        }
    }
}