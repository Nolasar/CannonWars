using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpawnProjectiles : MonoBehaviour
{
    // some kinds of projectiles
    [SerializeField] private List<GameObject> projectilePrefabs;
    // point, where projectile will be created
    [SerializeField] private Transform spawnPoint;
    // magazin
    private List<GameObject> projectiles;
    // game manager instance
    private GameManager gameManager;
    // initial delay and interval of shooting
    public float spawnInterval = 2.0f;
    public float startDelay = 1.0f;
    // index of corrent projectile
    [SerializeField] private int projectileIndex;
    // size of gun's magazin
    private int amountProjectile;
    private void Start()
    {
        // ref to game manager
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        // amount of projectiles
        amountProjectile = AmmoMagazinSize();
        // fill projectiles list with prefabs
        projectiles = FillMagazine();
        // shuffle projectiles list
        projectiles = Shuffle(projectiles);
        // shooting
        InvokeRepeating("Spawn", startDelay, spawnInterval);
    }

    private void Spawn()
    {
        // create projectiles while magazin not empty
        if (projectileIndex >= projectiles.Count)
        {
            // Finish shooting
            Debug.Log(gameObject.name + " end shooting");
            CancelInvoke("Spawn");
        }
        else
        {
            // Create projectiles
            Instantiate(projectiles[projectileIndex], spawnPoint.position, spawnPoint.rotation);
            projectileIndex++;
        }
    }

    // Calculate magazin size depends on game duration and projectile spawn interval
    private int AmmoMagazinSize()
    {
        int size = ((gameManager.playTimer - startDelay) / spawnInterval).ConvertTo<int>() - 1;
        // Magazin size must be multiple of projectile prefabs count
        return (size % projectilePrefabs.Count == 0) ? size: (size % projectilePrefabs.Count == 1) ? size-1: size-2;
    }

    // Куегкт filled list with prefabs
    // Amount of each kind of prefabs is equal
    private List<GameObject> FillMagazine()
    {
        List<GameObject> tmp = new List<GameObject>();
        for (int i = 0; i < projectilePrefabs.Count; i++)
        {
            for (int j = 0; j < amountProjectile / projectilePrefabs.Count; j++)
            {             
                tmp.Add(projectilePrefabs[i]);
            }
        }
        return tmp;
    }

    // Shuffle a list in random order( Fisher Yates Shuffle )
    private List<GameObject> Shuffle(List<GameObject> list)
    {
        System.Random rand = new System.Random();

        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            GameObject tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }
        return list;
    }
}
