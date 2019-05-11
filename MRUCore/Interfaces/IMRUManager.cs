﻿using System;
using System.Collections.Generic;

namespace MRUCore.Interfaces
{
    public interface IMRUManager
    {
        event Action MRUItemsListChanged;
        List<MRUItem> MRUItems { get; }
        void Initialize(IMRUItemStorage storage);
        void AddFile(string path);
        void RemoveFile(string path);
        void ClearMRUItems();
        void ChangePinStateForFile(string path);
        void SelectFile(string path);
        
    }
}
