﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class ResultRequestEntity
    {
        public Object Data { set; get; }
        public Boolean Error { set; get; }
        public String Message { set; get; }
    }
}
