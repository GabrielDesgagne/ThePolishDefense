﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{

    void Grabbed();
    void Dropped();
    void Highlight();
    void UnHighlight();


}