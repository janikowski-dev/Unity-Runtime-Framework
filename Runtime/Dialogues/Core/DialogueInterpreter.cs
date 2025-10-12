using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Internal.Runtime.Dialogues.Steps;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Core
{
    public class DialogueInterpreter : MonoBehaviour
    {
        private TextAsset _rawDialogue;
        
        public Dialogue Interpret(TextAsset rawDialogue)
        {
            Cache(rawDialogue);
            return InterpretText(InterpretLine);
        }

        private void Cache(TextAsset rawDialogue)
        {
            _rawDialogue = rawDialogue;
        }

        private Dialogue InterpretText(Action<Dialogue, string> interpretLineCallback)
        {
            using StringReader reader = new StringReader(_rawDialogue.text);
            Dialogue dialogue = new();
            
            while (reader.ReadLine() is { } line)
            {
                interpretLineCallback(dialogue, line);
            }

            return dialogue;
        }

        private void InterpretLine(Dialogue dialogue, string line)
        {
            string[] elements = line.Split("###");

            if (elements.Length == 0)
            {
                return;
            }
            
            switch (elements[0].Trim())
            {
                case "Text":
                    TextStep textStep = new TextStep { Text = elements[1].Trim() };
                    dialogue.Steps.Add(textStep);
                    break;
                case "Unskippable Text":
                    UnskippableTextStep unskippableText = new UnskippableTextStep { Text = elements[1].Trim() };
                    dialogue.Steps.Add(unskippableText);
                    break;
                case "Callback":
                    CallbackStep callbackStep = new CallbackStep { Key = elements[1].Trim() };
                    ClearStep clearStep = new ClearStep();
                    dialogue.Steps.Add(clearStep);
                    dialogue.Steps.Add(callbackStep);
                    break;
                case "Delay":
                    DelayStep delayStep = new DelayStep { DelayInSeconds = float.Parse(elements[1].Trim(), CultureInfo.InvariantCulture) };
                    dialogue.Steps.Add(delayStep);
                    break;
                case "Icon":
                    IconStep iconStep = new IconStep { Key = elements[1].Trim() };
                    dialogue.Steps.Add(iconStep);
                    break;
                case "Name":
                    NameStep nameStep = new NameStep { Name = elements[1].Trim() };
                    dialogue.Steps.Add(nameStep);
                    break;
                case "Question":
                    string[] subElements = elements[1].Split("|");
                    TextStep questionStep = new TextStep { Text = subElements[0].Trim() };
                    AnswerStep answerStep = new AnswerStep
                    {
                        Answers = subElements[1].Split("/").Select(answer => answer.Trim()).ToArray(),
                        Key = subElements[0].Trim()
                    };
                    dialogue.Steps.Add(questionStep);
                    dialogue.Steps.Add(answerStep);
                    break;
                case "Await":
                    AwaitStep awaitStep = new AwaitStep { Key = elements[1].Trim() };
                    dialogue.Steps.Add(awaitStep);
                    break;
            }
        }
    }
}