using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public class CameraEffectIntoxicated : MonoBehaviour, ICameraEffect
{
    public float Weight { get; set; } = 0f;

    public Vector2 CameraPosition { get; private set; } = Vector2.zero;
    public float CameraRotation { get; private set; } = 0f;
    public float CameraSizeScale { get; } = 1f;

    public Vector2 positionDelta = new Vector2(0.2f, 0.2f);

    public float positionSpeed = 1f;

    public float rotationDelta = 1f;
    public float rotationSpeed = 1f;

    public Vector2 vignetteCenterDelta = new Vector2(0.2f, 0.2f);

    public float vignetteCenterSpeed = 1f;

    public float vignetteIntensityBase = 0.2f;

    public float vignetteIntensityDelta = 0.2f;

    public float vignetteIntensitySpeed = 1f;

    private PostProcessVolume _volume;

    private Vignette _vignette;

    private float _timer;

    public void Start()
    {
        this._volume = this.GetComponent<PostProcessVolume>();
        this._volume.weight = this.Weight;

        this._vignette = this._volume.profile.GetSetting<Vignette>();
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

        this._vignette = this._volume.profile.GetSetting<Vignette>();

        this._vignette.center.value = new Vector2(
            0.5f + Mathf.Sin(this._timer * this.vignetteCenterSpeed) * this.vignetteCenterDelta.x,
            0.5f + Mathf.Sin(this._timer * this.vignetteCenterSpeed) * this.vignetteCenterDelta.y
        );

        var intensity = this.vignetteIntensityBase + Mathf.Sin(this._timer * this.vignetteIntensitySpeed) * this.vignetteIntensityDelta;

        this._vignette.intensity.value = intensity;
    }
}
