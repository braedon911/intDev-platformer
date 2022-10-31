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

    int facing = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        if (axis > 0)
        {
            facing = -1;
            transform.localPosition = spriteRenderer.localBounds.size.x * Vector3.right;
        }
        else if (axis < 0)
        {
            facing = 1;
            transform.localPosition = Vector3.zero;
        }

        float speed_x = controller.velocity_x;
        float speed_y = controller.velocity_y;

        float deltaX = (Mathf.Abs(speed_x - previousSpeedX));
        float deltaY = (Mathf.Abs(speed_y - previousSpeedY));
        

        if(Mathf.Abs(speed_x) > Mathf.Abs(speed_y))
        {
            transform.localScale = new Vector3(facing * (1 + Mathf.Min(exaggerationValue * Mathf.Abs(speed_x), exaggerationMax)), 1 - Mathf.Min(exaggerationValue * Mathf.Abs(speed_x), exaggerationMax), 1);
        }
        else
        {
            transform.localScale = new Vector3(facing * (1 - Mathf.Min(exaggerationValue * Mathf.Max(speed_y,0), exaggerationMax)), 1 + Mathf.Min(exaggerationValue * Mathf.Max(speed_y,0), exaggerationMax), 1);
        }

        previousSpeedX = speed_x;
        previousSpeedY = speed_y;
    }
}
