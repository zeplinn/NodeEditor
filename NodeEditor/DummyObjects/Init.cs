using NodeEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DummyObjects
{
    public static class Init
    {
        public static IEnumerable<Node> InitNodes(int count)
        {
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                yield return new Node() { Id = i, X = rnd.Next(0, 200), Y = rnd.Next(0, 200) };
            }
        }
    }
}
