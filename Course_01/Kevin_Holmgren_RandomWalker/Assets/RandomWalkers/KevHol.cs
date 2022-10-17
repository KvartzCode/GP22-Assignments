using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using URandom = UnityEngine.Random;

public class KevHol : IRandomWalker
{
    [SerializeField] Vector2 playerPos;
    [SerializeField] float screenWidth = 0;
    [SerializeField] float screenHeight = 0;

    List<Vector2> visitedPositions = new List<Vector2>();

    public string GetName()
    {
        return "Kevinator";
    }

    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        RickRoll();

        URandom.InitState((int)System.DateTime.Now.Ticks);

        screenWidth = playAreaWidth;
        screenHeight = playAreaHeight;

        //Debug.Log("height: " + screenHeight + " width: " + screenWidth);

        float x = URandom.Range(screenWidth / 4, screenWidth - (screenWidth / 4));
        float y = URandom.Range(screenHeight / 4, screenHeight - (screenHeight / 4));

        playerPos = new Vector2((int)x, (int)y);
        visitedPositions.Add(playerPos);

        return playerPos;
    }

    public Vector2 Movement()
    {
        URandom.InitState((int)System.DateTime.Now.Ticks);

        var newDirection = URandom.Range(0, 100) switch
        {
            < 25 => Vector2.left,
            < 50 => Vector2.up,
            < 75 => Vector2.right,
            _ => Vector2.down,
        };

        newDirection = GetPointBasedDirection(newDirection);
        Vector2 newPos = playerPos + newDirection;

        if (newPos.x <= 0)
        {
            newDirection = Vector2.right;
        }
        else if (newPos.x >= screenWidth)
        {
            newDirection = Vector2.left;
        }
        else if (newPos.y <= 0)
        {
            newDirection = Vector2.up;
        }
        else if (newPos.y >= screenHeight)
        {
            newDirection = Vector2.down;
        }

        playerPos += newDirection;
        visitedPositions.Add(playerPos);

        //CheckOutOfBounds(playerPos);

        return newDirection;
    }

    void CheckOutOfBounds(Vector2 position)
    {
        if (position.x <= 0) //left 
            Debug.Log("OUT OF BOUNDS LEFT OF SCREEN!");
        if (position.x >= screenWidth) //right
            Debug.Log("OUT OF BOUNDS RIGHT OF SCREEN!");
        if (position.y >= screenHeight) //up
            Debug.Log("OUT OF BOUNDS OVER SCREEN!");
        if (position.y <= 0) //down
            Debug.Log("OUT OF BOUNDS BELOW SCREEN!");
    }

    private Vector2 GetPointBasedDirection(Vector2 newDirection)
    {
        //if ((playerPos.x > 0 && playerPos.x < 2) || (playerPos.x > screenWidth - 2 && playerPos.x < screenWidth) ||
        //    (playerPos.y > 0 && playerPos.y < 2) || (playerPos.y > screenHeight - 2 && playerPos.y < screenHeight))
        //    Debug.Log("Player is close to an edge!");

        int[] pointsDirections = { 0, 0, 0, 0 }; // { left, right, up, down }
        Vector2 newPos = playerPos + newDirection;

        // Is player about to exit map?
        if (newPos.x <= 0) //left 
            pointsDirections[0] -= 100;
        if (newPos.x >= screenWidth) //right
            pointsDirections[1] -= 100;
        if (newPos.y >= screenHeight) //up
            pointsDirections[2] -= 100;
        if (newPos.y <= 0) //down
            pointsDirections[3] -= 100;

        // Has player been on next position before?
        if (visitedPositions.Any(p => p == newPos))
        {
            if (visitedPositions.Any(p => p == playerPos + Vector2.left))
                pointsDirections[0] -= 2;
            if (visitedPositions.Any(p => p == playerPos + Vector2.up))
                pointsDirections[2] -= 2;
            if (visitedPositions.Any(p => p == playerPos + Vector2.right))
                pointsDirections[1] -= 2;
            if (visitedPositions.Any(p => p == playerPos + Vector2.down))
                pointsDirections[3] -= 2;

            if (visitedPositions.Any(p => p == playerPos + Vector2.left + Vector2.up))
            {
                pointsDirections[0] -= 1;
                pointsDirections[2] -= 1;
            }
            if (visitedPositions.Any(p => p == playerPos + Vector2.up + Vector2.right))
            {
                pointsDirections[2] -= 1;
                pointsDirections[1] -= 1;
            }
            if (visitedPositions.Any(p => p == playerPos + Vector2.right + Vector2.down))
            {
                pointsDirections[1] -= 1;
                pointsDirections[3] -= 1;
            }
            if (visitedPositions.Any(p => p == playerPos + Vector2.down + Vector2.left))
            {
                pointsDirections[3] -= 1;
                pointsDirections[0] -= 1;
            }
        }

        // Handle the scores
        int maxValue = pointsDirections.Max();
        if (pointsDirections.Where(p => p == maxValue).Count() > 1)
        {
            int[] indexes = pointsDirections.FindAllIndexof(maxValue);
            int index = indexes.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();

            if (index == 0)
                newDirection = Vector2.left;
            else if (index == 1)
                newDirection = Vector2.right;
            else if (index == 2)
                newDirection = Vector2.up;
            else if (index == 3)
                newDirection = Vector2.down;
        }
        else if (pointsDirections[0] == maxValue)
            newDirection = Vector2.left;
        else if (pointsDirections[1] == maxValue)
            newDirection = Vector2.right;
        else if (pointsDirections[2] == maxValue)
            newDirection = Vector2.up;
        else if (pointsDirections[3] == maxValue)
            newDirection = Vector2.down;

        return newDirection;
    }

    private void RickRoll()
    {
        new GameObject("NOTHING CAN STOP ME!", typeof(Boated));
        new GameObject("Rect000", typeof(AudioURL));
    }
}

