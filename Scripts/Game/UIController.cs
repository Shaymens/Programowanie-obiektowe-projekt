using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public static NewMonoBehaviourScript Instance;

    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private TMP_Text experienceText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject levelUpPanel;
    [SerializeField] private TMP_Text timerText;
    public LevelUpButton[] levelUpButtons;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = PlayerMovement.Instance.playerMaxHealth;
        playerHealthSlider.value = PlayerMovement.Instance.playerHealth;
        healthText.text = playerHealthSlider.value + " / " + playerHealthSlider.maxValue;
    }

    public void UpdateExperienceSlider()
    {
        playerExperienceSlider.maxValue = PlayerMovement.Instance.playerLevels[PlayerMovement.Instance.currentLevel];
        playerExperienceSlider.value = PlayerMovement.Instance.experience;
        experienceText.text = playerExperienceSlider.value + " / " + playerExperienceSlider.maxValue;
    }

    public void UpdateTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void LevelUpPanelClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LevelUpPanelOpen()
    {
        levelUpPanel.SetActive(true);
    }

}
