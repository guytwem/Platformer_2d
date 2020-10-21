using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeybindManager : MonoBehaviour
{
    #region Variables
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
   
   
    public GameObject currentKey;
    public Color32 changed = new Color32(32, 171, 249, 255);
    public Color32 selected = new Color32(239, 116, 36, 255);
    // Start is called before the first frame update
    #endregion
    #region Struct
    [Serializable]
    public struct KeyUISetup
    {
        public string keyName;
        public Text keyDisplayText;
        public string defaultKey;
    }
    
    public KeyUISetup[] baseSetup;
    #endregion
    #region Start
    void Start()
    {
        //find the key name for each key
      for(int i = 0; i < baseSetup.Length; i++)
        {
            keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode),
                PlayerPrefs.GetString(baseSetup[i].keyName, baseSetup[i].defaultKey)));

            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();
        }
        
   

    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        Event thisEvent = Event.current;
        if( currentKey!= null)
        {
            string newKey = "";
            if (thisEvent.isKey)
            {
                newKey = thisEvent.keyCode.ToString();
            }
            //Allow left/right shift
            if(Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "")
            {
                keys[currentKey.name] = (KeyCode)Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changed;
                currentKey = null;
            }
        }
    }
    #endregion
    #region Change Key
    public void ChangeKey(GameObject clickedKey)
    {
        //switch key colours depending on if changed
        currentKey = clickedKey;
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = selected;

        }
        
        
    }
    #endregion
    #region Save Keys
    public void SaveKeys()
    {
        //save the keys as new keybind
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            
        }
        PlayerPrefs.Save();
        
    }
    #endregion
}

