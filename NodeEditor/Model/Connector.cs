﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Model
{
    public class Connector
    {
        public int Id { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }

    }
}
