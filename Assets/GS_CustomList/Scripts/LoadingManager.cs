using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameSlyce
{
    public class LoadingManager : MonoBehaviour
    {
        public void LoadLevel(int id)
        {
            SceneManager.LoadScene("Listview"+ id);
        }
    }
}