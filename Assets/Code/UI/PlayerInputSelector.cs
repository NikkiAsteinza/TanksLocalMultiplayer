using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.UI
{
    public class PlayerInputSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Dropdown _selectionDropdown;
        [SerializeField] private PlayerDeviceSelector _deviceSelector;

        [Header("Debug purposes, no need assignment")] [SerializeField]
        private int _owner;

        private void Awake()
        {
            _selectionDropdown.value = 0;
            _selectionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int selectedInputMode)
        {
            GameManager.Instance.SetSelectedInputToPlayer(_owner,selectedInputMode);
            ShowDeviceSelectorIfGamepad((InputMode)selectedInputMode);
        }

        private void ShowDeviceSelectorIfGamepad(InputMode selectedInputMode)
        {
            if (selectedInputMode == InputMode.Gamepad)
            {
                _deviceSelector.gameObject.SetActive(true);
            }
        }

        public void SetOwner(int playerIndex)
        {
            _owner = playerIndex;
            _label.text = _owner.ToString();
            _deviceSelector.SetOwner(_owner);

            SelectDefaultOption();
        }

        private void SelectDefaultOption()
        {
            if (_owner == 0 && GameManager.Instance.gameMode == GameMode.Multiplayer)
            {
                if (Gamepad.all.Count == 1)
                {
                    _selectionDropdown.options.RemoveAt(0);
                    GameManager.Instance.SetSelectedInputToPlayer(_owner, (int)InputMode.Keyboard);
                    _selectionDropdown.interactable = false;
                }
            }

            if (_owner > 0)
            {
                RemoveKeyboardOption();
            }
        }

        public void RemoveKeyboardOption()
        {
            _selectionDropdown.options.RemoveAt(1);
            GameManager.Instance.SetSelectedInputToPlayer(_owner, 2);
            _selectionDropdown.interactable = false;
            _selectionDropdown.options.RemoveAt(0);
            _deviceSelector.gameObject.SetActive(true);
        }
    }
}