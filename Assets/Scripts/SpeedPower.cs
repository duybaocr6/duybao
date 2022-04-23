using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    public float speed = 2;
    public float powerUpId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* speed *Time.deltaTime);
        if(transform.position.y <=-5.45f)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (powerUpId ==0)
            {
                playerController.PlayerSpeed();
            }
            if (powerUpId ==1)
            {
                playerController.PlayerSpeedUp();
            }
            if (powerUpId ==2)
            {
                playerController.PlayerUp();
            }
            Destroy(this.gameObject);
        }
    }
}
