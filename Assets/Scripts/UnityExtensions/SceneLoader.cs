using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityExtensions
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}