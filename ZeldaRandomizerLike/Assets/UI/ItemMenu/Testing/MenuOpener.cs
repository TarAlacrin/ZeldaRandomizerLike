using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{

	public GameObject testmenuObject;
	IOpenMenu menutoopen;
    // Start is called before the first frame update
    void Awake()
    {
		menutoopen = testmenuObject.GetComponent<IOpenMenu>();

	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (menutoopen.MenuIsOpen())
				menutoopen.CloseMenu();
			else
				menutoopen.OpenMenu();
		}
    }
}
