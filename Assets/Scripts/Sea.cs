using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sea : MonoBehaviour
{   
    [SerializeField] public int water_id = 0;
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
        if(other.tag == "Player")
        {   
            player.life -= 1;
            switch(this.water_id){
                case 0:
                    player.transform.position = new Vector3(-13,1,0);
                    break;
                case 1:
                    player.transform.position = new Vector3(40,3,0);
                    break;
                case 2:
                    player.transform.position = new Vector3(40,3,0);
                    break;
                case 3:
                    player.transform.position = new Vector3(40,3,0);
                    break;
                case 4:
                    player.transform.position = new Vector3(93,-14,0);
                    break;
                default:
                    break; 
            }
             
        }
    }
}
