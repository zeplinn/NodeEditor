using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Model
{
    public class Document
    {
        public ICollection<Node> Nodes { get; set; }
        public ICollection<Connector> Lines { get; set; }
    }
}
