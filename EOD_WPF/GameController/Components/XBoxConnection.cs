﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOD_WPF.GameController
{
    public class XBoxConnection : XBoxComponent<bool>
    {
        public XBoxConnection(bool initialValue = false) : base(initialValue) { }
    }
}
