using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BoxPuzzle : MonoBehaviour, IPuzzle
{

    [Header("Identificador")]
    [SerializeField] private string puzzleId = "box_main";
    [Header("Configuracion del puzzle")]
    [SerializeField]
    private List<BoxButton> solution = new()
    {
        BoxButton.Taza,
        BoxButton.Radio,
        BoxButton.Lampara
    };

    [Header("Tiempo de validacion")]
    [SerializeField, Min(0f)] private float resultDelay = 1f;

    [Header("Sonidos")]
    [SerializeField] private UnityEvent onButtonPressed;
    [SerializeField] private UnityEvent onCorrect;
    [SerializeField] private UnityEvent onFailed;

    public event Action OnAttemptFailed;

    private bool isResolving = false;
    public bool IsResolving => isResolving;
    [Header("Eventos")]
    [SerializeField] private UnityEvent onSolved;
    private readonly List<BoxButton> currentSequence = new();

    public IReadOnlyList<BoxButton> CurrentSequence => currentSequence;

    //Implementación de IPuzzle
    public event Action<IPuzzle> OnSolved;
    public bool IsSolved { get; private set; } = false;
    public string PuzzleId => puzzleId;
    public event Action OnSequenceChanged;

    public void Start()
    {
        // Registrarse automaticamente en PuzzleManager.
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.RegisterPuzzle(this);
        }
    }

    private void OnDestroy()
    {
        // Eliminar el registro al destruirse.
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.UnregisterPuzzle(this);
        }
    }

    public void ButtonPressed(BoxButton buttonName)
    {
        if (IsSolved || isResolving ||
            solution == null || solution.Count == 0)
            return;

        // Guardar pulsacion y reproducir clic.
        currentSequence.Add(buttonName);
        onButtonPressed?.Invoke();

        // Bloquear nuevas entradas al completar la secuencia.
        bool completed = currentSequence.Count == solution.Count;

        if (completed)
            isResolving = true;

        // Actualizar las imagenes.
        OnSequenceChanged?.Invoke();

        if (!completed)
            return;

        // Validar y reproducir el sonido correspondiente.
        bool correct = CheckSolution();

        if (correct)
        {
            onCorrect?.Invoke();
        }
        else
        {
            onFailed?.Invoke();
            OnAttemptFailed?.Invoke();
        }

        // Ambos resultados utilizan el mismo retraso.
        StartCoroutine(ResolveAttempt(correct));
    }
    private bool CheckSolution()
    {
        for (int i = 0; i < solution.Count; i++)
        {
            if (currentSequence[i] != solution[i])
                return false;
        }

        return true;
    }

    private void SolvePuzzle()
    {
        IsSolved = true;
        Debug.Log("Puzzle completado!");
        // Notificar al PuzzleManager.
        OnSolved?.Invoke(this);
        //Ejecutar acciones inspector
        onSolved?.Invoke();
    }

    public void ResetPuzzle()
    {
        if (IsSolved || isResolving)
            return;

        currentSequence.Clear();
        OnSequenceChanged?.Invoke();
    }
    private IEnumerator ResolveAttempt(bool correct)
    {
        yield return new WaitForSecondsRealtime(resultDelay);

        if (correct)
        {
            SolvePuzzle();
        }
        else
        {
            isResolving = false;
            ResetPuzzle();
        }
    }
}