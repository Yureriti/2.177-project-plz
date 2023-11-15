using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Movement : MonoBehaviour
{
    public Rigidbody player;
    public float m_speed, rotatespeed;
    public float switchHeight = 100.0f; 
    public string scenename;

    void FixedUpdate()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            player.velocity = transform.forward * m_speed * Time.deltaTime;
        }
        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            player.velocity = -transform.forward * m_speed * Time.deltaTime;
        }
        // Move up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.velocity = transform.up * m_speed * Time.deltaTime;
        }
        // Move down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.velocity = -transform.up * m_speed * Time.deltaTime;
        }
    }

    void Update()
    {
        // Rotate left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
        }
        // Rotate right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }

        // Check the player's height
        if (player.transform.position.y > switchHeight)
        {
            // Switch to scene named "Scene2". Make sure this scene is added to your build settings.
            SceneManager.LoadScene(scenename);
        }
    }
}
