using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool inverseMovement;

    void FixedUpdate()
    {
        WallMovement();
    }

    // Metodo de movimiento de la pared simple modificando la posicion del transform
    void WallMovement()
    {
        if (!inverseMovement) // Podemos invertir la direccion, si se quiere, con el booleano. (por si acaso)
        {
            gameObject.transform.position += transform.right * movementSpeed * Time.deltaTime;
        }
        else
        {
            gameObject.transform.position += transform.right * -movementSpeed * Time.deltaTime;
        }
    }

}
