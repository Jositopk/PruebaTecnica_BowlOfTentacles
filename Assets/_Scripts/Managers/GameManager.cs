
using System;
using UnityEngine;

public enum GameState
{
    STARTING,
    ONGOING,
    PAUSED,
    FINISHED
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool exitUnlocked;

    [Header("Referencias Principales")]
    [SerializeField] private PuzzleManager puzzleManager;
    //referencias del control del jugador
    [SerializeField] private FPSController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;

    private bool playerControlEnabled = true;

    //Atributos de gestion de estados
    public GameState CurrentState { get; private set; }
        = GameState.STARTING;

    public event Action<GameState> OnStateChanged;

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
        exitUnlocked = puzzleManager.AllPuzzlesSolved;
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
        bool canControl = CurrentState == GameState.ONGOING&& playerControlEnabled;

        playerController.enabled = canControl;
        playerInteractor.enabled = canControl;
    }
    #endregion
    public bool CanExit()
    {
        return CurrentState == GameState.ONGOING
            && exitUnlocked;
    }
}
