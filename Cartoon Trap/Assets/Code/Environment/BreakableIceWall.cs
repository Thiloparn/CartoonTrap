using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableIceWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PumEffectArea"))
        {
            Destroy(this.gameObject);
        }
    }
}
