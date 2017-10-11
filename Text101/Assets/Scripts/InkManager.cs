using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace InkEngine {
    public class InkManager : MonoBehaviour {

        [SerializeField]
        private TextAsset inkJSONAsset;
        private Story story;

        [SerializeField]
        private Canvas canvas;

        // UI Prefabs
        [SerializeField]
        private Text textPrefab;

        [SerializeField]
        private Button buttonPrefab;

        GameManager _GM;

        [SerializeField]
        public Text textDispay;


        void Start()
        {
            _GM = GetComponent<GameManager>();
            StartStory();
        }


        public void StartStory()
        {

            story = new Story(inkJSONAsset.text);

            List<string> watchList = new List<string> { "hasMirror", "hasHairClip", "isDressedUp", "isTheEnd", "isFree" };
 
            story.ObserveVariables(watchList, (string varname, object value) =>
            {
               
                switch(varname)
                {
                    case "hasMirror":
                        _GM.HasMirror = Convert.ToBoolean(value);
                        break;
                    case "hasHairClip":
                        _GM.HasHairClip = Convert.ToBoolean(value);
                        break;
                    case "isDressedUp":
                        _GM.IsDressedUp = Convert.ToBoolean(value);
                        break;
                    case "isTheEnd":                      
                        _GM.HasHairClip = false;
                        _GM.HasMirror = false;
                        _GM.IsDressedUp = false;
                        _GM.IsFree = true;
                        _GM.IsTheEnd = Convert.ToBoolean(value);
                        break;
                    case "isFree":
                        _GM.IsFree = Convert.ToBoolean(value);
                        break;
                    default:
                        break ;
                }
            });

            RefreshView();
        }

        void RefreshView()
        {
            RemoveChildren();

            while (story.canContinue)
            {
                string text = story.Continue().Trim();
                CreateContentView(text);
            }

            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            }
            else
            {
                Button choice = CreateChoiceView("Play Again?");
                choice.onClick.AddListener(delegate {
                    StartStory();
                });
            }
        }

        void OnClickChoiceButton(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
            RefreshView();
        }

        void CreateContentView(string text)
        {
    
            StopAllCoroutines();
            StartCoroutine(TypeSentence(text));
        }

        IEnumerator TypeSentence(string sentence)
        {
            textDispay.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                textDispay.text += letter;
                yield return null;
            }

        }

        Button CreateChoiceView(string text)
        {
            Button choice = Instantiate(buttonPrefab) as Button;
            choice.transform.SetParent(canvas.transform, false);

            Text choiceText = choice.GetComponentInChildren<Text>();
            choiceText.text = text;

            HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;

            return choice;
        }

        void RemoveChildren()
        {
            int childCount = canvas.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
                GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }
    }
}