﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TwinTower
{
    public class UI_CutScene : UI_Base
    {
        enum Images
        {
            Irise_Image,
            Dalia_Image
        }

        enum Texts
        {
            NameText,
            ChatText
        }   

        private List<string> scripts;
        private int script_idx;
        public override void Init()
        {
            Bind<Image>(typeof(Images));
            Bind<TextMeshProUGUI>(typeof(Texts));

            UIManager.Instance.InputHandler += KeyInput;
            if (DataManager.Instance.StageInfovalue.cutsceneflug != null)
            {
                script_idx = 0;
                scripts = DataManager.Instance.Scripstvalue;
            }
            
            NextScript();
        }

        private void KeyInput()
        {
            if (!Input.anyKey)
                return;

            if (_uiNum != UIManager.Instance.UINum)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (script_idx == scripts.Count)
                {
                    UIManager.Instance.InputHandler -= KeyInput;
                    UIManager.Instance.CloseNormalUI(this);
                }
                else
                {
                    NextScript();
                }
            }
        }

        private void NextScript()
        {
            string [] now = scripts[script_idx].Split(':');
            Color c1 = Get<Image>((int)Images.Irise_Image).gameObject.GetComponent<Image>().color;
            Color c2 = Get<Image>((int)Images.Dalia_Image).gameObject.GetComponent<Image>().color;
            if (now[1] == "1")
            {
                Get<TextMeshProUGUI>((int)Texts.NameText).gameObject.GetComponent<TextMeshProUGUI>().text = "아이리스";
                c1.a = 1f;
                c2.a = 0.5f;

            }
            else
            {
                Get<TextMeshProUGUI>((int)Texts.NameText).gameObject.GetComponent<TextMeshProUGUI>().text = "달리아";
                c1.a = 0.5f;
                c2.a = 1f;
            }
            Get<Image>((int)Images.Irise_Image).gameObject.GetComponent<Image>().color = c1;
            Get<Image>((int)Images.Dalia_Image).gameObject.GetComponent<Image>().color = c2;
            Get<TextMeshProUGUI>((int)Texts.ChatText).gameObject.GetComponent<TextMeshProUGUI>().text = now[0];
            Debug.Log(Get<Image>((int)Images.Irise_Image).gameObject.GetComponent<Image>().color.a);
            script_idx++;
        }
    }
}