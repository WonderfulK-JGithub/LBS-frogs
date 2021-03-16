using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{

    [Header("Weapons")]
    public GameObject activeWeapon;
    public List<GameObject> weapons = new List<GameObject>();

    [Header("Inventory Slots")]
    public GameObject activeSlot;
    public List<GameObject> inventorySlots = new List<GameObject>();

    [SerializeField]
    private int _activeSlotNum;
    public int activeSlotNum
    {
        get
        {
            return _activeSlotNum;
        }
        set
        {
            _activeSlotNum = value;
            if (_activeSlotNum > weapons.Count-1)
            {
                _activeSlotNum = 0;
            }
            else if (_activeSlotNum < 0)
            {
                _activeSlotNum = weapons.Count-1;
            }
        }
    }


    [Header("Customize")]
    [Range(0, 9)] public int inventorySlotsAmount = 5;
    public int spacesBetweenSlots;
    public Vector2 position;

    [Header("Prefabs")]
    public GameObject inventorySlotPrefab;
    public GameObject defaultWeapon;

    [Header("Debug")]
    public bool addWeapon = false;

    //Private variables
    Transform canvas;
    PlayerMovement player;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //Skapar ett empty GameObject som ska fungera som parent till alla inventorySLots - Max
        Transform inventoryParent = new GameObject().transform;
        inventoryParent = Instantiate(inventoryParent, canvas);
        inventoryParent.localPosition = position;

        //Skapar alla inventorySlots och placerar dem på en rad - Max
        for (int i = -(Mathf.FloorToInt(inventorySlotsAmount/2)); i <= Mathf.FloorToInt(inventorySlotsAmount / 2); i++)
        {
            Debug.Log(i);

            RectTransform newSlot = Instantiate(inventorySlotPrefab, inventoryParent).GetComponent<RectTransform>();
            newSlot.anchoredPosition += new Vector2(spacesBetweenSlots*i, 0);

            inventorySlots.Add(newSlot.gameObject);
        }

        //Lägger till defaultvapnet i första sloten - Max
        AddWeapon(defaultWeapon);

        //Gör slot[0] till den aktiva - Max
        SetActiveSlot(0);
    }

    void Update()
    {
        //Om man skrollar så ska man ändra activeSlot - Max
        if(Input.mouseScrollDelta.y != 0)
        {
            SetActiveSlot(activeSlotNum + (Input.mouseScrollDelta.y > 0 ? -1 : 1));
        }

        //Om någon siffra trycks in så ska man ändra activeSlot till det slotet, men bara om det finns ett vapen där såklart - Max
        int pressedNumber = GetPressedNumber();
        if(pressedNumber != -1)
        {
            if(pressedNumber <= weapons.Count)
            {
                SetActiveSlot(pressedNumber-1);
            }
        }

        //Kollar om throw-knappen trycks ner, då ska vapnet man håller i kastas bort - Max
        if (Input.GetButtonDown("Fire2"))
        {
            ThrowWeapon();
        }

        //Bara för debug-menyn, man kan lägga in vapen via inspectorn - Max
        if (addWeapon != false)
        {
            addWeapon = false;
            AddWeapon(defaultWeapon);
        }
    }

    public int GetPressedNumber()
    {
        //Loopar igenom alla nummertangenter som leder till ett inventorySlot med ett vapen på och returnar det nummer som är nedtryckt - Max
        for (int number = 1; number <= weapons.Count; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }

        return -1;
    }

    public void SetActiveSlot(int slotNum)
    {
        activeSlotNum = slotNum;
        Debug.Log(activeSlotNum);

        //När activeSlot ska ges ett GameObject för första gången finns det ingen activeSlot att ändra färg på innan, därför krävs en nullcheck - Max
        if(activeSlot != null)
        {
            //Här inne är det som händer med den förra activeSloten - förmodligen så resetas den till att se ut som den gjorde innan den blev active - Max
            activeSlot.GetComponent<Image>().color = Color.white;
        }
        
        //Ändrar activeSlot till den nya - Max
        activeSlot = inventorySlots[activeSlotNum];

        //Här så finns det som händer med den nya activeSloten, så att spelaren kan se att det är det slotet som används - Max
        activeSlot.GetComponent<Image>().color = Color.green;

        //Om det finns ett vapen på det nya activeSlot så sätts det till det vapen som spelaren har equippat - Max
        if(weapons.Count-1 >= slotNum && slotNum >= 0)
        {
            activeWeapon = weapons[slotNum];
            activeWeapon.GetComponent<WeaponScript>().SetActive(true);
            for (int i = 0; i < weapons.Count; i++)
            {
                if(weapons[i] != activeWeapon)
                {
                    weapons[i].GetComponent<WeaponScript>().SetActive(false);
                }
            }
        }

        if(activeSlotNum == 0)
        {
            player.anim.SetBool("hasHögaffel", true);
        }
        else
        {
            player.anim.SetBool("hasHögaffel", false);
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon, Vector3.zero, Quaternion.identity);
        newWeapon.GetComponent<WeaponScript>().playerTr = GameObject.Find("Player").transform;
        //If-statement kollar om det finns plats för ett nytt vapen i inventoryt/hotbaren - Max
        if(weapons.Count < inventorySlotsAmount)
        {
            //Finns det plats så skapas ett nytt vapen på första möjliga inventorySlot - Max
            weapons.Add(newWeapon);
            Instantiate(newWeapon.transform.GetChild(0), inventorySlots[weapons.Count - 1].transform);
        }
        else
        {
            //Annars så byts det equippade vapnet ut mot det man plockar upp - Max
            weapons[activeSlotNum] = newWeapon;
            Destroy(inventorySlots[activeSlotNum].transform.GetChild(0).gameObject);
            Instantiate(newWeapon.transform.GetChild(0), inventorySlots[activeSlotNum].transform);
        }
    }

    public void ThrowWeapon()
    {
        //Man kan inte kasta bort det första vapnet, högaffeln - Max
        if(activeSlotNum == 0)
        {
            return;
        }

        //Om det inte finns något vapen längre till höger i inventoryt så ska detta vapen helt försvinna - Max
        if(activeSlotNum == weapons.Count - 1)
        {
            weapons.Remove(weapons[activeSlotNum]);
        }

        //Tar bort previewen av vapnet - Max
        Destroy(inventorySlots[activeSlotNum].transform.GetChild(0).gameObject);

        //En for-loop som går igenom alla vapen i inventorySlotsen till höger om slotet man kastar från, för att flytta alla vapen ett steg åt vänster för att fylla hålet efter vapnet som försvinner - Max
        //i representerar, som man kan se, i början slotet till höger om det man kastar från, och rör sig sedan åt höger för varje loop - Max
        int weaponsCount = weapons.Count-1;
        for (int i = activeSlotNum+1; i <= weaponsCount; i++)
        {
            //Ger slotet till vänster om i slot[i]:s vapen - Max
            weapons[i-1] = weapons[i];
            Instantiate(weapons[i].transform.GetChild(0), inventorySlots[i-1].transform);

            //Om det är den sista slotet som har ett vapen så ska man också ta bort det vapnet helt - Max
            if(i == weaponsCount)
            {
                weapons.RemoveAt(i);
            }

            Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
        }

        //Ändrar activeSlot till slotet till vänster om det som man kastade från - Max
        SetActiveSlot(activeSlotNum-1);

        //Något som spawnar vapnet man kastar ut ska vara här
    }
}
