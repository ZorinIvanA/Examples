using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersExample
{

    public class ItemNotFoundInListException : Exception
    {
        public ItemNotFoundInListException(Item item)
            : base(String.Format("Элемент {0} не найден в модели", item))
        { }
    }

    /// <summary>
    /// Интерфейс модели из бизнес-логики
    /// </summary>
    public interface ILayersModel
    {
        IList<Item> List1 { get; set; }
        IList<Item> List2 { get; set; }

        void MoveItem(Item item);

    }

    /// <summary>
    /// Модель итема в бизнес-логике
    /// </summary>
    public class Item
    {
        public String ItemName { get; set; }
        public Int32 ItemAmount { get; set; }
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Сравнивает 2 итема и определяет их эквивалентность
        /// </summary>
        /// <param name="obj">Объект, с которым сравнивать</param>
        /// <returns>Равны объекты или нет</returns>
        public override bool Equals(object obj)
        {
            Item passed = obj as Item;
            //Если хеш-коды итемов не равны, значит итемы 100% разные, 
            //в противном случае - надо проверять
            if ((passed != null) && (passed.GetHashCode() == this.GetHashCode()))
            {
                return (passed.CreationDate == this.CreationDate &&
                    passed.ItemAmount == this.ItemAmount &&
                    this.ItemName == passed.ItemName);
            }
            else { return false; }
        }

        /// <summary>
        /// Получает хеш-код элемента
        /// Хеш-код - это целое число, которое используется для быстрого определения неравенства
        /// </summary>
        /// <returns>Хеш-код элемента</returns>
        public override int GetHashCode()
        {
            Int32 LettersInNameCount = this.ItemName.Length;
            Int32 amount = this.ItemAmount;
            Int32 dataCount = this.CreationDate.Day + this.CreationDate.Month + this.CreationDate.Year;
            return LettersInNameCount + amount + dataCount;
        }
    }

    /// <summary>
    /// Модель бизнес-логики
    /// </summary>
    public class LayersExampleModel : ILayersModel
    {
        public IList<Item> List1 { get; set; }
        public IList<Item> List2 { get; set; }

        /// <summary>
        /// Конструктор, который сразу заполняет список
        /// </summary>
        public LayersExampleModel()
        {
            List1 = new List<Item>() 
            {
                new Item(){ItemName = "Item1", ItemAmount = 1, CreationDate=DateTime.Now},
                new Item(){ItemName = "Item2", ItemAmount = 2, CreationDate=DateTime.Now},
                new Item(){ItemName = "Item3", ItemAmount = 3, CreationDate=DateTime.Now}
            };
            List2 = new List<Item>();
        }

        /// <summary>
        /// Перемещает элемент из одного списка в другой. 
        /// Откуда куда перемещать - определяется по нахождению элемента в списке
        /// </summary>
        /// <param name="item">Элемент для перемещения</param>
        public void MoveItem(Item item)
        {
            if (IsItemInList(item, this.List1))
            {
                Int32 index = List1.IndexOf(item);
                MoveItemInLists(List1, List2, index);
            }
            else
            {
                if (IsItemInList(item, this.List2))
                {
                    Int32 index = List2.IndexOf(item);
                    MoveItemInLists(List2, List1, index);
                }
                else
                {
                    throw new ItemNotFoundInListException(item);
                }
            }
        }

        /// <summary>
        /// Находится ли данный элемент (итем) в указанном списке
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <param name="list">Список</param>
        /// <returns>Находится ли элемент в списке или нет</returns>
        protected Boolean IsItemInList(Item item, IList<Item> list)
        {
            return list.Any(x => x.Equals(item));
        }

        /// <summary>
        /// Перемещает элемент с указанным индексом из списка listSource в список listTarget
        /// </summary>
        /// <param name="listSource">Исходный список</param>
        /// <param name="listTarget">Целевой список</param>
        /// <param name="index">Индекс перемещаемого элемента</param>
        protected void MoveItemInLists(IList<Item> listSource, IList<Item> listTarget, Int32 index)
        {
            listTarget.Add(listSource[index]);
            listSource.RemoveAt(index);
        }
    }
}
