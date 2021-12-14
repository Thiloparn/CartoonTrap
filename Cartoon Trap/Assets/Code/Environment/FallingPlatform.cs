using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    #region VARIABLES
    [Header("Platform Parameters")]
    public float fallingTimer;
    public float delayDestroy;
    public float respawnTimer;

    float fallingTimerBackUp, delayDestroyBackUp, respawnTimerBackUp;

    bool isRespawn = false;
    bool startDelay = false;
    bool isFalling = false;
    Transform respawnPoint;

    Rigidbody2D myRb;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SaveBackup();
        myRb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartTimer();
        DelayDestroy();
        PlatformRespawn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().isGrounded())
        {
            
            isFalling = true;  // Iniciamos el temporizador cuando lo toca el player
        }

    }
    #region SETs & RESETs
    public void SaveBackup()
    {
        fallingTimerBackUp = fallingTimer;
        delayDestroyBackUp = delayDestroy;
        respawnTimerBackUp = respawnTimer;
    }
    public void SetTimers()
    {
        fallingTimer = fallingTimerBackUp;
        delayDestroy = delayDestroyBackUp;
        respawnTimer = respawnTimerBackUp;
    }
    #endregion

    #region METODOS
    public void StartTimer()
    {
        if (isFalling)
        {
            if (fallingTimer > 0)
            {
                fallingTimer -= 1 * Time.deltaTime;
            }
            if (fallingTimer <= 0f)
            {
                myRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;   // Desbloqueamos todos los constraints excepto Y
                myRb.AddForce(transform.up * -0.1f, ForceMode2D.Impulse);  // Damos un pequeño impulso para que se mueva (maldito sistema de fisicas de unity)
                startDelay = true;   // Iniciamos el delay de tiempo para destruir el objeto
            }
        }
    }

    public void DelayDestroy()   // Metodo para destruir el objeto despues de unos segundos
    {
        if (startDelay)    // Comprobamos si se tiene que destruir
        {
            if (delayDestroy > 0)
            {
                delayDestroy -= 1 * Time.deltaTime;   // Bajamos el temporizador
            }
            if (delayDestroy <= 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                isRespawn = true;
                //Destroy(this.gameObject);    // Destruimos los objetos
            }
        }
    }
    public void PlatformRespawn() // Metodo para el temporizador de spawn y reseteo de las variables
    {
        if (isRespawn)
        {
            if (respawnTimer > 0)
            {
                respawnTimer -= 1 * Time.deltaTime;
            }
            if(respawnTimer <= 0)
            {
                transform.position = respawnPoint.position;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                isRespawn = false;
                startDelay = false;
                isFalling = false;
                myRb.constraints = RigidbodyConstraints2D.FreezeAll;
                SetTimers();
            }
        }
    }
    #endregion

}
