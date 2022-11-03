using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrollWindowController : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRectPanels;
    
    private GraphicRaycaster _blockRayCastWindow; 
    
    public void Start()
    {
        _blockRayCastWindow = transform.root.GetComponent<GraphicRaycaster>();
    }

    public void MoveToNextStep(int stepCount)
    {
        this.DOKill();
        
        _blockRayCastWindow.enabled = false;
        
        DOTween.To(
            x => _scrollRectPanels.horizontalNormalizedPosition = x,
            _scrollRectPanels.horizontalNormalizedPosition,
            stepCount,
            0.5f).SetEase(Ease.InOutBack);
  
        _blockRayCastWindow.enabled = true;
    }
}
