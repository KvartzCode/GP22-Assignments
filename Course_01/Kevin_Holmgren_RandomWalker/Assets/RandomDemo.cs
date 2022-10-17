using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDemo : ProcessingLite.GP22
{
    int timeStep = 0;   //So we can keep track of time

    //Keeping track of previous values so we can draw lines.
    float previousRandomValue = 0;
    float previousNoiseValue = 0;
    float previousCustomValue = 0;

    int[] gaussianNumbers;
    int gaussianSample = 200;

    void Start()
    {
        //Prepare our scene with all the following settings
        Application.targetFrameRate = 20;
        StrokeWeight(0.5f);
        Background(0);
        timeStep = 0;
        gaussianNumbers = new int[gaussianSample];
    }

    void Update()
    {
        //Restart the program on mouse click
        if (Input.GetMouseButtonDown(0))
            Start();

        //Loop the program 100 times then pause
        if (timeStep > 100)
            return;

        //Draw the following graphs
        //Comment out to draw one at a time.
        DrawRandomGraph();          //White graph, Normal randomization
                                    //DrawPerlinNoiseGraph();	 //Green graph, perlin noise.
                                    //DrawCustomGraph();          //Gold graph, a random graph that changes over time
                                    //DrawNomalizedGraph();       //light gray dots, normalized distribution.

        //Move one tick/frame/step
        timeStep++;
    }

    ///Draws a graph with completely random numbers.
    void DrawRandomGraph()
    {
        Stroke(255);

        //"Normal" random value between zero and screen height.
        float y = Random.Range(0, Height);

        //Calculate X positions that we use for visualizations
        float oldX = (timeStep - 1) * Width / 100;
        float x = timeStep * Width / 100; //100 data points distributed over the screen

        //Draw a line from the previous point to the new point
        Line(oldX, previousRandomValue, x, y);

        //Save the old point (we don't need X, it can be calculated)
        previousRandomValue = y;
    }

    //Add the other functions here inside the class.

}
