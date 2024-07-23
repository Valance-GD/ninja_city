using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
   public List<Building> buildings;
    public List<Resource> resources;

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
    public int quantity;
    //public int level;
}

[Serializable]
public class Resource
{
    public string resourceName;
    public int quantity;
}