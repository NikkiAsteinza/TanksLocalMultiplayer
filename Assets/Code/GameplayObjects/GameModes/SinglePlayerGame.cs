using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tanks.GameplayObjects;
using TMPro;
using UnityEngine;

public class SinglePlayerGame : MonoBehaviour, IGame
{
    [SerializeField] private Timer _timer;
    [SerializeField] private GameFinishedCanvas _gameFinishedCanvas;
    [SerializeField] private TMP_Text _destroyedCactiIndicator;
    [SerializeField] private int _destroyedCactusGoal = 20;
    [SerializeField] private Cactus _cactiPrefab;
    [SerializeField] private int _maxCacti = 5;
    [SerializeField] private float _spawnInterval = 2.0f;
    [SerializeField] private Transform[] _cactiSpawnPoints;
    private List<Cactus> _cactiList = new List<Cactus>();
    private int _destroyedCactus = 0;
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _gameFinishedCanvas.SetOwner(this);
        _destroyedCactiIndicator.text = _destroyedCactus.ToString();
        _gameFinishedCanvas.gameObject.SetActive(false);
        SpawnCactus();
        GameLoop();
    }
    public void GameLoop()
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        while (!IsSpawnPointValid(spawnPoint))
        {
            spawnPoint = GetRandomSpawnPoint();
        }

        Cactus cactus = GetDisabledCactus();
        if (cactus != null)
        {
            cactus.transform.position = spawnPoint.position;
            cactus.gameObject.SetActive(true);
        }
    }

    public void GameFinished()
    {
        _timer.Stop();
        _gameFinishedCanvas.gameObject.SetActive(true);
        _gameFinishedCanvas.SetMessageText($"Game Over \n You have destroyed 20 cactus on {_timer.timeCounter}");
    }

    public void Restart()
    {
        _gameFinishedCanvas.gameObject.SetActive(false);
        _timer.Reset();
        UpdateDestroyedCactus(true);
        DisableAllCactus();
        StartGame();
    }

    private void DisableAllCactus()
    {
        _cactiList.ForEach(x => x.gameObject.SetActive(false));
    }

    public void OnGameplayObjectDestroyed(GameplayObject gameplayObject)
    {
        UpdateDestroyedCactus();
    }


    public void OnGamePlayObjectEnabled(GameplayObject gameplayObject)
    {
        //
    }

    public void OnGameFinished()
    {
        throw new System.NotImplementedException();
    }
    void UpdateDestroyedCactus(bool reset = false)
    {
        _destroyedCactus = reset ? 0 : _destroyedCactus++;
        _destroyedCactiIndicator.text = _destroyedCactus.ToString();
    }
    private void SpawnCactus()
    {
        // Spawn 5 disabled cacti on awake
        for (int i = 0; i < 5; i++)
        {
            _cactiPrefab.gameObject.SetActive(false);
            Cactus cactus = Instantiate(_cactiPrefab);
            cactus.SetOwner(this);
            _cactiList.Add(cactus);
        }
    }
    private IEnumerator GameLoopCoroutine()
    {
        if (_destroyedCactus == _destroyedCactusGoal)
        {
            GameFinished();
            yield break;
        }
        int enabledCacti = _cactiList.Where(x => x.gameObject.activeInHierarchy).Count();
        if (enabledCacti == _maxCacti)
        {
            yield break;
        }
        GameLoop();
    }

    Transform GetRandomSpawnPoint()
    {
        // Select a random spawn point from the list
        int index = Random.Range(0, _cactiSpawnPoints.Length);
        Transform spawnPoint = _cactiSpawnPoints[index];
        return spawnPoint;
    }

    bool IsSpawnPointValid(Transform spawnPoint)
    {
        // Check if the selected spawn point is too close to another enabled cactus
        foreach (Cactus cactus in _cactiList)
        {
            if (cactus.gameObject.activeSelf)
            {
                float distance = Vector3.Distance(cactus.transform.position, spawnPoint.position);
                if (distance < 1.0f)
                {
                    return false;
                }
            }
        }

        // Check if the selected spawn point is too close to a tank object
        Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 1.0f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Tank") || collider.CompareTag("Cactus"))
            {
                return false;
            }
        }

        return true;
    }


    Cactus GetDisabledCactus()
    {
        // Loop through the list of cacti and return the first disabled cactus found
        foreach (Cactus cactus in _cactiList)
        {
            if (!cactus.gameObject.activeSelf)
            {
                return cactus;
            }
        }

        // If no disabled cacti were found, instantiate a new one from the prefab
        Cactus newCactus = Instantiate(_cactiPrefab, transform.position, Quaternion.identity);
        newCactus.gameObject.SetActive(false);
        _cactiList.Add(newCactus);

        return newCactus;
    }
}



