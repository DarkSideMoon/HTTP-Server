using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Composite
{
    public abstract class MenuElement
    {
        protected string _name;
        protected Guid _id;

        public MenuElement(string name)
        {
            this._id = Guid.NewGuid();
            this._name = name;
        }

        public Guid Id
        {
            get { return this._id; }
        }

        public string Name
        {
            get { return this._name; }
        }

        public abstract void Add(MenuElement menu);
        public abstract void Remove(MenuElement menu);
        public virtual void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + this.Name);
        }

        public abstract void Build();
        public abstract MenuElement GetChild(Guid child);
        public abstract MenuElement GetChild(string name);

        public abstract bool IsExist(Guid child);

        public override string ToString()
        {
            Console.WriteLine("Menu elements is: " + this.Name + " (" + this.Id + ")");
            return string.Format("Menu elements");
        }
    }
}
