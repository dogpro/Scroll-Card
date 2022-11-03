using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _backButtonTransform;
    [SerializeField] private Button _slideUpButtonTransform;
    [SerializeField] private Button _slideDownButtonTransform;
    
    [Space]
    [SerializeField] private ScrollRect _cardScrollRect;
    [SerializeField] private ScrollWindowController _scrollWindowController;

    private void Start()
    {
        _backButtonTransform.onClick.AddListener(BackButton);
        _slideUpButtonTransform.onClick.AddListener(delegate { SlideControll(1); });
        _slideDownButtonTransform.onClick.AddListener(delegate { SlideControll(0); });
    }

    private void BackButton()
    {
        _scrollWindowController.MoveToNextStep(0);
    }
    
    private void SlideControll(int stepCount)
    {
        DOTween.To(
            x => _cardScrollRect.verticalNormalizedPosition = x,
            _cardScrollRect.verticalNormalizedPosition,
            stepCount,
            0.7f).SetEase(Ease.InExpo);
    }
}
