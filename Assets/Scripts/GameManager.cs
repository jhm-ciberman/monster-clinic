using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        itemPreview.AnimationCompleted += this.ItemPreview_AnimationCompleted;

        DOVirtual.DelayedCall(2f, () =>
        {
            this.cameraController.GoToGameplay(
                () => itemPreview.StartAnimation()
            );
        });
    }

    public void OnWin()
    {
        Debug.Log("You win!!");
        this.cameraController.GoToIntro();
    }

    private void ItemPreview_AnimationCompleted(object sender, EventArgs e)
    {
        // Start game
        this.cameraController.StartMovement();
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