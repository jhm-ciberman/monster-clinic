using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private bool _isDragging = false;

    [SerializeField]
    private bool _isMouseOver = false;

    private bool _canLostLife = false;

    private Rigidbody2D _rb;

    public void Start()
    {
        this._rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (this._isMouseOver && Input.GetMouseButtonDown(0))
        {
            this.StartDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            this.EndDrag();
        }


    }

    public void FixedUpdate()
    {
        if (this._isDragging)
        {
            this.Drag();
        }
    }

    private void StartDrag()
    {
        this._isDragging = true;
        this._canLostLife = true;
    }

    private void EndDrag()
    {
        this._isDragging = false;
    }

    private void Drag()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        this._rb.MovePosition(pos);
    }

    public void OnMouseEnter()
    {
        this._isMouseOver = true;
    }

    public void OnMouseExit()
    {
        this._isMouseOver = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this._isDragging = false;
        Debug.Log("Collision!");
        Debug.Log(collision.gameObject.name);
        if (!this._canLostLife) return;

        this._canLostLife = false;

        // Find whether the parent has the LevelBlock component
        var levelBlock = this.GetComponentInParent<LevelBlock>();

        if (levelBlock == null)
        {
            GameManager.Instance.NotifyPlayerDied();
        }

    }
}
