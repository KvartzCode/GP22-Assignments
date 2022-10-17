using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

class Example : IRandomWalker
{
    //Add your own variables here.
    //Do not use processing variables like width or height

    public string GetName()
    {
        return "Kalle"; //When asked, tell them our walkers name
    }

    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        //Select a starting position or use a random one.
        float x = Random.Range(0, playAreaWidth);
        float y = Random.Range(0, playAreaHeight);

        //a PVector holds floats but make sure its whole numbers that are returned!
        return new Vector2(x, y);
    }

    public Vector2 Movement()
    {
        //add your own walk behavior for your walker here.
        //Make sure to only use the outputs listed below.

        if (Input.GetMouseButtonUp(0))
        {
            GetCameraTexture();
        }

        Vector2 newDirection;
        switch (Random.Range(0, 4))
        {
            case 0:
                newDirection = new Vector2(-1, 0);
                break;
            case 1:
                newDirection = new Vector2(1, 0);
                break;
            case 2:
                newDirection = new Vector2(0, 1);
                break;
            default:
                newDirection = new Vector2(0, -1);
                break;
        }
        return newDirection;
    }

    public Texture2D GetCameraTexture(Camera camera = null)
    {
        if (camera == null)
            camera = Camera.main;

        RenderTexture renderTexture = new RenderTexture(camera.scaledPixelWidth, camera.scaledPixelHeight, 32);
        camera.targetTexture = renderTexture;
        camera.Render();

        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        List<Color> colors = tex2d.GetPixels().ToList();
        //List<Color> colors = tex2d.GetPixels().Select(x => new Color(x.r, x.g, x.b, x.a)).ToList();
        Debug.Log(colors[0].ToString());
        Debug.Log(colors[0].ToHexString());
        //Color32 colors = new Color(0,0,0);

        List<Color> uniqueColors = colors.Select(x => new Color(x.r, x.g, x.b, x.a)).Distinct().ToList();


        Debug.Log("Total different colors: " + uniqueColors.Count);
        //foreach (Color color in uniqueColors)
        //{
        //    Debug.Log($"R:{color.r} G:{color.g} B:{color.b} A:{color.a}");
        //}

        camera.targetTexture = null;
        camera.Render();

        return tex2d;
    }
}

//All valid outputs:
// Vector2(-1, 0);
// Vector2(1, 0);
// Vector2(0, 1);
// Vector2(0, -1);

//Any other outputs will kill the walker!