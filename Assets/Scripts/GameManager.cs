using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Coleccionables")]
    [SerializeField] private int totalCrystals = 5;
    [SerializeField] private TMP_Text crystalCounterText;

    [Header("Pantalla final")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text messageText;

    [Header("Colores pantalla final")]
    [SerializeField] private Color winColor = new Color(0.05f, 0.35f, 0.15f, 0.8f);
    [SerializeField] private Color loseColor = new Color(0.5f, 0.05f, 0.05f, 0.8f);

    private int collectedCrystals = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        if (endPanel != null) endPanel.SetActive(false);
        UpdateCrystalCounter();
    }

    private void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R)) Restart();
    }

    public void CollectCrystal()
    {
        if (gameEnded || collectedCrystals >= totalCrystals) return;
        collectedCrystals++;
        UpdateCrystalCounter();
        if (AudioManager.Instance != null) AudioManager.Instance.PlayCrystal();
    }

    public void TryWin()
    {
        if (gameEnded) return;

        if (collectedCrystals >= totalCrystals)
        {
            if (AudioManager.Instance != null) AudioManager.Instance.PlayWin();
            EndGame("¡GANASTE!", winColor);
        }
        else
        {
            Debug.Log("Faltan cristales para ganar");
        }
    }

    public void GameOver()
    {
        if (gameEnded) return;
        if (AudioManager.Instance != null) AudioManager.Instance.PlayDeath();
        EndGame("PERDISTE", loseColor);
    }

    private void EndGame(string message, Color panelColor)
    {
        gameEnded = true;
        Time.timeScale = 0f;
        if (messageText != null) messageText.text = message;
        if (endPanel != null)
        {
            Image panelImage = endPanel.GetComponent<Image>();
            if (panelImage != null) panelImage.color = panelColor;
            endPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateCrystalCounter()
    {
        if (crystalCounterText != null)
            crystalCounterText.text = collectedCrystals + " / " + totalCrystals;
    }
}