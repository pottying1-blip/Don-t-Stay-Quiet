
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject consumeGuidance;
    public Transform playerPos;
    private Camera mainCamera;
    private UnityEngine.Vector2 offset = new UnityEngine.Vector2(0f, -130f);
    public GameObject dialogue;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI goodChoicesText;
    public TextMeshProUGUI badChoicesText;
    public Button goodButton;
    public Button badButton;
    private DialogueEntry currentEntry;
    private HumanStateManager currentNPC;
    public GameObject talkingDialogueHolder;
    public PlayerController playerController;
    void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveToPlayer();
    }

    public void ShowDialogue(DialogueEntry entry, HumanStateManager npc)
    {
        talkingDialogueHolder.SetActive(true);
        currentNPC = npc;
        currentEntry = entry;
        questionText.text = entry.question;
        goodChoicesText.text = entry.goodAnswer.choiceText;
        badChoicesText.text = entry.badAnswer.choiceText;
    }

    public void OnGoodButtonClicked()
    {
        ApplyChoice(currentEntry.goodAnswer);
        currentNPC.isTalking = false;
        currentNPC.hasTalked = true;
        playerController.isTalking = false;
    }

    public void OnBadButtonClicked()
    {
        ApplyChoice(currentEntry.badAnswer);
        currentNPC.isTalking = false;
        currentNPC.hasTalked = true;
        playerController.isTalking = false;
    }

    void ApplyChoice(DialogueChoice choice)
    {
        currentNPC.suspicionLevel += choice.suspicionAmount;
        GameManager.Instance.globalAlertLevel += choice.alertAmount;
        talkingDialogueHolder.SetActive(false);
    }
    void MoveToPlayer()
    {
        UnityEngine.Vector3 truePlayerPos = mainCamera.WorldToScreenPoint(playerPos.position);
        consumeGuidance.transform.position = truePlayerPos + (UnityEngine.Vector3)offset;
    }
}
