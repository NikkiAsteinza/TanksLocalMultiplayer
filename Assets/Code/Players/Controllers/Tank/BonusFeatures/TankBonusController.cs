using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects;
using UnityEngine;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankBonusController : MonoBehaviour
    {
        [SerializeField] private List<TankBonusFeature> _bonusFeatures;
        PlayerTank _owner;

        internal void Init(PlayerTank owner)
        {
            _owner = owner;
        }
        internal void ApplyObjectFeature(ObjectTypes type)
        {
            switch (type)
            {
                case ObjectTypes.Shield:
                    TankBonusFeature _shieldBonus = _bonusFeatures.FirstOrDefault(x => x.GetBonusType == type);
                    if (!_shieldBonus.gameObject.activeInHierarchy)
                        _shieldBonus.gameObject.SetActive(true);
                    break;
                case ObjectTypes.Ammo:
                    TankBonusFeature _ammoBonus = _bonusFeatures.FirstOrDefault(x => x.GetBonusType == type);
                    if (!_ammoBonus.gameObject.activeInHierarchy)
                        _ammoBonus.gameObject.SetActive(true);
                    break;
                case ObjectTypes.Turbo:
                    TankBonusFeature _speedBonus = _bonusFeatures.FirstOrDefault(x => x.GetBonusType == type);
                    if (!_speedBonus.gameObject.activeInHierarchy)
                        _speedBonus.gameObject.SetActive(true);
                    else
                    {
                        _speedBonus.ResetTimer();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}