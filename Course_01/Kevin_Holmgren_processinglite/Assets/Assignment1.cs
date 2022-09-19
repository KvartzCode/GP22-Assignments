using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Assignment1 : ProcessingLite.GP21
{
    public int curves = 15;

    public Transform start;
    public Transform middle;
    public Transform end;
    public float startX = 9.337861f;
    public float startY = 5;

    public Color color1;
    public Color color2;
    public Color color3;

    void Update()
    {
        Background(0);
        WriteName();
        DrawParabolicCurve();
    }

    void WriteName()
    {
        Stroke(255, 255, 255);

        // K
        Line(1, 7, 1, 5);
        Line(1, 6, 2, 7);
        Line(1, 6, 2, 5);

        // E
        Line(3, 7, 3, 5);
        Line(3, 7, 4, 7);
        Line(3, 6, 4, 6);
        Line(3, 5, 4, 5);

        // V
        Line(5, 7, 5.5f, 5);
        Line(5.5f, 5, 6, 7);

        // I
        Line(7, 7, 8, 7);
        Line(7.5f, 7, 7.5f, 5);
        Line(7, 5, 8, 5);

        // N
        Line(9, 5, 9, 7);
        Line(9, 7, 10, 5);
        Line(10, 5, 10, 7);
    }

    void DrawParabolicCurve()
    {
        Color color = new Color(1, 1, 1);
        int colorNumber = 1;
        for (int i = 0; i < curves + 1; i++)
        {
            if (i % 3 == 0)
            {
                // Would put a switch here but intellisense didn't like my syntax for some reason so this'll have to do
                if (colorNumber == 1)
                    color = color1;
                else if (colorNumber == 2)
                    color = color2;
                else if (colorNumber == 3)
                {
                    color = color3;
                    colorNumber = 0;
                }
                colorNumber++;
            }

            Vector3 test1 = (middle.position - start.position) / curves;
            Vector3 test2 = (end.position - middle.position) / curves;
            Vector2 lineStart = start.position + test1 * i;
            Vector2 lineEnd = middle.position + test2 * i;

            Stroke(255 * Mathf.RoundToInt(color.r), 255 * Mathf.RoundToInt(color.g), 255 * Mathf.RoundToInt(color.b));
            // For some reason the camera change X and Y positions at start so adjust start drawing points accordingly
            Line(lineStart.x + startX, lineStart.y + startY, lineEnd.x + startX, lineEnd.y + startY);
        }
    }
}
