using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    private void Awake()
    {
        Time.timeScale = 1;
        _gameOverPanel?.SetActive(false);
    }

    private void OnEnable()
    {
        Pig.PigCaptured += ShowPanel;
    }

    private void OnDisable()
    {
        Pig.PigCaptured -= ShowPanel;
    }

    private void ShowPanel()
    {
        _gameOverPanel?.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
