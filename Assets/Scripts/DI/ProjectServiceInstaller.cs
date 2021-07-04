using Data;
using MyInput;
using UnityEngine;
using UnityExtensions;
using Zenject;

namespace DI
{
    public class ProjectServiceInstaller : MonoInstaller
    {
        [SerializeField] private ClickManager clickManager;
        
        [SerializeField] private PlayerData playerData;
        
        [SerializeField] private SceneLoader sceneLoader;
        
        
        public override void InstallBindings()
        {
            Container.Bind<ClickManager>().FromInstance(clickManager);
            Container.Bind<PlayerData>().FromInstance(playerData);
            Container.Bind<SceneLoader>().FromInstance(sceneLoader);
        }
    }
}