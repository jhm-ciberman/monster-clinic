using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public class CameraEffectMushrooms : MonoBehaviour, ICameraEffect
{
    public float Weight { get; set; } = 0f;

    public Vector2 CameraPosition { get; private set; } = Vector2.zero;
    public float CameraRotation { get; private set; } = 0f;
    public float CameraSizeScale { get; private set; } = 1f;

    public Vector2 positionDelta = new Vector2(0.2f, 0.2f);

    public float positionSpeed = 1f;

    public float rotationDelta = 1f;
    public float rotationSpeed = 1f;

    public float hueShiftSpeed = 1f;

    public float cameraSizeScaleDelta = 0.1f;
    public float cameraSizeScaleSpeed = 1f;

    private PostProcessVolume _volume;

    private ColorGrading _colorGrading;

    private float _timer;

    public void Start()
    {
        this._volume = this.GetComponent<PostProcessVolume>();
        this._volume.weight = this.Weight;

        this._colorGrading = this._volume.profile.GetSetting<ColorGrading>();
    }

    public void Update()
    {
        this._volume.weight = this.Weight;

        if (this.Weight == 0) return;

        this._timer += Time.deltaTime;

        this.CameraPosition = new Vector2(
            Mathf.Sin(this._timer * this.positionSpeed) * this.positionDelta.x,
            Mathf.Sin(this._timer * this.positionSpeed) * this.positionDelta.y
        );

        this.CameraRotation = Mathf.Sin(this._timer * this.rotationSpeed) * this.rotationDelta;

        this._colorGrading.hueShift.value += Time.deltaTime * this.hueShiftSpeed;

        this.CameraSizeScale = 1f + this.cameraSizeScaleDelta + Mathf.Sin(this._timer * this.cameraSizeScaleSpeed) * this.cameraSizeScaleDelta;
    }
}