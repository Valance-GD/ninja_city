using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
   public List<Building> buildings;
    public List<Resource> resources;
    public int alliveNinja;
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
    //public int level;
}

[Serializable]
public class Resource
{
    public string resourceName;
    public int quantity;
}
public class GameDataManager
{
    public static GameData ResetGameData(GameData originalData)
    {
        GameData newData = new GameData
        { 
            resources = originalData.resources,
            currentMap = originalData.currentMap
        };

        newData.buildings = new List<Building>(); 
        newData.alliveNinja = 0; 
        newData.isMusicOn = false; 
        newData.currentLevel = 0; 
       
        return newData;
    }
}