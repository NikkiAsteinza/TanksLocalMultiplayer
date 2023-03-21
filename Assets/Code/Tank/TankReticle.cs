using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Tanks.Tanks
{
    public class TankReticle : MonoBehaviour
    {
        [Header("Reticle Properties")]
        private Vector3 _reticleNormal;
        private Camera _camera;
        private TankTurret _turret;

        internal void Init(Camera camera, TankTurret turret)
        {
            _camera = camera;
            _turret = turret;
        }

        private void Update()
        {
            HandleReticle();
        }

        private void HandleReticle()
        {

            Ray screenRay = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {

                transform.position = hit.point;
                _reticleNormal = hit.normal;
            }

            _turret.HandleTurret(transform.position);
        }
    }
}