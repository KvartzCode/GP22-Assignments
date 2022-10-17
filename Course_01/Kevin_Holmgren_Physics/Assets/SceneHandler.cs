using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] int pointsValue = 0;
    [SerializeField] int coinsValue = 10;
    [SerializeField] float coinsSpawnRate = 2.5f;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject coinPrefab;

    private Coroutine coroutine;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game Scene")
            return;

        pointsText.text = $"Points: {pointsValue}";
        coroutine = StartCoroutine("CoinSpawnTimer");
    }

    private void OnDestroy()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator CoinSpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(coinsSpawnRate);
            Instantiate(coinPrefab, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), new Quaternion());
        }
    }

    public void ChangePoints(GameObject coin)
    {
        if (coin == null || !coin.CompareTag("Coin"))
            return;

        pointsValue += coinsValue;

        if (pointsText != null)
            pointsText.text = $"Points: {pointsValue}";

        Destroy(coin);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
