using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler{
    [SerializeField] private GameObject _car;
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 5;
    private int currentPage = 3;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start(){
        panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data){
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference/50, 0, 0);
    }
    public void OnEndDrag(PointerEventData data){  
        float percentage = (data.pressPosition.x - data.position.x)/50 / ((mainCamera.orthographicSize * 2f) * mainCamera.aspect);
        if(Mathf.Abs(percentage) >= percentThreshold){
            if(currentPage == 2) _car.SetActive(false);
            Vector3 newLocation = panelLocation;
            if(percentage > 0 && currentPage < totalPages){
                currentPage++;
                newLocation += new Vector3(-(mainCamera.orthographicSize * 2f) * mainCamera.aspect, 0, 0);
            }else if(percentage < 0 && currentPage > 1){
                currentPage--;
                newLocation += new Vector3((mainCamera.orthographicSize * 2f) * mainCamera.aspect, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }else{
            if(currentPage == 2) _car.SetActive(true);
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        //if(currentPage == 2) _car.SetActive(false);
        //print(Vector3.Distance(startpos, endpos));
        float t = 0f;
        while(t <= 1.0){
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        if(currentPage == 2) _car.SetActive(true);
    }

    public void ChangePanel(int page)
    {
        
        int movements = Mathf.Abs(page - currentPage);
        Vector3 newLocation = panelLocation;
        
        if(page > currentPage)
        {
            newLocation += new Vector3(((mainCamera.orthographicSize * 2f) * mainCamera.aspect) * -movements, 0, 0);
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
        }
        else
        {
            newLocation += new Vector3(((mainCamera.orthographicSize * 2f) * mainCamera.aspect) * movements, 0, 0);
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
        }
        currentPage = page;
        if(currentPage != 2) _car.SetActive(false);
        panelLocation = newLocation;
    }
}