using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
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
        private TMP_Text _pointsIndicator;
        [SerializeField]
        private TMP_Text _pointsToReachIndicator;
        [SerializeField]
        private TMP_Text _finishMessage;
        [SerializeField]
        Image _pointsIndicatorImage;

        private void Start()
        {
            _canvasFader.FadeIn();
        }
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
        public void SetPoints(int playerPoints) {
            _pointsIndicator.text = playerPoints.ToString();
        }
        public void Init(int lives, int ammunition, int points,int pointsToReach, Sprite pointsImage)
        {
            _lifeIndicator.text = lives.ToString();
            _ammoIndicator.text = ammunition.ToString();
            _pointsIndicator.text = points.ToString();
            _pointsToReachIndicator.text = pointsToReach.ToString();
            _pointsIndicatorImage.sprite = pointsImage;
        }
    }
}