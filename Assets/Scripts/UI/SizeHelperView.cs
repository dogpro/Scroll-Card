using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeHelperView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    
    [Space]
    [SerializeField] private RectTransform[] _childRectElements;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    
    [Space]
    [SerializeField] private RectTransform _imageRect;
    [SerializeField] private TextMeshProUGUI _imageText;
    
    [Space]
    [SerializeField] private RectTransform _backButtonTransform;
    [SerializeField] private RectTransform _slideButtonTransform;
    
    private RectTransform _rootRectCanvas;
    
    private void Start()
    {
        _rootRectCanvas = transform.root.GetComponent<RectTransform>();
        float widthSizeScreenAdon = _rootRectCanvas.rect.width;
        
        foreach (var childRectElement in _childRectElements)
            childRectElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthSizeScreenAdon);
        
        OnRectTransformDimensionsChange();
    }

    private void OnRectTransformDimensionsChange()
    {
        float constValue = 0f;
        
        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
            constValue = Screen.height / _canvas.scaleFactor;
        else
            constValue = Screen.width / _canvas.scaleFactor;

        _gridLayoutGroup.cellSize = new Vector2( constValue * 0.5f, constValue * 0.5f);
        
        _gridLayoutGroup.spacing = new Vector2(constValue * 0.1f, constValue * 0.1f);
        
        _gridLayoutGroup.padding.bottom = Convert.ToInt32(constValue * 0.1f);
        _gridLayoutGroup.padding.top = Convert.ToInt32(constValue * 0.1f);
        _gridLayoutGroup.padding.left = Convert.ToInt32(constValue * 0.1f);
        _gridLayoutGroup.padding.right = Convert.ToInt32(constValue * 0.1f);
        
        _imageRect.sizeDelta = new Vector2(constValue * 0.8f, constValue * 0.8f);
        _imageText.fontSize = constValue * 0.08f;

        var textTest =  Screen.height / _canvas.scaleFactor * 0.5f - constValue * 0.8f / 2;
        _imageText.transform.GetComponent<RectTransform>().offsetMax = new(0, textTest);

        _backButtonTransform.anchoredPosition = new Vector2(constValue * 0.05f,-constValue * 0.05f);
        _slideButtonTransform.anchoredPosition = new Vector2(-constValue * 0.05f,constValue * 0.05f);
    }
}
