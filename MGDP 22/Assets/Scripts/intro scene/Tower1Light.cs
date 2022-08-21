using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Light : MonoBehaviour
{
    public bool lastScene = false;
    public GameManager2 manage;

    public GameObject player;
    public GameObject towerBlock;
    public Material unlit;
    public Material litUp;

    //12 light-up blocks total, w last 3 remaining lit up
    public GameObject towerBlock2;
    public GameObject towerBlock3;
    public GameObject towerBlock4;
    public GameObject towerBlock5;
    public GameObject towerBlock6;
    public GameObject towerBlock7;
    public GameObject towerBlock8;
    public GameObject towerBlock9;
    public GameObject towerBlock10;
    public GameObject towerBlock11;
    public GameObject towerBlock12;

    public GameObject litBlock;
    public GameObject litBlock2;
    public GameObject litBlock3;
    public GameObject litBlock4;
    public GameObject litBlock5;
    public GameObject litBlock6;
    public GameObject litBlock7;
    public GameObject litBlock8;
    public GameObject litBlock9;
    public GameObject litBlock10;
    public GameObject litBlock11;
    public GameObject litBlock12;


    public bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        towerBlock.SetActive(true);
        towerBlock2.SetActive(true);
        towerBlock3.SetActive(true);
        towerBlock4.SetActive(true);
        towerBlock5.SetActive(true);
        towerBlock6.SetActive(true);
        towerBlock7.SetActive(true);
        towerBlock8.SetActive(true);
        towerBlock9.SetActive(true);
        towerBlock10.SetActive(true);
        towerBlock11.SetActive(true);
        towerBlock12.SetActive(true);

        litBlock.SetActive(false);
        litBlock2.SetActive(false);
        litBlock3.SetActive(false);
        litBlock4.SetActive(false);
        litBlock5.SetActive(false);
        litBlock6.SetActive(false);
        litBlock7.SetActive(false);
        litBlock8.SetActive(false);
        litBlock9.SetActive(false);
        litBlock10.SetActive(false);
        litBlock11.SetActive(false);
        litBlock12.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if ((Vector3.Distance(player.transform.position, towerBlock.transform.position) <= 2) && !isStarted)
        {
            isStarted = true;
            StartCoroutine(lightUp());
        }
    }

    IEnumerator lightUp()
    {
        //towerBlock.SetActive(false);
        litBlock.SetActive(true);
        Debug.Log("hi");
        yield return new WaitForSeconds(0.03f);
        Debug.Log("hi again");
        towerBlock.SetActive(true);
        litBlock.SetActive(false);
        towerBlock2.SetActive(false);
        litBlock2.SetActive(true);
        Debug.Log("hi again again");
        yield return new WaitForSeconds(0.03f);
        towerBlock2.SetActive(true);
        litBlock2.SetActive(false);
        towerBlock3.SetActive(false);
        litBlock3.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock3.SetActive(true);
        litBlock3.SetActive(false);
        towerBlock4.SetActive(false);
        litBlock4.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock4.SetActive(true);
        litBlock4.SetActive(false);
        towerBlock5.SetActive(false);
        litBlock5.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock5.SetActive(true);
        litBlock5.SetActive(false);
        towerBlock6.SetActive(false);
        litBlock6.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock6.SetActive(true);
        litBlock6.SetActive(false);
        towerBlock7.SetActive(false);
        litBlock7.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock7.SetActive(true);
        litBlock7.SetActive(false);
        towerBlock8.SetActive(false);
        litBlock8.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock8.SetActive(true);
        litBlock8.SetActive(false);
        towerBlock9.SetActive(false);
        litBlock9.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock9.SetActive(true);
        litBlock9.SetActive(false);
        towerBlock10.SetActive(false);
        litBlock10.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock11.SetActive(false);
        litBlock11.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        towerBlock12.SetActive(false);
        litBlock12.SetActive(true);

        if(lastScene)
        {
            manage.showEnd();
        }
    }

}
