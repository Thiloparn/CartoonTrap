using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaCollider : MonoBehaviour
{
    public int damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            BasicEnemy bats = collision.gameObject.GetComponent<BasicEnemy>();
            Bush bush = collision.gameObject.GetComponent<Bush>();
            Hedgehog hedgehog = collision.gameObject.GetComponent<Hedgehog>();
            Mole mole = collision.gameObject.GetComponent<Mole>();
            Soldier soldier = collision.gameObject.GetComponent<Soldier>();
            Spider spider = collision.gameObject.GetComponent<Spider>();
            Turtle turtle = collision.gameObject.GetComponent<Turtle>();
            Boss boss = collision.gameObject.GetComponent<Boss>();

            if (bats != null)
            {
                bats.TakeDamage(damage, this.gameObject);
            }
            else if (bush != null)
            {
                bush.TakeDamage(damage, this.gameObject);
            }
            else if (hedgehog != null)
            {
                hedgehog.TakeDamage(damage, this.gameObject);
            }
            else if (mole != null)
            {
                mole.TakeDamage(damage, this.gameObject);
            }
            else if (soldier != null)
            {
                soldier.TakeDamage(damage, this.gameObject);
            }
            else if (spider != null)
            {
                spider.TakeDamage(damage, this.gameObject);
            }
            else if(turtle != null)
            {
                turtle.TakeDamage(damage, this.gameObject);
            } 
            else if (boss != null)
            {
                boss.TakeDamage(damage, this.gameObject);
            }
        }
    }
}
