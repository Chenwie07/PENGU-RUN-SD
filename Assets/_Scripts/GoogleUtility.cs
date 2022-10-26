using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class GoogleUtility : MonoBehaviour
{
    public GameObject connectedUI; 
    public GameObject disconnectedUI; 
    
    public static GoogleUtility instance;

    private void Awake()
    {
        instance = this;
        // GOOGLE PLAY SERVICES
        PlayGamesPlatform.Activate();
        OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated); 
    }
    public void OnConnectClick()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            OnConnectionResponse(success); 
        }); 
    }
    private void OnConnectionResponse(bool authenticated)
    {
        if (authenticated)
        {
            GameManager.Instance.UnlockAchievement(GPGSPenguRunSDIds.achievement_log_in); 
            connectedUI.SetActive(true);
            disconnectedUI.SetActive(false);
            // open the save for this user. 
            OpenSave(false); // false for our function means we are loading not saving. 
        }else
        {
            connectedUI.SetActive(false);
            disconnectedUI.SetActive(true); 
        }
    }

    private bool isSaving = false; 
    public void OpenSave(bool saving)
    {
        Debug.Log("Open Save"); 
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            // call to open up the save
            ((PlayGamesPlatform)Social.Active).
                SavedGame.
                OpenWithAutomaticConflictResolution(
                "RunningPengu", // the name of the save we create
                GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                SaveGameOpened
                ); 
        }
    }

    // Reference the tutorial Running Pengu lesson 24 to relearn this if forgotten. 
    private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log("Save Game Opened"); 
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving) // Writing/Saving
            {
                // we save a byte array of data. 
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(
                    GameManager.Instance.GetSaveString());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("" +
                    "Saved at " + DateTime.Now.ToString()).Build();

                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(
                    meta, update, data, SaveUpdate);
            }
            else // Reading/Loading
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta,
                    SaveRead); 
            }
        }
    }

    // Load Save
    private void SaveRead(SavedGameRequestStatus status, byte[] data)
    {
        // read our byte array of data we saved. 
        if (status == SavedGameRequestStatus.Success)
        {
            string saveData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            GameManager.Instance.LoadSaveString(saveData); 
            Debug.Log(saveData); 
        }
    }

    // Success Save
    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log(status); 
    }
}
