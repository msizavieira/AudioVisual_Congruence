using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectUI : MonoBehaviour
{
    public void LoadCongruentScene()
    {
        SceneManager.LoadScene("CongruentMaze");
    }

    public void LoadIncongruentScene()
    {
        SceneManager.LoadScene("IncongruentMaze");
    }
}
