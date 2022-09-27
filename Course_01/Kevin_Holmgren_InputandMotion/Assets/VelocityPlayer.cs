using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPlayer : ProcessingLite.GP21
{
    [SerializeField]
    private Vector2 playerPos;
    [SerializeField]
    private float diameter = 2;
    [SerializeField]
    private float velocity = 10;

    void Start()
    {
        playerPos = new(Width / 2, Height / 2);
    }

    void Update()
    {
        Background(0);

        playerPos.x += Input.GetAxisRaw("Horizontal") * velocity * Time.deltaTime;
        playerPos.y += Input.GetAxisRaw("Vertical") * velocity * Time.deltaTime;

        Stroke(255, 0, 0);
        Circle(playerPos.x, playerPos.y, diameter);
    }
}
