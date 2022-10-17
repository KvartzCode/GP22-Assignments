using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float force = 100;

    [SerializeField] Rigidbody2D body;
    [SerializeField] SceneHandler sceneHandler;
    
    void Update()
    {
        Vector2 velocity = body.velocity; 
        velocity.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        body.velocity = velocity;
        if (Input.GetKeyDown(KeyCode.Space))
            body.AddForce(new Vector2(0, force));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            sceneHandler.ChangePoints(collision.gameObject);
            Debug.Log("Player triggered coin event!");
        }
    }
}