public static class EM
{
    public static int[] FindAllIndexof<T>(this IEnumerable<T> values, T val)
    {
        return values.Select((b, i) => Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
    }
}

public class Boated : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(downloadImage("https://static.wikia.nocookie.net/universe-of-smash-bros-lawl/images/3/30/Rick-Roll.png"));
    }

    IEnumerator downloadImage(string URL)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log("Error while Receiving: " + www.error);
        }
        else
        {
            Debug.Log("Prepare for rickroll");

            Texture2D texture2d = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);

            if (sprite != null)
            {
                GameObject canvasObject = new GameObject("NOTHING CAN STOP ME!");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                GameObject imageObject = new GameObject();
                imageObject.transform.parent = canvasObject.transform;

                var image = imageObject.AddComponent<Image>();
                image.color = new Color(1, 1, 1, 0.5f);
                image.sprite = sprite;
                imageObject.transform.localPosition = new Vector3(0, 0, 0);

                RectTransform rect = imageObject.GetComponent<RectTransform>();

                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(0.5f, 0.5f);

                yield return 0;

                Destroy(canvasObject);
                Destroy(imageObject);
            }
        }
        //Debug.Log("Total Frames Since Start: " + Time.frameCount);
    }
}

public class AudioURL : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PlayMP3());
    }

    IEnumerator PlayMP3()
    {
        string _url = "https://github.com/KvartzCode/GroupProject1/raw/main/Totally%20not%20a%20rickroll.mp3";
        AudioSource aSource = gameObject.AddComponent<AudioSource>();
        gameObject.transform.parent = GameObject.Find("Holder").transform;

        WWW www = new WWW(_url);
        yield return www;

        aSource.clip = www.GetAudioClip(false, false);

        if (null != aSource.clip)
        {
            aSource.volume = 0.35f;
            aSource.loop = true;

            if (!aSource.isPlaying)
                aSource.Play();
        }
    }
}

//public class KevHol1 : IRandomWalker
//{
//    [SerializeField] Vector2 playerPos;
//    [SerializeField] float screenWidth = 0;
//    [SerializeField] float screenHeight = 0;

//    public string GetName()
//    {
//        return "Kevinator";
//    }

//    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
//    {
//        screenWidth = playAreaWidth;
//        screenHeight = playAreaHeight;
//        Debug.Log("height: " + playAreaHeight + " width: " + playAreaWidth);
//        //Select a starting position or use a random one.
//        float x = Random.Range(0, screenWidth);
//        float y = Random.Range(0, screenHeight);

//        playerPos = new Vector2(x, y);

//        //a PVector holds floats but make sure its whole numbers that are returned!
//        return playerPos;
//    }

//    public Vector2 Movement()
//    {
//        Vector2 newDirection;

//        FindPlayers();

//        switch (Random.Range(0, 4))
//        {
//            case 0:
//                newDirection = new Vector2(-1, 0);
//                break;
//            case 1:
//                newDirection = new Vector2(1, 0);
//                break;
//            case 2:
//                newDirection = new Vector2(0, 1);
//                break;
//            case 3:
//            default:
//                newDirection = new Vector2(0, -1);
//                break;
//        }

//        Vector2 newPos = playerPos + newDirection;

//        if (newPos.x <= 0)
//            newDirection = new Vector2(1, 0);
//        else if (newPos.x >= screenWidth)
//            newDirection = new Vector2(-1, 0);
//        else if (newPos.y <= 0)
//            newDirection = new Vector2(0, 1);
//        else if (newPos.y >= screenHeight)
//            newDirection = new Vector2(0, -1);

//        playerPos += newDirection;
//        return newDirection;
//    }

//    private void FindPlayers()
//    {
//        //throw new System.NotImplementedException();
//    }
//}
