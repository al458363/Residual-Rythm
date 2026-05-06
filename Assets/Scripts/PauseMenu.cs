using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    private bool paused = false;

    private void Awake()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (paused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        paused = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        paused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("01_MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}