using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.UI
{
    public class PlayerDeviceSelector : MonoBehaviour
    {
        [Header("Debug purposes, no need assignment")] [SerializeField]
        private int _owner;
        [SerializeField] private TMP_Dropdown _selectionDropdown;
      
        public int selectedDevice => _selectionDropdown.value;
        
        private void OnEnable()
        {
            _selectionDropdown.value = 0;
            _selectionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            List<string> dropOptions = new List<string>();
            foreach (Gamepad gamepad in Gamepad.all)
            {
                dropOptions.Add(gamepad.name);
            }
            _selectionDropdown.AddOptions(dropOptions);
            _selectionDropdown.value= _owner;
        }
        
        private void OnDropdownValueChanged(int selectedInputMode)
        {
            GameManager.Instance.SetSelectedGamepadToPlayer(_owner,selectedInputMode);
            
        }

        public void SetOwner(int playerIndex)
        {
            _owner = playerIndex;
        }
    }
}