using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    // Coleccion de puzzles registrados.
    private readonly Dictionary<string, IPuzzle> puzzles = new();

    // Evento general cuando se resuelve cualquier puzzle.
    public event Action<IPuzzle> PuzzleSolved;

    //Todos los puzles terminados
    public bool AllPuzzlesSolved
{
    get
    {
        if (puzzles.Count == 0)
            return false;

        foreach (IPuzzle puzzle in puzzles.Values)
        {
            if (!puzzle.IsSolved)
                return false;
        }

        return true;
    }
}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Registrar un nuevo puzzle.
    public void RegisterPuzzle(IPuzzle puzzle)
    {
        if (puzzle == null)
            return;

        if (string.IsNullOrWhiteSpace(puzzle.PuzzleId))
        {
            Debug.LogWarning("Un puzzle no tiene ID.");
            return;
        }

        // Evitar registros duplicados.
        if (puzzles.ContainsKey(puzzle.PuzzleId))
        {
            Debug.LogWarning(
                $"El puzzle {puzzle.PuzzleId} ya esta registrado.");
            return;
        }

        puzzles.Add(puzzle.PuzzleId, puzzle);

        // Escuchar cuando se resuelva.
        puzzle.OnSolved += HandlePuzzleSolved;
    }

    // Eliminar un puzzle del registro.
    public void UnregisterPuzzle(IPuzzle puzzle)
    {
        if (puzzle == null)
            return;

        if (puzzles.TryGetValue(puzzle.PuzzleId, out IPuzzle registered)
            && ReferenceEquals(registered, puzzle))
        {
            puzzle.OnSolved -= HandlePuzzleSolved;
            puzzles.Remove(puzzle.PuzzleId);
        }
    }

    // Consultar si un puzzle esta resuelto.
    public bool IsPuzzleSolved(string puzzleId)
    {
        return puzzles.TryGetValue(puzzleId, out IPuzzle puzzle)
            && puzzle.IsSolved;
    }

    // Recibir la notificacion de resolucion.
    private void HandlePuzzleSolved(IPuzzle puzzle)
    {
        Debug.Log($"Puzzle resuelto: {puzzle.PuzzleId}");

        // Notificar a otros sistemas interesados.
        PuzzleSolved?.Invoke(puzzle);
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        foreach (IPuzzle puzzle in puzzles.Values)
        {
            puzzle.OnSolved -= HandlePuzzleSolved;
        }

        puzzles.Clear();
        Instance = null;
    }
}