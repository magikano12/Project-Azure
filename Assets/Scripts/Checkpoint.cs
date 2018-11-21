using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float unactivatedRotationSpeed = 100, activatedRotationSpeed=300;

    [SerializeField]
    private float unactivatedScale = 1, activatedScale = 1.5f;

    [SerializeField]
    private Color unactivatedColor, activatedColor;

    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateColor()
    {
        Color color = unactivatedColor;
        if (isActivated)
        {
            color = activatedColor;
        }
        spriteRenderer.color=color;
    }
    private void UpdateScale()
    {
        float scale = unactivatedScale;
        if (isActivated)
        {
            scale = activatedScale;
        }
        transform.localScale = Vector3.one * scale;
    }
    private void UpdateRotation()
    {
        float rotationSpeed = unactivatedRotationSpeed;
        if(isActivated)
        {
            rotationSpeed = activatedRotationSpeed;
        }
        transform.Rotate(Vector3.up * rotationSpeed*Time.deltaTime);
    }
    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
        UpdateColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has crossed a checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
        }
    }
}
