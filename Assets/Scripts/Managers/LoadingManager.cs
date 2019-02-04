using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}