using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : ProcessingLite.GP21
{
    public float size = 0.5f;
    public Vector2 Position { get => position; }

    [SerializeField]
    Vector2 position; //Ball position
    [SerializeField]
    Vector2 velocity; //Ball direction
    [SerializeField]
    int[] rgb = new int[3];

    public Ball(float x, float y, int r, int g, int b)
    {
        rgb = new int[] { r, g, b };
        position = new Vector2(x, y);

        velocity = new Vector2();
        velocity.x = Random.Range(-5, 5);
        velocity.y = Random.Range(-5, 5);
        //velocity.x = 0;
        //velocity.y = 0;
    }

    public void Draw()
    {
        Stroke(rgb[0], rgb[1], rgb[2]);
        Circle(position.x, position.y, size);
    }

    public void UpdatePos()
    {
        CheckWallCollision();
        position += velocity * Time.deltaTime;
    }

    private void CheckWallCollision()
    {
        if (position.x - (size / 2) < 0 && velocity.x < 0 || position.x + (size / 2) > Width && velocity.x > 0)
            velocity.x *= -1;

        if (position.y - (size / 2) < 0 && velocity.y < 0 || position.y + (size / 2) > Height && velocity.y > 0)
            velocity.y *= -1;
    }
}
