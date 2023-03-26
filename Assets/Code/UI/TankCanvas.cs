using Tanks.UI;
using TMPro;
using UnityEngine;

namespace Tanks.Controllers.Tank
{
    public class TankCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasFader _canvasFader;
        [SerializeField]
        private TMP_Text _lifeIndicator;
        [SerializeField]
        private TMP_Text _ammoIndicator;
        [SerializeField]
        private TMP_Text _destroyedTankIndicator;
        [SerializeField]
        private TMP_Text _finishMessage;

        public void MultiplayerShowCanvasFinished(string text)
        {
            _finishMessage.text = text;
            _finishMessage.transform.parent.gameObject.SetActive(true);
        }

        public void SetLives(int lives) {
            _lifeIndicator.text = lives.ToString();
        }
        public void SetAmmo(int ammo) {
            _ammoIndicator.text = ammo.ToString();
        }
        public void SetDestryoedTanks(int destroyedTanks) {
            _destroyedTankIndicator.text = destroyedTanks.ToString();
        }
        public void Init(int lives, int ammunition, int destroyedTanks)
        {
            _canvasFader.FadeIn();
            _lifeIndicator.text = lives.ToString();
            _ammoIndicator.text = ammunition.ToString();
            _destroyedTankIndicator.text = destroyedTanks.ToString();
        }
    }
}
