using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkerTest : ProcessingLite.GP22
{
    [SerializeField] List<Color32> colors = new List<Color32>();
    [SerializeField] List<Walker> walkers = new List<Walker>();
    [SerializeField] float scaleFactor = 0.05f;

    [SerializeField] int targetFrameRate = 120; // this trigger OnValidate()

    void Start()
    {
        Background(0);

        DrawSpawnArea(Width / 4, Width - (Width / 4), Height / 4, Height - (Height / 4));

        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;

        GeneratePlayers(typeof(IRandomWalker));

        foreach (var walker in walkers)
        {
            walker.GetStartPosition((int)(Width / scaleFactor), (int)(Height / scaleFactor));
        }
    }

    void Update()
    {
        foreach (var walker in walkers)
        {
            Color32 col = walker.color;
            Stroke(col.r, col.g, col.b);
            Point(walker.walkerPos.x * scaleFactor, walker.walkerPos.y * scaleFactor);
            walker.Movement();
        }
    }

    private void DrawSpawnArea(float left, float right, float bot, float top)
    {
        Line(left, top, right, top);
        Line(right, top, right, bot);
        Line(right, bot, left, bot);
        Line(left, bot, left, top);
    }

    void GeneratePlayers(Type interfaceType)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p)).ToArray();

        for (int i = 0; i < types.Count(); i++)
        {
            if (types[i].FullName == "IRandomWalker")
                continue;

            Color32 color;
            if (i - 1 < colors.Count)
                color = colors[i - 1];
            else
                color = new Color(1, 1, 1);

            IRandomWalker walker = Activator.CreateInstance(types[i]) as IRandomWalker;
            walkers.Add(new Walker(walker, color, types[i].FullName));
        }
    }

    void OnValidate()
    {
        Application.targetFrameRate = targetFrameRate;
    }



    public class Walker
    {
        public string name;
        public Vector2 walkerPos;
        public Color32 color;
        IRandomWalker walker;

        public Walker(object walker, Color32 color, string name = null)
        {
            this.walker = walker as IRandomWalker;
            this.color = color;

            if (name == null)
                this.name = GetName();
            else
                this.name = name;

            Debug.Log("Name: '" + name + "' color: {" + color + "}");
        }

        public string GetName()
        {
            return walker.GetName();
        }

        public void GetStartPosition(int playAreaWidth, int playAreaHeight)
        {
            walkerPos = walker.GetStartPosition(playAreaWidth, playAreaHeight);
        }

        public void Movement()
        {
            walkerPos += walker.Movement();
        }
    }
}
