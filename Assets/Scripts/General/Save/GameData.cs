using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
   public List<Building> buildings;
    public List<Resource> resources;
    public List<Ninjas> ninjas;

    public bool isMusicOn;
    public int currentLevel;
    public int currentMap ;
    public GameData()
    {
        buildings = new List<Building>();
        resources = new List<Resource>();
    }
}

[Serializable]
public class Building
{
    public string buildingName;
    public bool isBuild;
    public int level;
}

[Serializable]
public class Resource
{
    public string resourceName;
    public int quantity;
}
[Serializable]
public class Ninjas
{
    public int alliveNinja;
    public string ninjaType;
}
public class GameDataManager
{
    public static void ResetGameData(GameData originalData)
    {
        GameData newData = new GameData
        { 
            resources = originalData.resources,
            currentMap = originalData.currentMap
        };

        newData.buildings = new List<Building>(); 
        newData.ninjas = new List<Ninjas>();
        newData.isMusicOn = false; 
        newData.currentLevel = 0;
        GameController.Instance.gameData = newData;
        GameController.Instance.SaveNewData();
        
    }
}