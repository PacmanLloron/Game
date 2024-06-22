using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZombiesPickup : MonoBehaviour
{
    public float stopDuration = 5f; 
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            ZombieController[] zombies = FindObjectsOfType<ZombieController>();
            foreach (ZombieController zombie in zombies)
            {
                zombie.StopZombie(stopDuration);
            }

            ZombieControllerLevel2[] zombies2 = FindObjectsOfType<ZombieControllerLevel2>();
            foreach (ZombieControllerLevel2 zombie in zombies2)
            {
                zombie.StopZombie(stopDuration);
            }

            Destroy(gameObject); 
        }
    }
}
