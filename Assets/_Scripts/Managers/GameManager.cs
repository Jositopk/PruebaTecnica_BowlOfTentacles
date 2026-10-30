
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    STARTING,
    ONGOING,
    PAUSED,
    ENDING,
    FINISHED
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Referencias Principales")]
    [SerializeField] private PuzzleManager puzzleManager;
    //referencias del control del jugador
    [SerializeField] private FPSController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;

    private bool playerControlEnabled = true;
    public bool allPuzzlesSolved;

    //Atributos de gestion de estados
    public GameState CurrentState { get; private set; }
        = GameState.STARTING;

    public event Action<GameState> OnStateChanged;
    [SerializeField] private UnityEvent onGameInitialized;

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
        onGameInitialized?.Invoke();
        UpdatePlayerControls();
    }
    private void OnEnable()
    {
        puzzleManager.PuzzleSolved += OnPuzzleSolved;
    }

    private void OnDisable()
    {
        puzzleManager.PuzzleSolved -= OnPuzzleSolved;
    }

    private void OnPuzzleSolved(IPuzzle puzzle)
    {
        CheckEndConditions();
    }
    private void CheckEndConditions()
    {
        allPuzzlesSolved = puzzleManager.AllPuzzlesSolved;

    }
    public void SetState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        UpdatePlayerControls();
        OnStateChanged?.Invoke(newState);
    }

    #region controlPlayer
    public void SetPlayerControl(bool enabled)
    {
        playerControlEnabled = enabled;
        UpdatePlayerControls();
    }

    private void UpdatePlayerControls()
    {
        bool canControl = CurrentState == GameState.ONGOING && playerControlEnabled;

        playerController.enabled = canControl;
        playerInteractor.enabled = canControl;
    }
    #endregion
    public bool CanExit()
    {
        return CurrentState == GameState.ONGOING
            && allPuzzlesSolved;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowInitialMessage()
    {
        UIManager.Instance.ShowInformation("He venido a recoger la caja de mamá. Después entregaré las llaves", 3, () => SetState(GameState.ONGOING));
    }
}
