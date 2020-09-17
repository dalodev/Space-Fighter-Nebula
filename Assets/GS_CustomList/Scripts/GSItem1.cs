using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GameSlyce
{
    public class GSItem1 : GSItem
    {
        public Image picImage;

        public void SetData(bool isOn, string nameString)
        {
            SetValues(isOn, nameString);
        }
    }
}