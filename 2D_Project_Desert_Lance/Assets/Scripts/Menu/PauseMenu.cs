using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleRestartClick);
        QuitButton.onClick.AddListener(HandleQuitClick);
    }

    void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleRestartClick()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitClick()
    {
        GameManager.Instance.QuitGame();
    }
}
