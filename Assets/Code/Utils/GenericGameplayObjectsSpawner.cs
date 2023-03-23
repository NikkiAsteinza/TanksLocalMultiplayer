using System.Collections;
using System.Collections.Generic;
using Tanks.Gameplay.Objects;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class GenericGameplayObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameplayObject> genericGameplayObjects;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int maxGenericGameplayObjectsAllowedAtTime;

        private List<GameplayObject> activeObjects = new List<GameplayObject>();
        private float timer;

        private void Start()
        {
            // Spawn objects in disabled state
            for (int i = 0; i < maxGenericGameplayObjectsAllowedAtTime; i++)
            {
                SpawnObject(false);
            }
        }

        private void Update()
        {
            // Check if we can activate more objects
            if (activeObjects.Count < maxGenericGameplayObjectsAllowedAtTime)
            {
                // Check if enough time has passed since last spawn
                timer += Time.deltaTime;
                if (timer >= spawnInterval)
                {
                    SpawnObject(true);
                    timer = 0f;
                }
            }
        }

        private void SpawnObject(bool activate)
        {
            // Get a random GameplayObject and spawn it at a random spawn point
            GameplayObject obj = genericGameplayObjects[Random.Range(0, genericGameplayObjects.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameplayObject instance = Instantiate(obj, spawnPoint.position, spawnPoint.rotation);

            // Activate or disable the object depending on the parameter
            instance.gameObject.SetActive(activate);

            // Add to the list of active objects if activated
            if (activate)
            {
                activeObjects.Add(instance);
            }
        }

        // Call this method to deactivate an object and remove it from the list of active objects
        public void DeactivateObject(GameplayObject obj)
        {
            obj.gameObject.SetActive(false);
            activeObjects.Remove(obj);
        }
    }
}
