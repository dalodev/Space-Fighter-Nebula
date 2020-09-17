using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GameSlyce
{
    public class GSItem4 : GSItem
    {
        public Text statusText;
        public InputField inputField;
      
        public void SetData(bool isOn, string nameString)
        {
            SetValues(isOn, nameString);

        }

        public void onEditComplete()
        {
            if (Random.Range(1, 3) == 1)
                statusText.text = "Available";
            else
            {
                statusText.text = "Not Avaiable";
            }
        }
    }
}