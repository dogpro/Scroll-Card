using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections.Generic;
using Models;

public class SetImageInScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _mainScrollRect;
    [SerializeField] private float disctanceToCheckCallingLoad = 50;
    [SerializeField] private float distanceToCallLoadImages = 0;

    public float ScrollViewContentLastPosistion = Mathf.Infinity;

    private bool IsReadyToWrok = false;
    
    private RectTransform _mainViewRectTransform;
    private List<GameObject> _cardList;

    private void Start()
    {
        _mainScrollRect.onValueChanged.AddListener(_ => OnCardScrollViewValueChanged());
        _mainViewRectTransform = _mainScrollRect.GetComponent<RectTransform>();
        
    }

    public async void Init(List<GameObject> _listToWork)
    {
        _cardList = _listToWork;
        IsReadyToWrok = true;
        await ServersCardScrollViewValueChanged();
    }

    private async void OnCardScrollViewValueChanged()
    {
        if (IsReadyToWrok && _mainScrollRect.gameObject.activeSelf)
            await ServersCardScrollViewValueChanged();
        else
            return;
    }

    private async Task ServersCardScrollViewValueChanged()
    {
        await Task.CompletedTask;

        float _scrollLocalPosY = _mainScrollRect.content.transform.localPosition.y;
        float _mathfAbsBetweenPositions = Mathf.Abs(ScrollViewContentLastPosistion - _scrollLocalPosY);

        if ( _mathfAbsBetweenPositions >= disctanceToCheckCallingLoad)
        {

            float _toCheckRectMinY = _mainViewRectTransform.rect.yMin - distanceToCallLoadImages;
            float _toCheckRectMaxY = _mainViewRectTransform.rect.yMax + distanceToCallLoadImages;

            foreach (var someCard in _cardList)
            {
                ScrollViewContentLastPosistion = _scrollLocalPosY;

                RectTransform _cardTransformPos = someCard.GetComponent<RectTransform>();
                Vector3 _positionInWord = _cardTransformPos.parent.TransformPoint(_cardTransformPos.localPosition);
                Vector3 _positionInScroll = _mainViewRectTransform.InverseTransformPoint(_positionInWord);
                float _cardMinYPos = _positionInScroll.y + _cardTransformPos.rect.yMin;
                float _cardMaxYPos = _positionInScroll.y + _cardTransformPos.rect.yMax;

                if (_cardMaxYPos >= _toCheckRectMinY && _cardMinYPos <= _toCheckRectMaxY)
                {
                    if (someCard.gameObject.activeSelf)
                        someCard.GetComponent<CardClass>().SetImage(someCard.GetComponent<CardClass>()._CardModel.Image);
                }
            }
        }
    }
}
