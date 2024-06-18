﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities.ConfigurationData
{
    /// <summary>
    /// Clase abstracta que define el tipo de las clases Building, Floor y Room
    /// </summary>
     public abstract class Structure : Entity
    {
        /// <summary>
        /// Constructor por defecto de Structure
        /// </summary>
        public Structure() 
        {
        }

        /// <summary>
        /// Constructor de la clase Structure
        /// </summary>
        /// <param name="id"></param>
        public Structure (Guid id) : base(id)
        {

        }
    }
}