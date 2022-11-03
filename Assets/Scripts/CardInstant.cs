using System.Collections.Generic;
using System.Threading;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInstant : MonoBehaviour
{
    [SerializeField] private Transform _cardPrefab;
    [SerializeField] private SetImageInScroll _setImageInScroll;
    [SerializeField] private ScrollWindowController _scrollWindowController;
    [SerializeField] private Image _detailsImage;
    [SerializeField] private TextMeshProUGUI _detailsText;

    private List<GameObject> _cardGameObjectsList = new List<GameObject>();
    private Transform _contentCardsTransform;

    private void Start()
    {
        _contentCardsTransform = transform;
        
        ContentLoader.GetListFromFile();
        Thread.Sleep(100);
        InstantCard();
    }

    private void InstantCard()
    {
        for (int i = 0; i < ContentLoader.GetCardList().Count; i++)
        { 
            var card = Instantiate(_cardPrefab, _contentCardsTransform);
            card.GetComponent<CardClass>()._CardModel = ContentLoader.GetCardList()[i];
            card.name = card.GetComponent<CardClass>()._CardModel.Name;
            card.GetComponent<Button>().onClick.AddListener(delegate
            {
                _scrollWindowController.MoveToNextStep(1);
                _detailsImage.sprite = card.GetComponent<CardClass>()._CardModel.Image;
                _detailsText.text = card.GetComponent<CardClass>()._CardModel.Name;
            });

            _cardGameObjectsList.Add(card.gameObject);            
        }
        
        _setImageInScroll.Init(_cardGameObjectsList);
    }
}
