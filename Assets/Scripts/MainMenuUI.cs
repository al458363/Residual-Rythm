using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "02_Game";
    [SerializeField] private string creditsSceneName = "03_Credits";

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("QuitGame llamado");
    }
}