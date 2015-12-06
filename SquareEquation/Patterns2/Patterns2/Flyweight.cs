using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace Patterns2
{
   
    public class TreeUser
    {
        public List<Tree> Trees { get; set; }
        public Image TreeImage { get; set; }

        public TreeUser()
        {
            TreeImage = new Image();
            Trees = new List<Tree>();
            Trees.Add(new Tree(12, TreeImage));
            Trees.Add(new Tree(14, TreeImage));
            Trees.Add(new Tree(8, TreeImage));
        }
    }

    public class Image
    { }


    public class Tree
    {
        //Высота у всех деревьев разная
        public Int32 Height { get; set; }
        //Одинаковая для всех деревьев картинка
        public Image Image { get; set; }

        public Tree(Int32 height, Image image)
        {
            Height = height;
            Image = image;
        }
    }

}
