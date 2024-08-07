using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] private float textSpeed;

    [SerializeField] private string[] dialogueLines;

    [Header("UI")]
    [SerializeField] private Image playerHpBarImage;
    [SerializeField] private Gradient playerHpBarColor;

    [SerializeField] private GameObject dialogueObj;
    [SerializeField] private TextMeshProUGUI lineText;

    private Color _curPlayerHpBarColor;

    private int _curLineI;

    private StringBuilder _lineBuilder = new();

    private Coroutine _writeLineCor;

    public void SetPlayerHPBar(float value)
    {
        playerHpBarImage.fillAmount = value;

        _curPlayerHpBarColor = playerHpBarColor.Evaluate(value);

        playerHpBarImage.color = _curPlayerHpBarColor;
    }

    public void StartDialogue()
    {
        Observer.OnHandleEvent(EventEnum.DialogueOpened);

        lineText.text = "";
        dialogueObj.SetActive(true);

        StartWritingLine();
    }
    private void StopDialogue()
    {
        StopWritingLine();
        
        dialogueObj.SetActive(false);

        Observer.OnHandleEvent(EventEnum.DialogueClosed);
    }

    //input
    public void OnSkipLine()
    {
        if (_writeLineCor != null) FinishWritingLine();
        else NextLine();
    }

    private void NextLine()
    {
        _curLineI++;

        if (_curLineI < dialogueLines.Length) StartWritingLine();
        else StopDialogue();
    }

    private void StartWritingLine()
    {
        StopWritingLine();

        _writeLineCor = StartCoroutine(WriteLineCor(dialogueLines[_curLineI]));
    }

    private IEnumerator WriteLineCor(string line)
    {
        _lineBuilder.Clear();

        foreach (char c in line.ToCharArray())
        {
            _lineBuilder.Append(c);
            lineText.text = _lineBuilder.ToString();

            yield return new WaitForSecondsRealtime(textSpeed);
        }

        _writeLineCor = null;
    }

    private void FinishWritingLine()
    {
        StopWritingLine();

        lineText.text = dialogueLines[_curLineI];
    }
    private void StopWritingLine()
    {
        if (_writeLineCor != null) StopCoroutine(_writeLineCor);

        _writeLineCor = null;
    }

}
