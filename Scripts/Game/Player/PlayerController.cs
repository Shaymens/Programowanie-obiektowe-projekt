using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private Vector2 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;

    private bool isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;

    public int experience;
    public int currentLevel = 0;
    public int maxLevel = 20;
    public List<int> playerLevels;

    [SerializeField]private Weapon activeWeapon;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (playerLevels.Count == 0)
        {
            playerLevels.Add(10);
        }

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            int nextLevel = Mathf.CeilToInt(playerLevels[i - 1] * 1.1f + 15);
            playerLevels.Add(nextLevel);
        }

        playerHealth = playerMaxHealth;

        NewMonoBehaviourScript.Instance.UpdateHealthSlider();
        NewMonoBehaviourScript.Instance.UpdateExperienceSlider();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        playerMoveDirection = new Vector2(inputX, inputY).normalized;

        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);
        animator.SetBool("moving", playerMoveDirection != Vector2.zero);

        if (immunityTimer > 0) immunityTimer -= Time.deltaTime;
        else isImmune = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerMoveDirection * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunityDuration;
            playerHealth -= damage;
            NewMonoBehaviourScript.Instance.UpdateHealthSlider();

            if (playerHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;

        while (experience >= playerLevels[currentLevel] && currentLevel < maxLevel - 1)
        {
            experience -= playerLevels[currentLevel];
            currentLevel++;
            LevelUp();
        }

        NewMonoBehaviourScript.Instance.UpdateExperienceSlider();
    }

    public void LevelUp()
    {
        playerHealth = playerMaxHealth;

        NewMonoBehaviourScript.Instance.UpdateHealthSlider();
        NewMonoBehaviourScript.Instance.UpdateExperienceSlider();
        NewMonoBehaviourScript.Instance.LevelUpPanelOpen();
        NewMonoBehaviourScript.Instance.levelUpButtons[0].ActivateButton(activeWeapon);
    }
}
