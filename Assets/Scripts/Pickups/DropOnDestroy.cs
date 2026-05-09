using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPickup;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    public void CheckDrop()
    {
        if(Random.value < chance)
        {
            GameObject toDrop = dropItemPickup[Random.Range(0, dropItemPickup.Count)];
            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
    }
}
