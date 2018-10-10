﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the hazard");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Debug.Log("Something other than the player has entered the hazard");
        }
        
    }

}
