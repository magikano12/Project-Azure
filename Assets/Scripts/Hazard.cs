using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hazard : MonoBehaviour
{

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the hazard");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Die();  
        }

        else
        {
            Debug.Log("Something other than the player has entered the hazard");
        }
        
    }

}
