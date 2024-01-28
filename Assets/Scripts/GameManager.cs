using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CameraController cameraController;

    public ItemPreview itemPreview;

    private bool _playerWon = false;

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
        if (this._playerWon) return;
        this._playerWon = true;

        this.cameraController.GoToIntro();
        this.cameraController.StopMovement();
    }

    private void ItemPreview_AnimationCompleted(object sender, EventArgs e)
    {
        // Start game
        this.cameraController.StartMovement();
    }

    public void NotifyPlayerDied()
    {
        Debug.Log("Game Over!");
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.Restart();
        }
    }
}