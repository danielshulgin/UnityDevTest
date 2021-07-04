using Generic;
using UnityEngine;
using UnityExtensions;
using Zenject;

namespace UI
{
    public class StartMenuUI : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
        
        [Inject]
        private void Resolve(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void StartGameButtonClick()
        {
            _sceneLoader.LoadScene(Constants.MainScene);
        }

        public void ExitGameButtonClick()
        {
            Application.Quit(); 
        }
    }
}
