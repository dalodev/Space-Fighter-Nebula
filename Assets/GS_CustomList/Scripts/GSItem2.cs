using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GameSlyce
{
    public class GSItem2 : GSItem
    {
        public Button btnSync;
        // Use this for initialization
        protected new void Start()
        {
            base.Start();
            _anim = GetComponent<Animator>();
        }

        public void SetData(bool isOn, string nameString)
        {
            SetValues(isOn, nameString);

        }
        Animator _anim;
        bool synched;
        public void OnClickSynced()
        {
            if (!synched)
            {
                synched = true;
                _anim.SetTrigger("play");
            }
        }

        public void SyncCompleted(){
            Debug.Log("I am in Sync Compelete");
            btnSync.GetComponentInChildren<Text>().text = "Synched";
            btnSync.enabled = false;
        }
    }
}