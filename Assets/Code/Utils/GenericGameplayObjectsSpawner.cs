using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects.Generics;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class GenericGameplayObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private List<GenericGameplayObject> _genericGameplayObjects;
        [SerializeField] private float _spawnInterval = 5;
        
        private GenericGameplayObject _activatedObject;
        private float _timer;

        private void Awake()
        {
            _genericGameplayObjects = GetComponentsInChildren<GenericGameplayObject>().ToList();
            Debug.Log("Generics: " +_genericGameplayObjects.Count);
            DisableAll();
        }
        
        private void Update()
        {
            if (!_activatedObject)
            {
                _timer += Time.deltaTime;
                if (_timer >= _spawnInterval)
                {
                    Debug.Log("Generics spawner timer");
                    ActivateObject();
                    _timer = 0f;
                }
            }
        }
        
        private void DisableAll()
        {
            _genericGameplayObjects.ForEach((x) =>
            {
                x.gameObject.SetActive(false);
                x.SetSpawner(this);
            });
        }
        
        private void ActivateObject()
        {
            GenericGameplayObject obj = _genericGameplayObjects[Random.Range(0, _genericGameplayObjects.Count)];
            obj.gameObject.SetActive(true);
            _activatedObject = obj;
            Debug.Log("Generic gameplayObject activated");
        }

        public void ResetTimer()
        {
            _timer = 0;
            _activatedObject = null;
        }
    }
}
