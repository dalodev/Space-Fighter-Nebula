using UnityEngine;
using UnityEngine.UI;
namespace GameSlyce
{
    public class GSItem : MonoBehaviour
    {
        public Text nameTxt;
        public Toggle tglBtn;

        

        protected void Start()
        {
            //tglBtn.GetComponent<Toggle>().onValueChanged.AddListener(ToggleClicked);
        }

        protected void SetValues(bool isChecked, string nameString)
        {
            nameTxt.text = nameString;
            tglBtn.isOn = isChecked;

        }
        /*protected void ToggleClicked(bool state)
        {
            //Debug.Log("Toggle Clicked");
            GetComponentInParent<ListManager>().ChangeToggleState(ListManager.ToggleState.Partial);
        }*/
    }
}