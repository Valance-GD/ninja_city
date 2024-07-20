using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CutTree : MonoBehaviour
{
    [SerializeField] private GameObject baseTree, baseCut, upCut;
    [SerializeField] private float deleatingTime;
    [SerializeField] private int _woodAmount;
    private PlayerResurses player;
    private void Start()
    {
        player = FindObjectOfType<PlayerResurses>();
    }
    public void Cut()
    {
        baseTree.SetActive(false);
        baseCut.SetActive(true);
        upCut.SetActive(true);
        StartCoroutine(DeletingProcces());
    }
    private IEnumerator DeletingProcces()
    {
        yield return new WaitForSeconds(deleatingTime);
        player.AddResource("Wood", _woodAmount);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
