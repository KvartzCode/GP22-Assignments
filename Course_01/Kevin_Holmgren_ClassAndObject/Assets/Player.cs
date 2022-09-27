using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ProcessingLite.GP21
{
    public float size = 2;
    public float Speed { get => speed; }
    public Vector2 Position { get => position; }

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    Vector2 position;
    [SerializeField]
    Vector2 velocity;

    void Start()
    {
        position = new Vector2(Width / 2, Height / 2);
    }

    void Update()
    {
        Background(50, 166, 240);

        position.x = Mathf.Clamp(position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0 + size / 2, Width - size / 2);
        position.y = Mathf.Clamp(position.y + Input.GetAxis("Vertical") * speed * Time.deltaTime, 0 + size / 2, Height - size / 2);

        Stroke(255);
        Circle(position.x, position.y, size);
    }
}
