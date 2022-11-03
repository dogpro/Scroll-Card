using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    public class CardClass : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public CardModel _CardModel;
        
        public void SetImage(Sprite setImage)
        {
            _image.sprite = setImage;
        }
    }
}