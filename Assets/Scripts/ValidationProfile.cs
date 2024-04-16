using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValidationProfile : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField nameProfile;
    public TMP_Dropdown logo;
    public Button BotonContinue;

    public bool isSelectedName;
    public bool isSelectedLogo;
    void Start()
    {
        isSelectedName = false;
        isSelectedLogo = false;
        
        
    }

    private void isNameDog(string textNameDog)
    {

        if(string.IsNullOrEmpty(textNameDog)){
          isSelectedName = false;  
        }else{
            isSelectedName = true;
        }
    }
    private void isLogoDog(int indexLogoDog)
    {
        isSelectedLogo = true;
    }

    // Update is called once per frame
    void Update()
    {
        nameProfile.onValueChanged.AddListener(isNameDog);
        logo.onValueChanged.AddListener(isLogoDog);

        if(isSelectedName && isSelectedLogo){
            BotonContinue.interactable = true;
        }else{
            BotonContinue.interactable = false;
        }  
    }
}
