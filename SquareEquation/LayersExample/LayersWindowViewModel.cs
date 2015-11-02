using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersExample
{
    public class ListItemViewModel
    {
        public String ItemName { get; set; }
        public Int32 ItemAmount { get; set; }
        public DateTime CreationDate { get; set; }

    }

    /// <summary>
    /// ViewModel
    /// </summary>
    public class LayersWindowViewModel : INotifyPropertyChanged
    {
        public String LeftListName { get; set; }
        public String RightListName { get; set; }

        public Int32 SelectedInLeft { get; set; }
        public Int32 SelectedInRight { get; set; }

        public ObservableCollection<ListItemViewModel> LeftList { get; set; }
        public ObservableCollection<ListItemViewModel> RightList { get; set; }

        protected ILayersModel _model;

        public LayersWindowViewModel()
        {
            LeftListName = "Список с левой стороны";
            RightListName = "Список с правой стороны";

            _model = (ILayersModel)(Activator.CreateInstance(this.GetCurrentLayersModel()));
            RefillLists();
        }

        /// <summary>
        /// Заполняет обозреваемую коллекцию из бизнес-логики
        /// </summary>
        /// <param name="items">Список из бизнес-логики</param>
        /// <returns>Обозреваемая коллекция</returns>
        public ObservableCollection<ListItemViewModel> FillObservable(IList<Item> items)
        {
            var newObservable = new ObservableCollection<ListItemViewModel>();
            foreach (var item in items)
            {
                newObservable.Add(new ListItemViewModel()
                {
                    CreationDate = item.CreationDate,
                    ItemAmount = item.ItemAmount,
                    ItemName = item.ItemName
                });
            }
            return newObservable;
        }

        /// <summary>
        /// Возвращает текущую модель из свойств приложения
        /// См. меню Проект -> Свойства
        /// </summary>
        /// <returns>Тип текущей модели</returns>
        protected Type GetCurrentLayersModel()
        {
            String typeName = "LayersExample." + Properties.Settings.Default.CurrentModel;
            Type modelType = Type.GetType(typeName);
            return modelType;
        }

        /// <summary>
        /// Двигает элемент слева направо через BL
        /// </summary>
        /// <param name="index">индекс элмента в левом окне</param>
        public void MoveToRight(Int32 index)
        {
            if ((index > -1) && (LeftList.Count >= index))
            {
                MoveVMItem(LeftList, index);
                RefillLists();
            }
        }

        /// <summary>
        /// Обращается к BL для перемещения объекта
        /// </summary>
        /// <param name="list">Список</param>
        /// <param name="index">индекс элемента в списке</param>
        protected void MoveVMItem(ObservableCollection<ListItemViewModel> list, Int32 index)
        {
            ListItemViewModel model = list[index];
            Item itemToMove = new Item()
            {
                CreationDate = model.CreationDate,
                ItemAmount = model.ItemAmount,
                ItemName = model.ItemName
            };
            _model.MoveItem(itemToMove);
        }

        /// <summary>
        /// Перезаполняет обозреваемые коллекции
        /// </summary>
        protected void RefillLists()
        {
            LeftList = FillObservable(_model.List1);
            RightList = FillObservable(_model.List2);
            NotifyPropertyChanged("LeftList");
            NotifyPropertyChanged("RightList");
        }

        /// <summary>
        /// Двигает элемент из правого списка в левый
        /// </summary>
        /// <param name="index">Индекс передвигаемого элемента</param>
        public void MoveToLeft(Int32 index)
        {
            if ((index > -1) && (RightList.Count >= index))
            {
                MoveVMItem(RightList, index);
                RefillLists();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;



        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {               
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
