using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ProductType
    {
        public ProductType()
        {
        }

        public int ProductTypeId { get; set; }
        public string Name { get; set; } = null!;


    }
}
