using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Status.Models.Domain
{
    public class Status : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
