using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven : UI_Scene
{
    PlayerStat playerstat;
    PlayerController Player;

    enum GameObjects
    {
        //GridPanel,
        ItemPanel,
    }

    enum Images
    {
        HPBarBack,
        HPBar,
        SkillIcon,
        Test,
    }

    enum Buttons
    {

    }

    enum Texts
    {
        HP,
    }

    void Start()
    {
        playerstat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        GameObject itemPanel = Get<GameObject>((int)GameObjects.ItemPanel);
        GetImage((int)Images.HPBarBack);
        //GetImage((int)Images.HPBar);
        GetImage((int)Images.SkillIcon);
        //GetText((int)Texts.HP).text = $"{playerstat.Hp}";
        GetImage((int)Images.Test);


        for (int i = 0; i < 3; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: itemPanel.transform, "Item").gameObject;

            UI_Inven_Item invenitem = Util.GetAddComponent<UI_Inven_Item>(item);
        }
    }

    private void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (Player.ispossession)
        {
            GetImage((int)Images.Test).color = Color.black;
        }
        else
        {
            GetImage((int)Images.Test).color = Color.white;
        }

        GetText((int)Texts.HP).text = $"{playerstat.Hp}";
        GetImage((int)Images.HPBar).fillAmount = Mathf.Lerp(GetImage((int)Images.HPBar).fillAmount, playerstat.Hp / playerstat.MaxHp, Time.deltaTime * 0.9f);
    }
}
