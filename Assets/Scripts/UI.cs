using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButton;

    private int scoreValue;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreValue.ToString("#,#");
    }

    public void OpenScene()
    {
        restartButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void AddScore(int coinValue)
    {
        scoreValue += coinValue;
        scoreText.text = scoreValue.ToString("#,#");
    }
    

}
