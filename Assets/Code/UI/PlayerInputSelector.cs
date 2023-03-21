using TMPro;
using UnityEngine;

namespace Tanks.UI
{
    public class PlayerInputSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Dropdown _selectionDropdown;

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
        }

        public void SetOwner(int playerIndex)
        {
            _owner = playerIndex;
            _label.SetText(_owner.ToString());
        }
    }
}