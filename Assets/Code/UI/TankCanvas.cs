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
        [SerializeField] Timer _timer;
        [SerializeField]
        private TMP_Text _lifeIndicator;
        [SerializeField]
        private TMP_Text _ammoIndicator;
        [SerializeField]
        private TMP_Text _pointsIndicator;
        [SerializeField]
        private TMP_Text _pointsToReachIndicator;
        [SerializeField]
        private TMP_Text _finishMessageTitle;
        [SerializeField]
        private TMP_Text _finishMessageContent;
        [SerializeField]
        Image _pointsIndicatorImage;

        public void ShowFinalMessageTimer(string title)
        {
            ShowFinalMessage(title, "Elapsed time: "+Math.Round(_timer.Time,2).ToString());
        }
        public void ShowFinalMessage(string title, string message = null)
        {
            _finishMessageTitle.text = title;
            if (!string.IsNullOrEmpty(message))
            {
                _finishMessageContent.text = message;
            }
            else
            {
                _finishMessageContent.gameObject.SetActive(false);
            }

            
            _finishMessageTitle.transform.parent.gameObject.SetActive(true);
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

            switch (GameManager.Instance.gameMode)
            {
                case GameMode.SinglePlayer:
                    _lifeIndicator.transform.parent.gameObject.SetActive(false);
                    break;
                case GameMode.Multiplayer:
                    _timer.gameObject.SetActive(false);
                    break;
            }
            _canvasFader.FadeIn();
        }
    }
}