using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitSelection : MonoBehaviour {

    public string[] unitNames;
	public GameObject priceTag;
	public GameObject uiCost;
    //public string[] unitPrefabsLocation;
    public GameObject[] unitPrefabsLocation;
    public GameObject rangeSpot;
	public GameObject confirmationPop;
	

    public int selectedUnit =-1;
    
	

    //references
    private PutUnit putUnit;
	private GameObject unitRange;
	private Vector3 currentPosition;
	private bool isSelected = false;
	private bool isBodySelected = false;
	private GameObject selectedUnitBody;

	private GameObject selectedUnitIcon;
	

	private int cost = 0;
	private int totalCost = 0;




    void Start()
    {

    }

    

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			
			if (Physics.Raycast(ray, out hit, 100))
			{
				
				if (hit.collider.tag == "block" && isSelected)
				{
					//Debug.Log(hit.collider.gameObject.name);
					putUnit = hit.collider.transform.parent.GetComponent<PutUnit>();
					//Debug.Log(hit.collider.tag);
					if (!putUnit.isUnitPlaced && totalCost+cost <= UserData.Money)
					{

						//GameObject unit = (GameObject)Instantiate(Resources.Load(unitPrefabsLocation[selectedUnit]));
						var unit = (GameObject)Instantiate(unitPrefabsLocation[selectedUnit]);
						unit.name = unitNames[selectedUnit];

						unit.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.25F, hit.collider.transform.position.z);
						putUnit.isUnitPlaced = true;

						unit.transform.parent = hit.collider.transform.parent;
                        /*
						float height = unit.transform.GetChild(3).GetComponent<SphereCollider>().radius / Mathf.Sin(Mathf.Deg2Rad * 15F);
                        
						Vector3 spotPosition = new Vector3(hit.collider.transform.position.x, height, hit.collider.transform.position.z);
						unitRange = (GameObject)Instantiate(rangeSpot);
						unitRange.transform.position = spotPosition;
						unitRange.tag = Tags.UnitRangeSpot;
                        unitRange.transform.parent = unit.transform;
                        */

                        totalCost += cost;
						uiCost.GetComponent<Text>().text = totalCost + "";

							
                        
					}
				}
				else if (hit.collider.tag == Tags.UnitBody)
				{
					isBodySelected = true;
					if (hit.collider.gameObject != selectedUnitBody && selectedUnitBody != null)
					{
						confirmationPop.SetActive(false);
						selectedUnitBody.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(selectedUnitBody.GetComponent("Halo"), false, null);
					}
					
					selectedUnitBody = hit.collider.gameObject;
					confirmationPop.SetActive(true);
					selectedUnitBody.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(selectedUnitBody.GetComponent("Halo"), true, null);
					
				}
				else
				{
					if(isBodySelected)
					{
						hit.collider.gameObject.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(selectedUnitBody.GetComponent("Halo"), false, null);
						confirmationPop.SetActive(false);
					}
					isBodySelected = false;
					
				}
			}
			
			isSelected = false;
			ResetPriceTag();
        }
        
    }

	public void SelectUnit(string unitName)
    {
		if (totalCost < UserData.Money) { 
			isSelected = true;
			selectedUnit = IndexOf(unitName);

			if (unitName == "HG")
			{
				priceTag.transform.position = GameObject.FindGameObjectWithTag(Tags.UnitHG).transform.position;
				priceTag.transform.position = new Vector3(priceTag.transform.position.x + 30,priceTag.transform.position.y - 40,priceTag.transform.position.z  );
				priceTag.GetComponent<Text>().text = "100";

				cost = 100;
			
			}
			else if (unitName == "SG")
			{
				priceTag.transform.position = GameObject.FindGameObjectWithTag(Tags.UnitSG).transform.position;
				priceTag.transform.position = new Vector3(priceTag.transform.position.x + 30, priceTag.transform.position.y - 40, priceTag.transform.position.z);
				priceTag.GetComponent<Text>().text = "150";

				cost = 150;
			}
		}
        //Debug.Log(selectedUnit);
        //Debug.Log(unitPrefabsLocation[selectedUnit]);
    }

	public void SellUnit()
	{
		if (selectedUnitBody != null)
		{
			
			confirmationPop.SetActive(false);
			if (selectedUnitBody.transform.parent.name == "HG")
			{
				

				cost = -100;

			}
			else if (selectedUnitBody.transform.parent.name == "SG")
			{
				

				cost = -150;
			}
			selectedUnitBody.transform.parent.parent.gameObject.GetComponent<PutUnit>().isUnitPlaced = false;
			Destroy(selectedUnitBody.transform.parent.gameObject);
			selectedUnitBody = null;
			isBodySelected = false;
			totalCost += cost;
			uiCost.GetComponent<Text>().text = totalCost + "";
			cost = 0;
		}
	}

	private void ResetPriceTag()
	{
		cost = 0;
		priceTag.transform.position = new Vector3(1000, 1000, priceTag.transform.position.z);
	}
    
    int IndexOf(string str)
    {
        for(int i = 0; i < unitNames.Length; i++)
        {
            if (unitNames[i] == str)
            {
                return i;
            }
        }
        return -1;
    }

}
