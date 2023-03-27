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
            if(_owner >0 )
                RemoveKeyboardOption();
        }

        private void RemoveKeyboardOption()
        {
            _selectionDropdown.options.RemoveAt(1);
          
            _selectionDropdown.interactable = false;
            _selectionDropdown.options.RemoveAt(0);
            _deviceSelector.gameObject.SetActive(true);
            
            GameManager.Instance.SetSelectedInputToPlayer(_owner, 2);
        }
    }
}