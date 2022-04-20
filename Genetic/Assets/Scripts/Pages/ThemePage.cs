using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePage : PageController
{
    public override Action flyoutTapped => controller.NextPalette;
}
