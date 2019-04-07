﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMRUSuite.Common;
using OpenMRUSuite.DefaultStorage;

namespace Tests
{
    [TestClass]
    public class MRUItemFileStorageTests
    {
        [TestMethod]
        public void ShouldCreateStorageWithValidPath()
        {
            MRUItemFileStorage fileStorage = new MRUItemFileStorage(path);
            Assert.IsNotNull(fileStorage);
        }

        [TestMethod]
        public void ShouldNotCreateStorageWithInvalidExtension()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                string path = ".mrustorage.pdf";
                MRUItemFileStorage fileStorage = new MRUItemFileStorage(path);
            }, "Storage created with invalid file extension");
        }

        [TestMethod]
        public void ShouldNotCreateStorageWithInvalidPath()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                string path = null;
                MRUItemFileStorage fileStorage = new MRUItemFileStorage(path);
            }, "Storage created with invalid file path");
        }

        [TestMethod]
        public void ShouldReturnEmptyListFromEmptyStorageFile()
        {
            MRUItemFileStorage fileStorage = new MRUItemFileStorage(path);
            List<MRUItem> items = fileStorage.ReadMRUItems();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count == 0);
        }

        [TestMethod]
        public void ShouldSaveAndReadMRUItems()
        {
            MRUItemFileStorage fileStorage = new MRUItemFileStorage(path);
            List<MRUItem> items = CreateItems();
            fileStorage.SaveMRUItems(items);

            List<MRUItem> readedItems = fileStorage.ReadMRUItems();
            Assert.IsNotNull(readedItems);
            Assert.IsTrue(readedItems.Count == 2);
        }

        

        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        readonly string path = "mrustorage.xml";

        private List<MRUItem> CreateItems()
        {
            List<MRUItem> items = new List<MRUItem>();

            MRUItem item1 = new MRUItem
            {
                FilePath = "path1",
                Pinned = true,
                SelectedCount = 1
            };
            MRUItem item2 = new MRUItem
            {
                FilePath = "path2",
                Pinned = false,
                SelectedCount = 3
            };
            items.Add(item1);
            items.Add(item2);

            return items;
        }
    }
}
