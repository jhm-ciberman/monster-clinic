using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public class CameraEffectIntoxicated : MonoBehaviour, ICameraEffect
{
    public float Weight { get; set; } = 0f;

    public Vector2 CameraPosition { get; private set; } = Vector2.zero;
    public float CameraRotation { get; private set; } = 0f;

    private PostProcessVolume _volume;

    public void Start()
    {
        this._volume = this.GetComponent<PostProcessVolume>();
        this._volume.weight = this.Weight;
    }

    public void Update()
    {
        //this._cameraController.volume.weight = Mathf.Lerp(this._cameraController.volume.weight, 1, Time.deltaTime * 2);
    }
}
