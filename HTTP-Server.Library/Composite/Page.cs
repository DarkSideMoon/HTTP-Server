using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Composite
{
    // Simple page in web
    public class Page : MenuElement
    {
        public Page(string name)
            : base(name)
        {
        }

        public override void Add(MenuElement menu)
        {
            Console.WriteLine("Cannot add to Page");
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
            Console.WriteLine("Cannot remove from a Page");
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
