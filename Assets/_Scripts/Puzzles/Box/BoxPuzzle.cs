using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxPuzzle : MonoBehaviour,IPuzzle
{
    
    [Header("Identificador")]
    [SerializeField] private string puzzleId = "box_main";
    [Header("Configuracion del puzzle")]
    [SerializeField] private List<BoxButton> solution = new()
    {
        BoxButton.Taza,
        BoxButton.Radio,
        BoxButton.Lampara
    };

    [Header("Eventos")]
    [SerializeField] private UnityEvent onSolved;
    private int nButtonsPressed = 0;

      //Implementación de IPuzzle
    public event Action<IPuzzle> OnSolved;
    public bool IsSolved { get; private set; } = false;
    public string PuzzleId =>puzzleId;

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
        // Si ya esta resuelto, ignoramos los botones.
        if (IsSolved || solution == null || solution.Count == 0)
            return;
        // Comprobar el siguiente elemento de la secuencia.
        if (buttonName == solution[nButtonsPressed])
        {
            nButtonsPressed++;
            Debug.Log("Boton correcto: " + buttonName);
            if (nButtonsPressed == solution.Count)
            {
                SolvePuzzle();
            }
        }
        else
        {
            Debug.Log("Boton incorrecto: " + buttonName);
            ResetPuzzle();
        }
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
        nButtonsPressed = 0;
    }
}