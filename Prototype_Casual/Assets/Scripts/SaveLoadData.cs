using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class SaveLoadData : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;
        private bool canSave = false;
        public void Initialize()
        {
            
            if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)  
            {
                LoadData();                                     
            }
            else                                                
            {
                SaveData();                                     
                PlayerPrefs.SetInt("GameStartFirstTime", 1);    
            }
            canSave = true;
        }

        private void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if(canSave)
            {
                SaveData();
            }
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR
            //needed only in editor
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveData();
            }
#endif
        }

        public void SaveData()
        {
            string shopDataString = JsonUtility.ToJson(shopUI.shopData);
            Debug.Log("Save:" + shopDataString);

            try
            {
                System.IO.File.WriteAllText(Application.persistentDataPath + "/ShopData.json", shopDataString);
                Debug.Log("Data Saved");

            }
            catch (System.Exception e)
            {
                Debug.Log("Error Saving Data:" + e);
                throw;
            }
        }

        private void LoadData()
        {
            try
            {
                string shopDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
                Debug.Log("Load:" + shopDataString);
                shopUI.shopData = new ShopData();
                shopUI.shopData = JsonUtility.FromJson<ShopData>(shopDataString); 

                Debug.Log("Data Loaded");
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Loading Data:" + e);
                throw;
            }

        }

        public void ClearData()
        {
            Debug.Log("Data Cleared");
            PlayerPrefs.SetInt("GameStartFirstTime", 0);
        }

    }
}
