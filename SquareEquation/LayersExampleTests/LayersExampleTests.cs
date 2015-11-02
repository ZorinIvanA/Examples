using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LayersExample;

namespace LayersExampleTests
{
    /// <summary>
    /// Заглушка модели для тестов
    /// </summary>
    public class LayersModelMock : ILayersModel
    {

        public IList<Item> List1
        {
            get;
            set;
        }

        public IList<Item> List2
        {
            get;
            set;
        }

        public LayersModelMock()
        {
            List1 = new List<Item>();
            List2 = new List<Item>();
        }

        public void MoveItem(Item item)
        {
            if (item.ItemName == "1")
            {
                List2.Add(new Item() { ItemName = "1", ItemAmount = 1, CreationDate = DateTime.Parse("11.11.2011") });
                List1.Clear();
            }
            else
            {
                List1.Add(new Item() { ItemName = "2", ItemAmount = 2, CreationDate = DateTime.Parse("12.12.2012") });
                List2.Clear();
            }
        }
    }

    public class LayersViewModelForTests : LayersWindowViewModel
    {
        public ILayersModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public LayersViewModelForTests(Boolean useMock)
            : base()
        {
            if (useMock)
            {
                _model = new LayersModelMock();
                LeftList = FillObservable(_model.List1);
                RightList = FillObservable(_model.List2);
            }
        }
    }

    [TestClass]
    public class LayersExampleTests
    {
        [TestMethod]
        public void TestFillObservableShouldSuccess()
        {
            LayersWindowViewModel vm = new LayersWindowViewModel();
            IList<Item> items = new List<Item>()
            {
                new Item(){ItemName = "Item1", ItemAmount = 1, CreationDate=DateTime.Now},
                new Item(){ItemName = "Item2", ItemAmount = 2, CreationDate=DateTime.Now},
                new Item(){ItemName = "Item3", ItemAmount = 3, CreationDate=DateTime.Now}
            };
            var result = vm.FillObservable(items);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Item1", result[0].ItemName);
            Assert.AreEqual("Item2", result[1].ItemName);
            Assert.AreEqual("Item3", result[2].ItemName);
            Assert.AreEqual(1, result[0].ItemAmount);
            Assert.AreEqual(2, result[1].ItemAmount);
            Assert.AreEqual(3, result[2].ItemAmount);
        }

        [TestMethod]
        public void TestViewModelConstructor()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(false);
            Assert.IsNotNull(vm.Model);
            Assert.IsInstanceOfType(vm.Model, typeof(LayersExampleModel));
        }

        [TestMethod]
        public void TestMoveToRightShouldSuccess()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);            
            vm.LeftList.Add(new ListItemViewModel()
            {
                ItemName = "1",
                CreationDate = DateTime.Parse("11.11.2011"),
                ItemAmount = 1
            });
            vm.MoveToRight(0);
            Assert.AreEqual(0, vm.LeftList.Count);
            Assert.AreEqual(1, vm.RightList.Count);
            Assert.AreEqual("1", vm.RightList[0].ItemName);
            Assert.AreEqual(1, vm.RightList[0].ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, vm.RightList[0].CreationDate);
        }

        [TestMethod]
        public void TestMoveToRightShouldFailWhenBigIndex()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);
            vm.LeftList.Add(new ListItemViewModel()
            {
                ItemName = "1",
                CreationDate = DateTime.Parse("11.11.2011"),
                ItemAmount = 1
            });
            vm.MoveToRight(2);
            Assert.AreEqual(1, vm.LeftList.Count);
            Assert.AreEqual(0, vm.RightList.Count);
            Assert.AreEqual("1", vm.LeftList[0].ItemName);
            Assert.AreEqual(1, vm.LeftList[0].ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, vm.LeftList[0].CreationDate);
        }

        [TestMethod]
        public void TestMoveToRightShouldFailWhenIndexLess0()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);
            vm.LeftList.Add(new ListItemViewModel()
            {
                ItemName = "1",
                CreationDate = DateTime.Parse("11.11.2011"),
                ItemAmount = 1
            });
            vm.MoveToRight(-1);
            Assert.AreEqual(1, vm.LeftList.Count);
            Assert.AreEqual(0, vm.RightList.Count);
            Assert.AreEqual("1", vm.LeftList[0].ItemName);
            Assert.AreEqual(1, vm.LeftList[0].ItemAmount);
            DateTime dt = DateTime.Parse("11.11.2011");
            Assert.AreEqual(dt, vm.LeftList[0].CreationDate);
        }

        [TestMethod]
        public void TestMoveToLeftShouldSuccess()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);
            vm.RightList.Add(new ListItemViewModel()
            {
                ItemName = "2",
                CreationDate = DateTime.Parse("12.12.2012"),
                ItemAmount = 2
            });
            vm.MoveToLeft(0);
            Assert.AreEqual(0, vm.RightList.Count);
            Assert.AreEqual(1, vm.LeftList.Count);
            Assert.AreEqual("2", vm.LeftList[0].ItemName);
            Assert.AreEqual(2, vm.LeftList[0].ItemAmount);
            DateTime dt = DateTime.Parse("12.12.2012");
            Assert.AreEqual(dt, vm.LeftList[0].CreationDate);
        }

        [TestMethod]
        public void TestMoveToLeftShouldFailWhenBigIndex()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);
            vm.RightList.Add(new ListItemViewModel()
            {
                ItemName = "2",
                CreationDate = DateTime.Parse("12.12.2012"),
                ItemAmount = 2
            });
            vm.MoveToLeft(2);
            Assert.AreEqual(0, vm.LeftList.Count);
            Assert.AreEqual(1, vm.RightList.Count);
            Assert.AreEqual("2", vm.RightList[0].ItemName);
            Assert.AreEqual(2, vm.RightList[0].ItemAmount);
            DateTime dt = DateTime.Parse("12.12.2012");
            Assert.AreEqual(dt, vm.RightList[0].CreationDate);
        }

        [TestMethod]
        public void TestMoveToLeftShouldFailWhenIndexLess0()
        {
            LayersViewModelForTests vm = new LayersViewModelForTests(true);
            vm.RightList.Add(new ListItemViewModel()
            {
                ItemName = "2",
                CreationDate = DateTime.Parse("12.12.2012"),
                ItemAmount = 2
            });
            vm.MoveToLeft(-2);
            Assert.AreEqual(0, vm.LeftList.Count);
            Assert.AreEqual(1, vm.RightList.Count);
            Assert.AreEqual("2", vm.RightList[0].ItemName);
            Assert.AreEqual(2, vm.RightList[0].ItemAmount);
            DateTime dt = DateTime.Parse("12.12.2012");
            Assert.AreEqual(dt, vm.RightList[0].CreationDate);
        }
    }
}
