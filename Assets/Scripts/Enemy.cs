using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject scoreUITextGo;
    public GameObject ExplosionGo;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector3.down*speed*Time.deltaTime);

        if (transform.position.y <=-5.81f){
            float randomX = Random.Range(-5.94f,5.88f);
            transform.position = new Vector3(randomX, 5.69f, transform.position.z);        
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag =="Player")
        {
           PlayerController player = collision.GetComponent<PlayerController>();
           player.RecieveDamage();
           PlayExplosion();
           scoreUITextGo.GetComponent<GameScore>().Score += 100;
           Destroy(this.gameObject);
        }
        if(collision.tag == "PlayerBulletTag")
        {
            PlayExplosion();
            scoreUITextGo.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);
        explosion.transform.position=transform.position;
    }
}
