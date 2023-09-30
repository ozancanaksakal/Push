using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text anneunceText;
    public GameObject restartButton;
    public Animator textAnimator;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Physics.gravity *= 1.2f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateScore(int score)
    {
        scoreText.text = "Waves " + score;
        anneunceText.text = "Waves " + score;
    }

    public void OpenMenu()
    {
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartAnimation()
    {
        scoreText.gameObject.SetActive(false);
        anneunceText.gameObject.SetActive(true);
    }
    public void StopAnimation()
    {
        scoreText.gameObject.SetActive(true);
        anneunceText.gameObject.SetActive(false);
    }
}