using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        PlayerController player = other.GetComponent<PlayerController>();
        if(other.tag == "Player"){
            player.ActivateJumpBonus();
            Destroy(this.gameObject);
        } 
    }
    
}
