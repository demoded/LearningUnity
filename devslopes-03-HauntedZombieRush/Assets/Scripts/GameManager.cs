using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameOverUI;
    public static GameManager instance;
    private bool playerActive;
    private bool gameOver;
    private bool gameStarted;
    private float score;

    public bool PlayerActive { get => playerActive; set => playerActive = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public float Score { get => score; set => score = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Assert.IsNotNull(mainMenu);
        Assert.IsNotNull(gameplayUI);
    }

    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        playerActive = false;
        gameOver = false;
        gameStarted = false;
        mainMenu.SetActive(true);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(false);
        Player player = FindObjectOfType<Player>();
        player.InitPlayer();
        score = 0;
    }

    public void PlayerCollided()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
    }

    public void CoinGrab()
    {
        score += 100;
    }

    public void PlayerStartedGame()
    {
        playerActive = true;
        gameplayUI.SetActive(true);
    }

    public void ToggleVisibility(GameObject _object, bool visible = true)
    {
        Renderer[] renderers = _object.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }
    }

    public void EnterGame()
    {
        gameStarted = true;
        mainMenu.SetActive(false);
    }

    public void RestartGame()
    {
        InitGame();
    }
}
