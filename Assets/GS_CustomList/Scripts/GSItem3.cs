using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GameSlyce
{
    public class GSItem3 : GSItem
    {
        public Text quantityText;
        public Text numberText;
        public Scrollbar scrollbar;

        

        public void SetData(bool isOn, string nameString)
        {
            SetValues(isOn, nameString);
        }

        public void OnValueChanged()
        {
            quantityText.text = "" + scrollbar.value;
        }
    }
}