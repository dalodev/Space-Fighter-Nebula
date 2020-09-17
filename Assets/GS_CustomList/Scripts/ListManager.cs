using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameSlyce
{
    public class ListManager : MonoBehaviour
    {

        public AudioSource btnAudio;
        GSItem[] gsItemList = new GSItem[0];
        List<GSItem> gsList = new List<GSItem>();
        // List containers that list Items - (Dynamically Increasing ListView <Custom>)
        public GameObject GS_ListContainer;
        //Prefabs that holds items that will be places in the containers.
        
        public GSItem gsItemPrefab;
        public Text loadingText;

        HighScoreDreamlo highscoreManager;


        void Start()
        {
            if(PlayerPrefs.GetInt("MuteSound") == 1)
            {
                btnAudio.enabled = false;
            }
            else
            {
                btnAudio.enabled = true;
            }

            highscoreManager = GameObject.FindObjectOfType(typeof(HighScoreDreamlo)) as HighScoreDreamlo;
            gsList.Clear();
            SetScrollBarVisibility(false);
            StartCoroutine(AddItem());
            StartCoroutine("RefreshHighScores");

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }

        //Click Handler of Select All Button
        /*public void TglSelectAllClickHandler()
        {
            switch (tglStateSlctAll)
            {
                case ToggleState.Partial:
                case ToggleState.Unchecked:
                    foreach (var item in gsList)
                    {
                        item.tglBtn.isOn = true;
                    }
                    tglStateSlctAll = ToggleState.Checked;
                    ChangeToggleState(ToggleState.Checked);
                    break;
                case ToggleState.Checked:
                    foreach (var item in gsList)
                    {
                        item.tglBtn.isOn = false;
                    }
                    ChangeToggleState(ToggleState.Unchecked);
                    break;
            }
        }
        //Method to change Toggle State On the Fly
        public void ChangeToggleState(ToggleState state)
        {
            switch (state)
            {
                case ToggleState.Unchecked:
                    tglStateSlctAll = state;
                    btnSlctAll.GetComponent<Image>().sprite = stateSprites[0];
                    break;
                case ToggleState.Partial:
                    bool flagOn = false, flagOff = false;
                    foreach (var item in gsList)
                    {
                        if (item.tglBtn.isOn)
                        {
                            flagOn = true;
                        }
                        else
                        {
                            flagOff = true;
                        }
                    }
                    if (flagOn && flagOff)
                    {
                        tglStateSlctAll = state;
                        btnSlctAll.GetComponent<Image>().sprite = stateSprites[1];
                        //Debug.Log("Partial");
                    }
                    else if (flagOn && !flagOff)
                    {
                        ChangeToggleState(ToggleState.Checked);
                        //Debug.Log("Checked");
                    }
                    else if (!flagOn && flagOff)
                    {
                        ChangeToggleState(ToggleState.Unchecked);
                        //Debug.Log("Unchecked");
                    }
                    break;
                case ToggleState.Checked:
                    tglStateSlctAll = state;
                    btnSlctAll.GetComponent<Image>().sprite = stateSprites[2];
                    break;
            }
        }*/

        //Method to add item to the custom invitable dynamically scrollable list
        public IEnumerator AddItem()
        {
            yield return new WaitForSeconds(2.0f);
            Highscore[] highscores = highscoreManager.highScoresList;
            yield return highscores;
            if(highscores != null)
            {
                for (int i = 0; i < highscores.Length; i++)
                {
                    GSItem3 tempItem = Instantiate(gsItemPrefab) as GSItem3;
                    tempItem.numberText.text = "" + (i+1);
                    tempItem.nameTxt.text = highscores[i].username;
                    tempItem.quantityText.text = highscores[i].score.ToString();
                    tempItem.transform.SetParent(GS_ListContainer.transform, false);
                    gsList.Add(tempItem);
                    Debug.Log("TempItem" + gsList[i].nameTxt.text);
                }
                loadingText.enabled = false;
            }
            
            
            //tempItem.nameTxt.text = highscore[0].username;
            //gsList.Add(tempItem);

            //if (gsList.Count > 3)
            //{
            //    SetScrollBarVisibility(true);
            //}
        }
        IEnumerator RefreshHighscores()
        {
            while (true)
            {
                highscoreManager.DownloadHighScores();
                //HighScoreDreamlo.DownloadHighScores();
                yield return new WaitForSeconds(30);
            }
        }

        private void SetScrollBarVisibility(bool status)
        {
            //scrollbarV.gameObject.SetActive(status);
        }

        public void OnScrollRectValueChanged()
        {
            //scrollbarV.gameObject.SetActive(true);
            Invoke("HideScrollbar", 2);
        }


        void HideScrollbar()
        {
            SetScrollBarVisibility(false);
        }
        //Click Handler for Back Button
        public void BackMain()
        {
            btnAudio.Play();
            SceneManager.LoadScene("Menu");
        }
    }
}