using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Light : MonoBehaviour
{
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

    public bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        towerBlock.GetComponent<MeshRenderer>().material = unlit;
        towerBlock2.GetComponent<MeshRenderer>().material = unlit;
        towerBlock3.GetComponent<MeshRenderer>().material = unlit;
        towerBlock4.GetComponent<MeshRenderer>().material = unlit;
        towerBlock5.GetComponent<MeshRenderer>().material = unlit;
        towerBlock6.GetComponent<MeshRenderer>().material = unlit;
        towerBlock7.GetComponent<MeshRenderer>().material = unlit;
        towerBlock8.GetComponent<MeshRenderer>().material = unlit;
        towerBlock9.GetComponent<MeshRenderer>().material = unlit;
        towerBlock10.GetComponent<MeshRenderer>().material = unlit;
        towerBlock11.GetComponent<MeshRenderer>().material = unlit;
        towerBlock12.GetComponent<MeshRenderer>().material = unlit;
    }

    // Update is called once per frame
    void Update()
    {
        if((Vector3.Distance(player.transform.position,towerBlock.transform.position) <= 2) && !isStarted)
        {
            isStarted = true;
            StartCoroutine(lightUp());
            //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 8f, 0.1f);

        }
    }

    IEnumerator lightUp()
    {
        towerBlock.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock.GetComponent<MeshRenderer>().material = unlit;
        towerBlock2.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock2.GetComponent<MeshRenderer>().material = unlit;
        towerBlock3.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock3.GetComponent<MeshRenderer>().material = unlit;
        towerBlock4.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock4.GetComponent<MeshRenderer>().material = unlit;
        towerBlock5.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock5.GetComponent<MeshRenderer>().material = unlit;
        towerBlock6.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock6.GetComponent<MeshRenderer>().material = unlit;
        towerBlock7.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock7.GetComponent<MeshRenderer>().material = unlit;
        towerBlock8.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock8.GetComponent<MeshRenderer>().material = unlit;
        towerBlock9.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock9.GetComponent<MeshRenderer>().material = unlit;
        towerBlock10.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock11.GetComponent<MeshRenderer>().material = litUp;
        yield return new WaitForSeconds(0.03f);
        towerBlock12.GetComponent<MeshRenderer>().material = litUp;
    }

}
