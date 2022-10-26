using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsScript : MonoBehaviour
{
    public static UnityAdsScript instance;

    private void Awake()
    {
        instance = this;
        Advertisement.Initialize("4990094"); // after initializing based on our google ID
        // we can then show our ads however we wish, it's our logic from here. 
    }

    [Obsolete]
    public void RequestRevive()
    {
        ShowOptions showOptions = new ShowOptions();
        showOptions.resultCallback = Revive;
        Advertisement.Show("Rewarded_Android", showOptions);
    }

    //private void HandleShowResult(ShowResult obj)
    //{
    //    switch (obj)
    //    {
    //        case ShowResult.Failed:
    //            break;
    //        case ShowResult.Skipped:
    //            break;
    //        case ShowResult.Finished:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void Revive(ShowResult sr)
    {
        if (sr == ShowResult.Finished)
        {
            FindObjectOfType<PlayerMotor>().Revive();
            GameManager.Instance.IsDead = false;

            foreach (GlacierSpawner glacierSpawner in FindObjectsOfType<GlacierSpawner>())
            {
                glacierSpawner.IsScrolling = true;
            }
            GameManager.Instance.deadAnimPanel.SetActive(false);
            GameManager.Instance.gameMenu.SetTrigger("Show");
        }
        else
        {
            GameManager.Instance.OnPlayButton(); 
        }
    }
}
