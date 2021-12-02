using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationController : Singleton<NavigationController>
{
    public PageController currentPage;

    public List<PageController> pages;

    public Text navTitle;
    public Image navButton;
    public NavbarButtonController navButtonController;

    public FlyoutController flyout;
    public List<PageController> flyoutItems;
    public GameObject flyoutList;
    public GameObject flyoutItemPrefab;


    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        //add flyout items to flyout
        foreach (var item in flyoutItems)
        {
            Debug.Log($"Adding {item.pageName} to flyout");
            var listItem = Instantiate(flyoutItemPrefab, flyoutList.transform);
            FlyoutItemController itemController = listItem.GetComponent<FlyoutItemController>();
            itemController.title.text = item.pageTitle;
            itemController.icon.sprite = item.icon;
            itemController.action = item.flyoutTapped;
        }
        //set default page (Home)
        SetPage(flyoutItems[0]);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPage(PageController page)
    {
        EnablePage(page);
        navTitle.text = page.pageTitle;
        currentPage = page;
        
    }

    public void GotoPage(PageController page)
    {
        DisableCurrentPage();
        EnablePage(page);
        currentPage = page;
        navTitle.text = page.pageTitle;
        CloseFlyout();
    }
    public void GotoPage(string name)
    {
        PageController page;
        if (FindPage(name, out page))
        {
            GotoPage(page);
        }
    }

    public void GotoPage(int index)
    {
        //goes to page by index
        PageController page = pages[index];
        GotoPage(page);
    }

    void EnablePage(PageController page)
    {
        page.gameObject.SetActive(true);
    }
    void EnablePage(string name)
    {
        PageController page;
        if(FindPage(name, out page)){
            page.gameObject.SetActive(true);
        }
    }
    void EnablePage(int index)
    {
        pages[index].gameObject.SetActive(true);
    }

    void DisablePage(PageController page)
    {
        //disables page by object
        page.gameObject.SetActive(false);
    }
    void DisablePage(string name)
    {
        PageController page;
        if (FindPage(name, out page))
        {
            page.gameObject.SetActive(false);
        }
    }
    void DisablePage(int index)
    {
        //disables page by index
        pages[index].gameObject.SetActive(false);
    }
    void DisableCurrentPage()
    {
        //disables current page
        DisablePage(currentPage);
    }
    bool FindPage(string name, out PageController page)
    {
        page = pages.Find(p => p.pageName == name);
        if (page == null)
        {
            //no page by name
            return false;
        }
        else
        {
            return true;
        }
    }

    void CloseFlyout()
    {
        if (flyout.visible)
        {
            navButtonController.Tapped();
        }
    }
}
