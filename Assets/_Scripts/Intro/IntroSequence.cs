
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class IntroSequence : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Configuracion")]
    [SerializeField] private string gameSceneName = "Casa";
    [SerializeField] private bool allowSkip = true;

    private bool isLoading;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        if (videoPlayer == null || videoPlayer.clip == null)
        {
            Debug.LogError("No hay video de introduccion.");
            LoadGame();
            return;
        }

        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.errorReceived += OnVideoError;

        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        player.Play();
    }

    private void OnVideoFinished(VideoPlayer player)
    {
        LoadGame();
    }

    private void OnVideoError(VideoPlayer player, string message)
    {
        Debug.LogError("Error al reproducir intro: " + message);
        LoadGame();
    }

    private void Update()
    {
        if (!allowSkip || isLoading)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        if (isLoading)
            return;

        isLoading = true;
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnDestroy()
    {
        if (videoPlayer == null)
            return;

        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.loopPointReached -= OnVideoFinished;
        videoPlayer.errorReceived -= OnVideoError;
    }
}
