using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LayersExample;
using System.Collections.Generic;

namespace LayersExampleTests
{
    public class LayersBLMock : LayersExampleModel
    {
        public Boolean IsItemInLeftListForTests(Item item, IList<Item> list)
        {
            return this.IsItemInList(item, list);
        }

        public void MoveItemInListsForTests(IList<Item> listSource, IList<Item> listTarget, Int32 index)
        {
            this.MoveItemInLists(listSource, listTarget, index);
        }
    }

    [TestClass]
    public class LayersBLTests
    {
        [TestMethod]
        public void TestsIsItemInListsShouldBeTrue()
        {
            LayersBLMock mock = new LayersBLMock();
            IList<Item> items = new List<Item>();
            items.Add(new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") });
            Item itemToPass = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };
            Boolean itemInList = mock.IsItemInLeftListForTests(itemToPass, items);

            Assert.IsTrue(itemInList);
        }

        [TestMethod]
        public void TestsIsItemInListsShouldBeFalse()
        {
            LayersBLMock mock = new LayersBLMock();
            IList<Item> items = new List<Item>();
            items.Add(new Item() { ItemName = "2", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") });
            Item itemToPass = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };
            Boolean itemInList = mock.IsItemInLeftListForTests(itemToPass, items);

            Assert.IsTrue(!itemInList);
        }

        [TestMethod]
        public void TestItemEqualsShouldBeTrue()
        {
            Item item1 = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };
            Item item2 = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };

            Assert.AreEqual(item1, item2);
        }

        [TestMethod]
        public void TestItemEqualsShouldBeFalseWithSameHashcodes()
        {
            Item item1 = new Item() { ItemName = "1", ItemAmount = 2, CreationDate = DateTime.Parse("10.11.2011") };
            Item item2 = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };

            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
            Assert.AreNotEqual(item1, item2);
        }

        [TestMethod]
        public void TestGetHashCodeShouldReturn2035()
        {
            Int32 hashCode = 1 + 1 + 11 + 11 + 2011;
            Item item1 = new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };

            Assert.AreEqual(hashCode, item1.GetHashCode());
        }

        [TestMethod]
        public void TestMoveItemInListsShouldSuccess()
        {
            IList<Item> items = new List<Item>();
            IList<Item> items2 = new List<Item>();
            items.Add(new Item() { ItemName = "2", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") });
            LayersBLMock mock = new LayersBLMock();
            mock.MoveItemInListsForTests(items, items2, 0);

            Assert.AreEqual(0, items.Count);
            Assert.AreEqual(1, items2.Count);
            Item iResult = items2[0];
            Assert.AreEqual("2", iResult.ItemName);
            Assert.AreEqual(1, iResult.ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, iResult.CreationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestMoveItemInListsShouldThrowOutOfRange()
        {
            IList<Item> items = new List<Item>();
            IList<Item> items2 = new List<Item>();
            LayersBLMock mock = new LayersBLMock();
            mock.MoveItemInListsForTests(items, items2, 123);
        }

        [TestMethod]
        public void TestMoveItemShouldMoveToRight()
        {
            LayersBLMock mock = new LayersBLMock();
            Item itemToMove = new Item() { ItemName = "2", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };
            mock.List1.Add(itemToMove);

            mock.MoveItem(itemToMove);

            Assert.AreEqual(3, mock.List1.Count);
            Assert.AreEqual(1, mock.List2.Count);
            Item iResult = mock.List2[0];
            Assert.AreEqual("2", iResult.ItemName);
            Assert.AreEqual(1, iResult.ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, iResult.CreationDate);

        }

        [TestMethod]
        public void TestMoveItemShouldMoveToLeft()
        {
            LayersBLMock mock = new LayersBLMock();
            Item itemToMove = new Item() { ItemName = "2", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };
            mock.List2.Add(itemToMove);
            mock.MoveItem(itemToMove);

            Assert.AreEqual(4, mock.List1.Count);
            Assert.AreEqual(0, mock.List2.Count);
            Item iResult = mock.List1[3];
            Assert.AreEqual("2", iResult.ItemName);
            Assert.AreEqual(1, iResult.ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, iResult.CreationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundInListException))]
        public void TestMoveItemShouldThrowItemNotFound()
        {
            LayersBLMock mock = new LayersBLMock();
            Item itemToMove = new Item() { ItemName = "2", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") };            
            mock.MoveItem(itemToMove);
        }
    }
}
