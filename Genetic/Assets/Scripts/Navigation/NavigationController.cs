using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>The object that controls the app's navigation. Only one should exist.</summary>
public class NavigationController : Singleton<NavigationController>
{

    /// <summary>The current page that the navigation controller is on.</summary>
    public PageController currentPage;
    /// <summary>The nav button controller. Animates the button and flyout menu.</summary>
    public NavbarButtonController navButtonController;
    /// <summary>The pages in the app.</summary>
    public List<PageController> pages;
    /// <summary>The pages to be added to the app. To make a flyout item that acts as a button, just create a page and overwrite what the 'flyoutTapped' action does.</summary>
    public List<PageController> flyoutItems;
    /// <summary>The GameObject that will contain flyout items. Should have a vertical layout and be within a scroll view.</summary>
    public GameObject flyoutList;
    /// <summary>The flyout item prefab
    /// to attach to the flyoutList.</summary>
    public GameObject flyoutItemPrefab;
    /// <summary>Gets the app controller.</summary>
    /// <value>The app controller.</value>
    public PaletteController controller { get => AppController.instance.controller; }

    /// <summary>The flyout touch background. Becomes active and visible when the flyout is visible. Swipe left to close the app flyout.</summary>
    public Image flyoutTouchBackground;
    /// <summary>The nav bar background.</summary>
    public Image navBackground;
    /// <summary>The nav bar title.</summary>
    public Text navTitle;
    /// <summary>The nav bar button.</summary>
    public Image navButton;

    /// <summary>The flyout header background.</summary>
    public Image flyoutHeaderBackground;
    /// <summary>The flyout background.</summary>
    public Image flyoutBackground;
    /// <summary>The flyout footer background.</summary>
    public Image flyoutFooterBackground;
    /// <summary>
    /// The texts to be recolored. Since NavigationController is a Singleton, it cannot inherit StyleHandler. To make up for this, the NavigationController subscribes to the ColorsChanged event.
    /// </summary>
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

    /// <summary>Sets the page to be viewed.
    /// Will disable the current page, and then enable the page to go to.</summary>
    /// <param name="page">The page.</param>
    public void SetPage(PageController page)
    {
        EnablePage(page);
        navTitle.text = page.pageTitle;
        currentPage = page;
        
    }

    /// <summary>Navigates to the specified page.</summary>
    /// <param name="page">The page to navigate to.</param>
    public void GotoPage(PageController page)
    {
        DisableCurrentPage();
        EnablePage(page);
        currentPage = page;
        navTitle.text = page.pageTitle;
        CloseFlyout();
    }
    /// <summary>Navigates to the specified page by name.</summary>
    /// <param name="name">The name of the page to navigate to.</param>
    public void GotoPage(string name)
    {
        PageController page;
        if (FindPage(name, out page))
        {
            GotoPage(page);
        }
    }

    /// <summary>Navigates to a page by index.</summary>
    /// <param name="index">The index of the page to navigate to.</param>
    public void GotoPage(int index)
    {
        //goes to page by index
        PageController page = pages[index];
        GotoPage(page);
    }

    /// <summary>Enables the specified page.</summary>
    /// <param name="page">The page to enable.</param>
    void EnablePage(PageController page)
    {
        page.OnAppearing();
        page.gameObject.SetActive(true);
    }
    /// <summary>Enables the page by name.</summary>
    /// <param name="name">The name of the page to enable.</param>
    void EnablePage(string name)
    {
        PageController page;
        if(FindPage(name, out page)){
            page.OnAppearing();
            page.gameObject.SetActive(true);
        }
    }

    /// <summary>Disables the specified page.</summary>
    /// <param name="page">The page to disable.</param>
    void DisablePage(PageController page)
    {
        //disables page by object
        page.OnDisappearing();
        page.gameObject.SetActive(false);
    }
    /// <summary>Disables the page by name.</summary>
    /// <param name="name">The name of the page to disable.</param>
    void DisablePage(string name)
    {
        PageController page;
        if (FindPage(name, out page))
        {
            page.OnDisappearing();
            page.gameObject.SetActive(false);
        }
    }
    /// <summary>Disables the current page.</summary>
    void DisableCurrentPage()
    {
        //disables current page
        DisablePage(currentPage);
    }
    /// <summary>Disables all pages.</summary>
    void DisableAllPages()
    {
        foreach (var page in pages)
        {
            DisablePage(page);
        }
    }

    /// <summary>Finds the first page with the given name.</summary>
    /// <param name="name">The name of the page to find.</param>
    /// <param name="page">The page to return.
    /// Will be null if there was no page to be found. The function will return 'False' if there was no page to find.</param>
    /// <returns>Returns 'True' if there was a match and page will be non-null; Returns 'False' if there was no match and page will be null.</returns>
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

    /// <summary>Opens the flyout.</summary>
    public void OpenFlyout()
    {
        navButtonController.Open();
    }
    /// <summary>Closes the flyout.</summary>
    public void CloseFlyout()
    {
        navButtonController.Close();
    }

    /// <summary>Event Handler for when the colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
    public void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        var palette = args.palette;
        navTitle.color = palette.PrimaryText;
        navBackground.color = palette.Navbar;
        navButton.color = palette.FlyoutBtn;
        var tempBackground = palette.FlyoutTouchBackground;
        tempBackground.a = 0.25f;
        flyoutTouchBackground.color = tempBackground;
        flyoutHeaderBackground.color = palette.FlyoutHeaderBackground;
        flyoutBackground.color = palette.FlyoutBackground;
        flyoutFooterBackground.color = palette.FlyoutFooterBackground;
        foreach(var t in PrimaryTexts)
        {
            t.color = palette.PrimaryText;
        }
    }
}
