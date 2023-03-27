using UnityEngine;


namespace Tanks.Controllers.Tank.Common
{
    public class TankVisualsController : MonoBehaviour
    {
        [Header("Tank Visuals")]
        [SerializeField] GameObject _tankModel;
        [SerializeField] GameObject _destroyedTankModel;

        [Header("Idle Tank Renderers")]
        [SerializeField] private Material _alternativeMaterial;
        [SerializeField] private MeshRenderer[] _renderers;

        private void Start()
        {
            _destroyedTankModel.SetActive(false);
        }

        internal void SetNormalVisualsOn(bool enable)
        {
            _tankModel.SetActive(enable);
            _destroyedTankModel.SetActive(!enable);
        }

        internal void SetAlternativeColor()
        {
            foreach (MeshRenderer tankMeshRenderer in _renderers)
            {
                tankMeshRenderer.material = _alternativeMaterial;
            }
        }
    }
}