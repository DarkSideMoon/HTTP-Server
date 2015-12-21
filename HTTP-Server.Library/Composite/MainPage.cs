using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Composite
{
    // The main pages that contains simple pages
    public class MainPage : MenuElement
    {
        public MainPage(string name)
            : base(name)
        {
        }

        public override void Add(MenuElement menu)
        {
            Console.WriteLine("Cannot add to Main Page");
        }

        public override void Build()
        {
            Console.WriteLine("Name: " + this._name + "\tId: " + this._id);
        }

        public override MenuElement GetChild(Guid child)
        {
            return null;
        }

        public override MenuElement GetChild(string name)
        {
            return null;
        }

        public override void Remove(MenuElement menu)
        {
            Console.WriteLine("Cannot remove from a Main Page");
        }

        public override bool IsExist(Guid child)
        {
            return false;
        }

        public override void Display(int depth)
        {
            base.Display(depth);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
