using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using System.Collections;

public class UnityAds : MonoBehaviour {

    public void ShowAd()
    {
        #if UNITY_ADS
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        #endif
    }
}
