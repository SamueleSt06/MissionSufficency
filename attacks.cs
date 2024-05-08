using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacks : MonoBehaviour
{
    public Patrolling chasing;
    public Movement speed;
    public GameObject playerBody;
    public MouseCameraLook camerella;
    public GameObject camera1;
    public GameObject DeathCamera;
    private Animator animator;
    public GameObject light;
    public GameObject DeathScreen;


    public GameObject tubo;

    public int health;

    void Start()
    {
        health = 100;
        chasing.enabled = true;
        animator = tubo.GetComponent<Animator>();
    }

    void Update()
    {


        if (health == 100)
        {
            chasing.enabled = true;
            speed.walkSpeed = 5f;
            speed.runSpeed = 10f;
        }

        if (health == 50)
        {
            speed.walkSpeed = 7f;
            speed.runSpeed = 7f;
            chasing.enabled = false;
            Invoke("ReactivateChasing", 15f);
            health = 49; // Setting health to 0 to avoid retriggering this condition every frame
        }
    }

    void ReactivateChasing()
    {
        chasing.enabled = true;
        Debug.Log("Riattivato");
    }

    public void attacked()
    {
        if (health == 100)
        {
            health = 50;

        }
        if(health == 48)
        {
            health = 0;
            camera1.SetActive(false);
            light.SetActive(true);
            DeathCamera.SetActive(true);
            DeathScreen.SetActive(true);
            animator.SetTrigger("TrCaduta");
            speed.enabled = false;
            camerella.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    
}