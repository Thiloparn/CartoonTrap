using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHittablePlatform : MonoBehaviour
{
    Rigidbody2D myRb;
    bool isFalling = false;
    bool isStopped = false;

    //public bool pruebaFall = false;    // DESCOMENTAR SI SE QUIERE PROBAR

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*if (pruebaFall)
        {
            UnFreezeRigibody();          // DESCOMENTAR SI SE QUIERE PROBAR
            pruebaFall = false;
        }
        print(isFalling); */
    }

    // Comparamos con que colisiona el objeto
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) // Cuando colisiona con lo que desamos, ya sea floor como alguna cosa en concreto, se para
        {
            FreezeRigidBody();
        }

    }

    // Comparacion objetos falseados
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice_Floor_Platform")) // Cuando colisiona con lo que desamos, ya sea floor como alguna cosa en concreto, se para
        {
            FreezeRigidBody();
        }
    }

    // Metodo para bloquear el movimiento
    public void FreezeRigidBody()
    {
        myRb.constraints = RigidbodyConstraints2D.FreezeAll; // Bloquea todos los axis del rigidbody includo rotación
        isFalling = false;
    }

    // Metodo para que se pueda mover
    public void UnFreezeRigibody()
    {
        if (!isStopped) // Bool para que solo se desboquee una vez y evitamos problemas cuando ya ha tocado el suelo de destino
        {
            myRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // Bloqueamos todos los axis excepto Y
            myRb.AddForce(transform.up * -0.1f, ForceMode2D.Impulse); // Le damos un impulso minimo porque de por si no se mueve tras desbloquearlo
            isFalling = true;
            isStopped = true;
        }
    }
}
