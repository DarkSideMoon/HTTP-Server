using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Composite
{
    // The complex object that contain the MainPages and Pages
    public class Composite : MenuElement
    {
        private List<MenuElement> _elements = new List<MenuElement>();

        //private ArrayList _elements = new ArrayList();

        public Composite(string name)
            : base(name)
        {
        }

        public override void Add(MenuElement element)
        {
            _elements.Add(element);
        }

        public override void Remove(MenuElement element)
        {
            _elements.Remove(element);
        }

        public override void Display(int depth)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', depth) + this.Name + " [" + this.Id + "]");
            Console.ForegroundColor = ConsoleColor.White;
            //_elements.ForEach(element =>
            //{
            //    element.Display(depth + 2);
            //});

            foreach (MenuElement element in _elements)
                element.Display(depth + 2);
        }

        public override void Build()
        {
        }

        public override MenuElement GetChild(Guid child)
        {
            return _elements.Find(element => element.Id == child);
        }

        public override MenuElement GetChild(string name)
        {
            return _elements.Find(element => element.Name == name);
        }


        public override bool IsExist(Guid child)
        {
            return _elements.Exists(x => x.Id == child);
        }
    }
}
