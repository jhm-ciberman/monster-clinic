using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _livesCount = 3;

    public void Start()
    {
        Instance = this;
    }

    public void NotifyPlayerDied()
    {
        this._livesCount--;
        Debug.Log($"Lives: {this._livesCount}");

        if (this._livesCount <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
