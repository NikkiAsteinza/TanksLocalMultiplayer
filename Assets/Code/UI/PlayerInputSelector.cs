using TMPro;
using UnityEngine;

namespace Tanks.UI
{
    public class PlayerInputSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Dropdown _selectionDropdown;
        [SerializeField] private PlayerDeviceSelector _deviceSelector;

        [Header("Debug purposes, no need assignment")] [SerializeField]
        private int _owner;
        
        public int selectedMode => _selectionDropdown.value;

        private void Awake()
        {
            _selectionDropdown.value = 0;
            _selectionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int selectedInputMode)
        {
            GameManager.Instance.SetSelectedInputToPlayer(_owner,selectedInputMode);
            ShowDeviceSelectorIfGamepad(selectedInputMode);
        }

        private void ShowDeviceSelectorIfGamepad(int selectedInputMode)
        {
            if (selectedInputMode > 0)
            {
                _deviceSelector.gameObject.SetActive(true);
            }
            else
            {
                _deviceSelector.gameObject.SetActive(false);
            }
        }

        public void SetOwner(int playerIndex)
        {
            _owner = playerIndex;
            _label.text = _owner.ToString();
            _deviceSelector.SetOwner(_owner);
            if (_owner > 0)
            {
                RemoveKeyboardOption();
            }
        }

        public void RemoveKeyboardOption()
        {
            _selectionDropdown.options.RemoveAt(0);
            GameManager.Instance.SetSelectedInputToPlayer(_owner,1);
            _deviceSelector.gameObject.SetActive(true);
        }
    }
}