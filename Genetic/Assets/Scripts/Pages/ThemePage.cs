using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Controls the behavior of the ThemePage.</summary>
public class ThemePage : PageController
{
    /// <summary>The action to perform when this page's flyout item is tapped. Overwritten to swap to next palette instead of navigating to page.</summary>
    /// <value>The action to perform.</value>
    public override Action flyoutTapped => controller.NextPalette;
}
