using System;
using Data.JSON;
using Managers;
using UI.Components;
using UnityEngine;

namespace Map.UI
{
    public class IconsShopMapButton : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UIButton _openIconsShopButton;

        private TopButtonsPanel _topButtonsPanel;

        private void Start()
        {
            _topButtonsPanel = GetComponentInParent<TopButtonsPanel>();
        }

        private void OnEnable()
        {
            _openIconsShopButton.OnClick += GoToIconsShop;
            _openIconsShopButton.OnClick += HideTopPanel;
        }

        private void OnDisable()
        {
            _openIconsShopButton.OnClick -= GoToIconsShop;
            _openIconsShopButton.OnClick -= HideTopPanel;
        }
       
        private void GoToIconsShop()
        {
            GameManager.UIController.ShowIconsShopWindow();
        }

        private void HideTopPanel()
        {
            _topButtonsPanel.SetTopPanelVisible(false);
        }
    }
}
