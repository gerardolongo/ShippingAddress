﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneIndirizzi.ExceptionCustom
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message)
        : base(message)
        {
        }
    }
}
