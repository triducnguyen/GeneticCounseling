using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationController : Singleton<NavigationController>
{
    
    public PageController currentPage;
    public NavbarButtonController navButtonController;
    public List<PageController> pages;
    public FlyoutController flyoutCntroller;
    public List<PageController> flyoutItems;
    public GameObject flyoutList;
    public GameObject flyoutItemPrefab;
    public PaletteController controller { get => AppController.instance.controller; }

    public Image flyoutTouchBackground;
    public Image navBackground;
    public Text navTitle;
    public Image navButton;

    public Image flyoutHeaderBackground;
    public Image flyoutBackground;
    public Image flyoutFooterBackground;
    public List<Text> PrimaryTexts;
    

    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        controller.ColorsChanged += ColorsChanged;
        //make sure all pages are disabled
        DisableAllPages();
        //add flyout pages to flyout. We use an aditional list for flyout items so that not all pages are added to the flyout.
        foreach (var item in flyoutItems)
        {
            Debug.Log($"Adding {item.pageName} to flyout");
            var listItem = Instantiate(flyoutItemPrefab, flyoutList.transform);
            FlyoutItemController itemController = listItem.GetComponent<FlyoutItemController>();
            itemController.title.text = item.pageTitle;
            itemController.pageName = item.pageName;
            itemController.icon.sprite = item.icon;
            itemController.action = item.flyoutTapped;
        }
        //set default page (Home)
        SetPage(flyoutItems[0]);
        //set colors
        ColorsChanged(new ColorPaletteChangedEventArgs(controller.currentPalette));
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
        page.OnAppearing();
        page.gameObject.SetActive(true);
    }
    void EnablePage(string name)
    {
        PageController page;
        if(FindPage(name, out page)){
            page.OnAppearing();
            page.gameObject.SetActive(true);
        }
    }

    void DisablePage(PageController page)
    {
        //disables page by object
        page.OnDisappearing();
        page.gameObject.SetActive(false);
    }
    void DisablePage(string name)
    {
        PageController page;
        if (FindPage(name, out page))
        {
            page.OnDisappearing();
            page.gameObject.SetActive(false);
        }
    }
    void DisableCurrentPage()
    {
        //disables current page
        DisablePage(currentPage);
    }
    void DisableAllPages()
    {
        foreach (var page in pages)
        {
            DisablePage(page);
        }
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

    public void OpenFlyout()
    {
        navButtonController.Open();
    }
    public void CloseFlyout()
    {
        navButtonController.Close();
    }

    public void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        var palette = args.palette;
        navTitle.color = palette.PrimaryText;
        navBackground.color = palette.Navbar;
        navButton.color = palette.FlyoutBtn;
        flyoutTouchBackground.color = palette.FlyoutTouchBackground;
        flyoutHeaderBackground.color = palette.FlyoutHeaderBackground;
        flyoutBackground.color = palette.FlyoutBackground;
        flyoutFooterBackground.color = palette.FlyoutFooterBackground;
        foreach(var t in PrimaryTexts)
        {
            t.color = palette.PrimaryText;
        }
    }
}
