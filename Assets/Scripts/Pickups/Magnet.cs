using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public CircleCollider2D magnetCollider;
       private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CoinPickUp>(out CoinPickUp coin))
        {
            coin.SetTarget(transform.parent.position);
        }
        
        if (collision.gameObject.TryGetComponent<GemPickUpObject>(out GemPickUpObject Gem))
        {
            Gem.SetTarget(transform.parent.position);
        }
        
        if (collision.gameObject.TryGetComponent<HealPickupObject>(out HealPickupObject heal))
        {
            heal.SetTarget(transform.parent.position);
        }
    } 
   
}
