using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class NinjasManager : MonoBehaviour
{
    public static NinjasManager Instance { get; private set; }
    private List<Ninjas> ninjaTypes = new List<Ninjas>();
    public List<Ninjas> NinjaType => ninjaTypes;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("oneMoreNinjasManager");
            //Destroy(gameObject);
        }
    }
    public void SaveNinjasHouse()
    {
        foreach (Ninjas ninjaType in ninjaTypes)
        {
            Ninjas house = GameController.Instance.gameData.ninjas.Find(nin => nin.ninjaType == ninjaType.ninjaType);
            if (house != null)
            {
                house.alliveNinja = ninjaType.alliveNinja;
            }
            else
            {
                Ninjas thisHouse = new Ninjas();
                thisHouse.ninjaType = ninjaType.ninjaType;
                thisHouse.alliveNinja = ninjaType.alliveNinja;
                GameController.Instance.gameData.ninjas.Add(thisHouse);
            }
        }
        ninjaTypes.Clear();
    }
    public void LoadNinjasHouse(List<Ninjas> saveDataNinjas)
    {
        if (saveDataNinjas == null)
        {
            return;
        }
        ninjaTypes.Clear();
        List<NinjaHouse> ninjaHouse = FindObjectsOfType<NinjaHouse>().ToList();
        foreach (Ninjas ninjaType in saveDataNinjas)
        {
            Ninjas newNinjaHouse = new Ninjas();
            newNinjaHouse.ninjaType = ninjaType.ninjaType;
            newNinjaHouse.alliveNinja = ninjaType.alliveNinja;
            ninjaTypes.Add(newNinjaHouse);
            foreach (NinjaHouse house in ninjaHouse) 
            { 
                if(house.HouseNinjaType == newNinjaHouse.ninjaType)
                {
                    house._alliveAICount = newNinjaHouse.alliveNinja;
                    break;
                }
            }
        }

    }




}
