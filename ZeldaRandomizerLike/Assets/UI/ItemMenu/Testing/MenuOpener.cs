using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
	[Dependency("ItemMenu")]
	IOpenMenu itemMenuToOpen = null;
	[Dependency]
	IHandlePlayerControlState playerControlStateManager = null;
    // Start is called before the first frame update
    void Awake()
    {
		this.ResolveDependencies();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (itemMenuToOpen.MenuIsOpen())
				itemMenuToOpen.CloseMenu();
			else if(playerControlStateManager.PlayerCanOpenMenus())
				itemMenuToOpen.OpenMenu();
		}
    }
}
