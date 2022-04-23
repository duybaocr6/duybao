using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public GameObject GameManagerGo;
    public float speed = 2;         //
    const float Maxspeed = 10;
    private float verticalInput;    //
    private float horizontalInput;  //

    public float health = 100;

    public GameObject laserPrefab;  //
    public Transform laserLaunchPos;//

    public float canFire;           //
    public float fireRate = 0.2f;   //
    public bool canSpeedUp = false;
    public bool canSpeed = false;
    public GameObject ExplosionGo;
    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;
    private Material matWhile;
    private Material matDefault;
    SpriteRenderer sr;
    private Animator ani;
    private Rigidbody2D rg;
    public GameObject Player;
    GameObject scoreUITextGo1;
    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = new Vector2(0,0);
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
       sr=GetComponent<SpriteRenderer>();
       matWhile = Resources.Load("WhileFlash",typeof(Material)) as Material;
       matDefault = sr.material;
       ani = Player.GetComponent<Animator>();
       scoreUITextGo1 = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
          Moverment();
          Shooting();
    }
    public void RecieveDamage()
    {
        if(canSpeed==false)
        {
            health -= 30;
        }
        
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void RecieveHealth()
    {
        if(canSpeed==false)
        {
            health += 30;
        }
        
        if(health >= 100)
        {
            health =100;
        }
    }

    void Shooting()
    {
        canFire = canFire + Time.deltaTime;

        if(canFire >= fireRate)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                GetComponent<AudioSource>().Play();
                Instantiate(laserPrefab, laserLaunchPos.position, Quaternion.identity);
                canFire = 0;
                
            }
        } 
    }
    void Moverment()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if(canSpeedUp==true)
        {
             transform.Translate(new Vector3(horizontalInput, verticalInput,0)*8*Time.deltaTime);

        }
        else
        {
             transform.Translate(new Vector3(horizontalInput, verticalInput,0)*speed*Time.deltaTime);
        }

        transform.Translate(new Vector3(horizontalInput, verticalInput,0)*speed*Time.deltaTime);
         if(transform.position.y >= 4.43f)
        {
            transform.position = new Vector3(transform.position.x, 4.43f,0);
        } 
        if(transform.position.y <= -4.42f)
        {
            transform.position = new Vector3(transform.position.x, -4.42f,0);
        } 
        if(transform.position.x >= 8.37f)
        {
            transform.position = new Vector3(8.37f,transform.position.y,0);
        } 
        if(transform.position.x <= -8.37f)
        {
            transform.position = new Vector3(-8.37f,transform.position.y,0);
        }
    }
    public void PlayerSpeed()
    {
        canSpeedUp=true;
        StartCoroutine(SpeedDownRoutine());
    }
    IEnumerator SpeedDownRoutine()
    {
        yield return new WaitForSeconds(1);
        canSpeedUp = false;
        scoreUITextGo1.GetComponent<GameScore>().Score += 300;
    }

    public void PlayerSpeedUp()
    {
        canSpeed=true;
        StartCoroutine(SpeedUpRoutine());
    }
    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(1);
        canSpeed = false;
        if(lives<3)
        {
            lives += 1;
            LivesUIText.text = lives.ToString();
        }
        lives = MaxLives;
        gameObject.SetActive(true);
    }
    public void PlayerUp()
    {
        canSpeed=true;
        StartCoroutine(SpeedRoutine());
    }
    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(1);
        canSpeed = false;
        if(speed<8)
        {
            speed += 1;
        }
        else
        speed = Maxspeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Enemy")
        {
            
            // PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            sr.material = matWhile;
            if(lives == 0)
            {
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                PlayExplosion();
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Invoke("ResetMaterial",.1f);
            }
        }
        if(collision.tag == "EnemyBulletTag1")
        {
            
            // PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            sr.material = matWhile;
            if(lives == 0)
            {
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                PlayExplosion();
                
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Invoke("ResetMaterial",.1f);
            }
        }
    }
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }
    int PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);
        explosion.transform.position=transform.position;
        return 1;
    }
}
