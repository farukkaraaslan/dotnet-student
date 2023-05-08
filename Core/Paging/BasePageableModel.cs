﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Paging;

public abstract class BasePageableModel
{
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}