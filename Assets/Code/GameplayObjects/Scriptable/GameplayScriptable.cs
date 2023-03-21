using System.Collections;
using System.Collections.Generic;
using Tanks.GameplayObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Gameplay", menuName = "ScriptableObjects/GameplayScriptable", order = 1)]
public class GameplayScriptable : ScriptableObject
{
    [SerializeField] private ObjectTypes _objectType;
    [SerializeField] private AudioClip _soundEffect;
    [SerializeField] private GameObject _effectObject;
    
    public ObjectTypes Type => _objectType;
    public AudioClip Sound => _soundEffect;

}
