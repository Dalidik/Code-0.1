using System;
using Data.JSON;
using Data.ScriptableObjects;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
   public class BuyPlayerIcon : MonoBehaviour
   {
      
       public PlayerIconController PlayerIconController;
       public PlayerIcon Icon;
       
      [Header("Components")]
      [SerializeField] private UIButton _iconButton;
      [SerializeField] private UIButton _button;
      [SerializeField] private Image _okImage;
      
      [Header("Button sprites")]
      [SerializeField] private Image _valueTypeSprite;
      [SerializeField] private Sprite _cantBuyIcon;
      [SerializeField] private Sprite _canBuyIcon;
      [SerializeField] private LockitSetter _text;
      
      private bool _canBuy = false;
      private Image _image;
      
      private void Start()
      {
         _image = GetComponentInChildren<Image>();

         if (Icon != null) _image.sprite = Icon.IconSprite;
         if (GameManager.Player.IsIconPurchased(Icon.Id)) _button.gameObject.SetActive(false);
      }

      private void Update()
      {
         IconContent();
         UpgradeCoins();

         if (Icon.Id == PlayerIconController.CurrentIcon.Id)
         {
            _okImage.gameObject.SetActive(true);
         }
         else
         {
            _okImage.gameObject.SetActive(false);
         }
      }

      private void OnEnable()
      {
         _iconButton.OnClick += SelectIcon;
         _button.OnClick += BuyIcon;
      }

      private void OnDisable()
      {
         _iconButton.OnClick -= SelectIcon;
         _button.OnClick -= BuyIcon;
      }

      private void UpgradeCoins()
      {
         var (levels, _) = GameManager.Map.GetUpgradesStatus();
         var amount = GameManager.Resource.Get(Icon.ResourceType);
         
         if (levels >= Icon.Stars && amount >= Icon.Cost)
         {
            _button.gameObject.GetComponent<Image>().sprite = _canBuyIcon;
         }
         else
         {
            _button.gameObject.GetComponent<Image>().sprite = _cantBuyIcon;
         }
      }

      private void BuyIcon()
      {
         var amount = GameManager.Resource.Get(Icon.ResourceType);
         var iconCost = Icon.Cost;

         if (amount >= Icon.Cost && _canBuy == true) 
         {
            GameManager.Resource.Add(Icon.ResourceType, -iconCost);
            GameManager.Player.PurchaseIcon(Icon.Id);
            PlayerIconController.SelectIcon(Icon.Id);
            _button.gameObject.SetActive(false);
         }
         
      }

      private void SelectIcon()
      {
         if (_button.gameObject.activeInHierarchy == false)
         {
            PlayerIconController.SelectIcon(Icon.Id);
         }
      }

      public void IconContent()
      {
         var (levels, _) = GameManager.Map.GetUpgradesStatus();
         
         if (levels < Icon.Stars)
         {
            _text.OverrideText($"{levels}/ {Icon.Stars}");
            _valueTypeSprite.sprite = GameManager.Resource.GetSprite(GameResource.UnlockCurrency);
            _canBuy = false;
         }

         if (levels >= Icon.Stars)
         {
            _text.OverrideText($"{Icon.Cost}");
            _valueTypeSprite.sprite = GameManager.Resource.GetSprite(GameResource.UpgradeCurrency);
            _canBuy = true;
         }

         if (Icon.Cost == 0)
         {
           // GameManager.Player.PurchaseIcon(Icon.Id);
            _button.gameObject.SetActive(false);
            _canBuy = true;
            
         }
      }

      
   }
}
