using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelManager levelManager;
    public string pathToFile;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.self;
        pathToFile = "../autosave.save";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        XmlDocument x = new XmlDocument();
        XmlElement root = x.CreateElement("AutoSaveData");
        root.SetAttribute("FileName", "autosave");
        XmlElement lvm = x.CreateElement("Levels");
        lvm.InnerText = levelManager.enabledLevel1.ToString() + "," + levelManager.enabledLevel2.ToString() + "," + levelManager.enabledLevel3.ToString() + "," + levelManager.enabledLevel4.ToString() + "," + levelManager.enabledLevel5.ToString();
        root.AppendChild(lvm);
        x.AppendChild(root);
        x.Save(pathToFile);
        print("Successful Auto-Save!");
    }

    public void Load()
    {
        if (pathToFile == "" || string.IsNullOrEmpty(pathToFile)) pathToFile = "../autosave.save";
        print(pathToFile);
        
        if (File.Exists(pathToFile))
        {
            XmlDocument x = new XmlDocument();
            x.Load(pathToFile);

            string[] splits = x.GetElementsByTagName("Levels")[0].InnerText.Split(",");
            print(splits[0]);
            if (levelManager == null) levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            print(bool.Parse(splits[0]));
            levelManager.enabledLevel1 = bool.Parse(splits[0]);
            levelManager.enabledLevel2 = bool.Parse(splits[1]);
            levelManager.enabledLevel3 = bool.Parse(splits[2]);
            levelManager.enabledLevel4 = bool.Parse(splits[3]);
            levelManager.enabledLevel5 = bool.Parse(splits[4]);
            print("Load Successful");
        }
        else
        {
            print("Non existant save");
        }
    }
}
