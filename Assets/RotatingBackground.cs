using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBackground : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public float rotationSpeed = 1f;

    public void Start()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        this._spriteRenderer.transform.Rotate(0, 0, this.rotationSpeed * Time.deltaTime);
    }
}
