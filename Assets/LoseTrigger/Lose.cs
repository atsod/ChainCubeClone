using System;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public static event Action OnLoseUI;

    private void OnEnable() => Cube.OnPlayerLose += OnLose;

    private void OnDisable() => Cube.OnPlayerLose -= OnLose;

    private void OnLose()
    {
        OnLoseUI?.Invoke();
    }
}
