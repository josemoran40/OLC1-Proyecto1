using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto1
{
    class Conjunto
    {
        private string id;
        private LinkedList<string> caracteres;

        public Conjunto(string id)
        {
            this.id = id;
            this.caracteres = new LinkedList<string>();
        }

        public string getId()
        {
            return id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public LinkedList<string> getCaracteres()
        {
            return caracteres;
        }

        public void setCaracteres(LinkedList<string> caracteres)
        {
            this.caracteres = caracteres;
        }
    }
}
