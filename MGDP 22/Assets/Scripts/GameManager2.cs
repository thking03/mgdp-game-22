using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public bool rod = false;
    public int batteries = 0;

    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;

    public GameObject ui;

    public GameObject did;
    public GameObject didnot;
    public Text txt;

    public void UpdateRod()
    {
        rod = true;
    }

    public void countBattery()
    {
        if (battery1.transform.position == new Vector3(7, 1, 8))
        {
            batteries++;
        }
        if (battery2.transform.position == new Vector3(57, 6, 9))
        {
            batteries++;
        }
        if (battery3.transform.position == new Vector3(91, 1, 17))
        {
            batteries++;
        }
    }

    public void showEnd()
    {
        ui.SetActive(true);

        countBattery();

        if(rod)
        {
            did.SetActive(true);
        } else
        {
            didnot.SetActive(true);
        }

        txt.text = batteries.ToString();
    }
}
