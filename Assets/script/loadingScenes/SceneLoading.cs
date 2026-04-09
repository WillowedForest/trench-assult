using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public static SceneLoading instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene("Level1");
        
    }

    public void LoadMainMenu()  
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
