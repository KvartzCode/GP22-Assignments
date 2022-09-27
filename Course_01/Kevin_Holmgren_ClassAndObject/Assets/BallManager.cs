using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : ProcessingLite.GP21
{
    public int maxBallCount = 100;
    public float size = 0.5f;
    public int r = 255;
    public int g = 0;
    public int b = 0;
    //private int numberOfBalls = 0;

    [SerializeField]
    private Player player;
    private List<Ball> balls = new List<Ball>();

    DateTime startTimer = DateTime.Now;
    DateTime shortTimer = DateTime.Now;

    void Start()
    {
        //balls = new Ball[maxBallCount];
        //balls[numberOfBalls] = new Ball(UnityEngine.Random.Range(1, Width - 1), UnityEngine.Random.Range(1, Height - 1), r, g, b);
        //balls.Add(new Ball(UnityEngine.Random.Range(1, Width - 1), UnityEngine.Random.Range(1, Height - 1), r, g, b));
        //numberOfBalls++;
        AddBall();
    }

    void Update()
    {
        //Background(50, 166, 240);

        //for (int i = 0; i < balls.Length; i++)
        //{
        //    balls[i].UpdatePos();
        //    balls[i].Draw();
        //}

        //for (int i = 0; i < numberOfBalls; i++)
        //{
        //    balls[i].UpdatePos();
        //    balls[i].Draw();
        //}

        foreach (Ball ball in balls)
        {
            ball.UpdatePos();
            ball.Draw();
        }

        if (CheckPlayerCollide())
            Debug.LogWarning("Player was hit!");

        Text((DateTime.Now - startTimer).ToString(@"mm\:ss\.fff"), Width / 2, Height * 0.75f);

        if ((DateTime.Now - shortTimer).Seconds >= 3)
        {
            //Debug.Log("Spawn object! " + (DateTime.Now - shortTimer).Seconds);
            //numberOfBalls++;
            //balls[numberOfBalls] = new Ball(UnityEngine.Random.Range(1, Width - 1), UnityEngine.Random.Range(1, Height - 1), r, g, b);
            AddBall();
            shortTimer = DateTime.Now;
        }
    }

    bool CheckPlayerCollide()
    {
        foreach (Ball ball in balls)
        {
            if (CircleCollision(ball))
                return true;
        }

        return false;
    }

    //Check collision, 2 circles
    //x1, y1 is the position of the first circle
    //size1 is the radius of the first circle
    //then the same data for circle number two

    //function will return true (collision) or false (no collision)
    //bool is the return type

    bool CircleCollision(Ball ball)
    {
        float maxDistance = (player.size / 2) + (ball.size / 2);

        //first a quick check to see if we are too far away in x or y direction
        //if we are far away we don't collide so just return false and be done.
        if (Mathf.Abs(player.Position.x - ball.Position.x) > maxDistance || Mathf.Abs(player.Position.y - ball.Position.y) > maxDistance)
        {
            return false;
        }
        //we then run the slower distance calculation
        //Distance uses Pythagoras to get exact distance, if we still are to far away we are not colliding.
        else if (Vector2.Distance(new Vector2(player.Position.x, player.Position.y), new Vector2(ball.Position.x, ball.Position.y)) > maxDistance)
        {
            return false;
        }
        //We now know the points are closer then the distance so we are colliding!
        else
        {
            return true;
        }
    }
    //A better way to do this is to have a circle object and pass the objects in to the function,
    //then we just have to pass 2 objects instead of 6 different values.

    void AddBall()
    {
        if (balls.Count < maxBallCount)
            balls.Add(new Ball(UnityEngine.Random.Range(1, Width - 1), UnityEngine.Random.Range(1, Height - 1), r, g, b));
        else if (balls.Count > maxBallCount)
            balls.RemoveRange(maxBallCount, balls.Count - maxBallCount);
    }
}
