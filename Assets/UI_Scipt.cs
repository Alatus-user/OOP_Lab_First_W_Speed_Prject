using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Scipt : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene()
    {
        Time.timeScale = 0;
        restartButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
