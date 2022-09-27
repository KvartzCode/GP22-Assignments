using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPlayer : ProcessingLite.GP21
{
    [SerializeField]
    private Vector2 playerPos;
    [SerializeField]
    private float diameter = 2;
    [SerializeField]
    private float velocity = 0;
    [SerializeField]
    private float maxSpeed = 20;
    [SerializeField]
    private float accelSpeed = 0.1f;
    [SerializeField]
    private float accelerationX = 0;
    [SerializeField]
    private float accelerationY = 0;

    void Start()
    {
        playerPos = new(Width / 2, Height / 2);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            bool movementKeyPressed = false;

            if (Input.GetKey(KeyCode.W))
            {
                accelerationY += accelSpeed;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                accelerationY -= accelSpeed;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                accelerationX += accelSpeed;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                accelerationX -= accelSpeed;
                movementKeyPressed = true;
            }

            if (!movementKeyPressed)
                DeAccelerate();
        }
        else
        {
            DeAccelerate();
        }

        CheckMaxSpeed();
        playerPos.x += accelerationX * Time.deltaTime;
        playerPos.y += accelerationY * Time.deltaTime;
        //CheckCollision();

        Stroke(0, 0, 255);
        Circle(playerPos.x, playerPos.y, diameter);
    }

    // Scrapped
    void CheckCollision()
    {
        Vector2 leftWidth = new(playerPos.x - (diameter / 2), playerPos.x + (diameter / 2)); // This is a size Vector
        Vector2 botHeight = new(playerPos.y - (diameter / 2), playerPos.y + (diameter / 2)); // This is a size Vector

        if (leftWidth.x < 0 || leftWidth.y > Width)
        {
            accelerationX = accelerationX / 2 * -1;
        }

        if (botHeight.x < 0 || botHeight.y > Height)
        {
            accelerationY = accelerationY / 2 * -1;
        }
    }

    void CheckMaxSpeed()
    {
        if (accelerationX > maxSpeed)
            accelerationX = maxSpeed;
        else if (accelerationX < -maxSpeed)
            accelerationX = -maxSpeed;

        if (accelerationY > maxSpeed)
            accelerationY = maxSpeed;
        else if (accelerationY < -maxSpeed)
            accelerationY = -maxSpeed;
    }

    void DeAccelerate()
    {
        if (accelerationX > 0)
            accelerationX -= accelSpeed;
        else if (accelerationX < 0)
            accelerationX += accelSpeed;
        else
            accelerationX = 0;

        if (accelerationY > 0)
            accelerationY -= accelSpeed;
        else if (accelerationY < 0)
            accelerationY += accelSpeed;
        else
            accelerationY = 0;
    }
}
