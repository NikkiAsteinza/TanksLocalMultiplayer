using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects.Generics;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class GenericGameplayObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private List<GenericGameplayObject> genericGameplayObjects;
        [SerializeField] private float spawnInterval = 5;
        
        private GenericGameplayObject _activatedObject;
        private float timer;

        private void Awake()
        {
            genericGameplayObjects = GetComponentsInChildren<GenericGameplayObject>().ToList();
            Debug.Log("Generics: " +genericGameplayObjects.Count);
            DisableAll();
        }

        private void DisableAll()
        {
            genericGameplayObjects.ForEach((x) =>
            {
                x.gameObject.SetActive(false);
                x.SetSpawner(this);
            });
        }

        private void Update()
        {
            if (!_activatedObject)
            {
                timer += Time.deltaTime;
                if (timer >= spawnInterval)
                {
                    Debug.Log("Generics spawner timer");
                    ActivateObject();
                    timer = 0f;
                }
            }
        }

        private void ActivateObject()
        {
            GenericGameplayObject obj = genericGameplayObjects[Random.Range(0, genericGameplayObjects.Count)];
            obj.gameObject.SetActive(true);
            _activatedObject = obj;
            Debug.Log("Generic gameplayObject activated");
        }

        public void ResetTimer()
        {
            timer = 0;
            _activatedObject = null;
        }
    }
}
