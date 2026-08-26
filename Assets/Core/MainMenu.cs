using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject hart;
    public GameObject Body;
    public GameObject insideBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hart.SetActive(false);
        Body.SetActive(true);
        insideBody.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click_Hart()
    {
        hart.SetActive(true);
        Body.SetActive(false);
        insideBody.SetActive(false);
    }
    public void Click_Body()
    {
        hart.SetActive(false);
        Body.SetActive(true);
        insideBody.SetActive(false);
    }
    public void Click_InsideBody()
    {
        hart.SetActive(false);
        Body.SetActive(false);
        insideBody.SetActive(true);
    }
}
