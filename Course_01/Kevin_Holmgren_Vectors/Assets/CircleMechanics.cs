using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMechanics : ProcessingLite.GP21
{
    public float diameter = 2.5f;
    public Vector2 circlePos;
    public Vector2 direction;

    [SerializeField]
    private float speedLimit = 20;
    [SerializeField]
    [Range(0, 2)]
    private float speed = 0;
    [Range(0, 2)]
    [SerializeField]
    private float velocity = 0;

    public float magnitude = 0;

    private void Start()
    {
        circlePos = new Vector2(circlePos.x + (Width / 2), circlePos.y + (Height / 2)); // set position in middle of screen.
    }

    void Update()
    {
        Vector2 mousePos = new(MouseX, MouseY);

        if (Input.GetMouseButtonDown(0))
        {
            circlePos = mousePos;
            velocity = 0;
        }

        Background(0);

        if (Input.GetMouseButton(0))
        {
            StrokeWeight(1.5f);
            Line(circlePos, mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = new Vector2(mousePos.x - circlePos.x, mousePos.y - circlePos.y);
            velocity = speed;
        }

        CheckCollision();

        if (direction.magnitude > speedLimit)
            direction = direction.normalized * speedLimit;

        magnitude = direction.magnitude;
        circlePos += velocity * Time.deltaTime * direction;

        StrokeWeight(1);
        Circle(circlePos.x, circlePos.y, diameter);
    }

    void CheckCollision()
    {
        float left = circlePos.x - (diameter / 2);
        float right = circlePos.x + (diameter / 2);
        float top = circlePos.y + (diameter / 2);
        float bottom = circlePos.y - (diameter / 2);

        if (left <= 0 && direction.x < 0 || right >= Width && direction.x > 0)
            direction = new Vector2(-direction.x, direction.y);

        if (bottom <= 0 && direction.y < 0 || top >= Height && direction.y > 0)
            direction = new Vector2(direction.x, -direction.y);
    }
}
