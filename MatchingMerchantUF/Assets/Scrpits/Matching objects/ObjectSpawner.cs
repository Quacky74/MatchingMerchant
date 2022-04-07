using System.Collections.Generic;
using UnityEngine;

namespace Scrpits.Matching_objects
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Vector3 SpawnPos;
        [SerializeField] private int SpawnCounter;
        [SerializeField] private GameObject RedPrefab;
        [SerializeField] private GameObject YellowPrefab;
        [SerializeField] private GameObject GreenPrefab;
        [SerializeField] private GameObject PurplePrefab;
        [SerializeField] private GameObject BluePrefab;
        [SerializeField] private List<GameObject> SpawnedObjects;
  


        // Start is called before the first frame update
        void Start()
        {
            SpawnedObjects = new List<GameObject>();
            Spawn();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void Spawn()
        {
            for (int i = 0; i < SpawnCounter; i++)
            {
                SpawnedObjects.Add(Instantiate(RedPrefab,SpawnPos, Quaternion.identity));
                SpawnedObjects.Add(Instantiate(GreenPrefab,SpawnPos, Quaternion.identity));
                SpawnedObjects.Add(Instantiate(BluePrefab,SpawnPos, Quaternion.identity));
                //  SpawnedObjects.Add(Instantiate(RedPrefab,SpawnPos, Quaternion.identity));
            }
        }


    }
}
