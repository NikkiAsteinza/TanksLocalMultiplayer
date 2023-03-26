using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.UI
{
    public class PlayerDeviceSelector : MonoBehaviour
    {
        [Header("Debug purposes, no need assignment")]
        [SerializeField] private int _owner;
        [SerializeField] private TMP_Dropdown _selectionDropdown;

        private void Start()
        {
            InitDropdown();
        }

        private void InitDropdown()
        {
            _selectionDropdown.value = 0;
            _selectionDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

            List<string> dropOptions = new List<string>();
            foreach (Gamepad gamepad in Gamepad.all)
            {
                dropOptions.Add(gamepad.name);
            }
            _selectionDropdown.AddOptions(dropOptions);
            _selectionDropdown.value = _owner;
        }

        private void OnDropdownValueChanged(int selectedDevice)
        {
            if(selectedDevice-1>=0)
                GameManager.Instance.SetSelectedGamepadToPlayer(_owner,selectedDevice-1);
        }

        public void SetOwner(int playerIndex)
        {
            _owner = playerIndex;
        }
    }
}