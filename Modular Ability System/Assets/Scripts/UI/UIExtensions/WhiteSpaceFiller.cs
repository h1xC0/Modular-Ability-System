using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace UI.UIExtensions
{
    public class WhiteSpaceFiller : MonoBehaviour
    {
        [SerializeField] private string _leftText;
        [SerializeField] private string _rightText;
        [SerializeField] private int _charactersCount;

        [SerializeField] private TMP_Text _tmpText;

        public void FillSpace(string leftText, string rightText, TMP_Text tmpText, bool addSprite)
        {
            var sb = new StringBuilder(leftText);

            var fillCount = _charactersCount - (leftText.Length + (int)(rightText.Length));
            if (fillCount > 0)
            {
                sb.Append(string.Concat(Enumerable.Repeat('.', fillCount)));
            }

            sb.Append(rightText);
            if(addSprite)
                sb.Append("<sprite=0>");
            
            tmpText.text = sb.ToString();
        }
        
        /// <summary>
        /// For editor things or just setup it in inspector
        /// </summary>
        /// <param name="addSprite">You need your sprite to be added?</param>
        public void FillSpace(bool addSprite)
        {
            var sb = new StringBuilder(_leftText);

            var fillCount = _charactersCount - (_leftText.Length + (int)(_rightText.Length));
            if (fillCount > 0)
            {
                sb.Append(string.Concat(Enumerable.Repeat('.', fillCount)));
            }

            sb.Append(_rightText);
            if(addSprite)
                sb.Append("<sprite=0>");
            
            _tmpText.text = sb.ToString();
        }
    }
}
