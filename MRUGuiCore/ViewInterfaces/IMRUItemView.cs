﻿using System;

namespace MRUGuiCore.ViewInterfaces
{
    public interface IMRUItemView
    {
        event Action<string> PinItemRequested;
        event Action<string> DeleteItemRequested;
        event Action<string> ItemSelected;
    }
}