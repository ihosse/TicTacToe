using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Load(int SceneIndex) 
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
