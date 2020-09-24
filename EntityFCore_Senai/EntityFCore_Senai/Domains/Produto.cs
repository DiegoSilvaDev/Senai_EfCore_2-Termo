﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFCore_Senai.Domains
{
    public class Produto : BaseDomain
    {
        /// <summary>
        /// Definimos a classe produto
        /// </summary>
            public string Nome { get; set; }
            public float Preco { get; set; }

    }
}
