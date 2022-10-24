using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashNStretch : MonoBehaviour
{
    public float exaggerationValue = 0.1f;
    public float exaggerationMax = .4f;

    public PlayerController controller;
    SpriteRenderer spriteRenderer;
    float previousSpeedX = 0f;
    float previousSpeedY = 0f;

    public Sprite left;
    public Sprite right;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float speed_x = controller.velocity_x;
        float speed_y = controller.velocity_y;

        float deltaX = (Mathf.Abs(speed_x - previousSpeedX));
        float deltaY = (Mathf.Abs(speed_y - previousSpeedY));

        if (deltaX != 0)
        {
            spriteRenderer.sprite = Mathf.Sign(speed_x) > 0 ? right : left;
        }
        

        if(speed_x > speed_y)
        {
            transform.localScale = new Vector3(1 + Mathf.Min(exaggerationValue * Mathf.Abs(speed_x), exaggerationMax), 1 - Mathf.Min(exaggerationValue * Mathf.Abs(speed_x), exaggerationMax), 1);
        }
        else
        {
            transform.localScale = new Vector3(1 - Mathf.Min(exaggerationValue * Mathf.Abs(speed_y), exaggerationMax), 1 + Mathf.Min(exaggerationValue * Mathf.Abs(speed_y), exaggerationMax), 1);
        }

        previousSpeedX = speed_x;
        previousSpeedY = speed_y;
    }
}
