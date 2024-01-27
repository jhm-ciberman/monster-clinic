using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CameraController cameraController;

    public ItemPreview itemPreview;

    private int _livesCount = 3;

    public void Start()
    {
        Instance = this;

        itemPreview.gameObject.SetActive(true);
        itemPreview.AnimationCompleted += this.ItemPreview_AnimationCompleted;
    }

    private void ItemPreview_AnimationCompleted(object sender, EventArgs e)
    {
        this.cameraController.GoToGameplay();
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
