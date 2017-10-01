using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedDevTeamNames.Models
{
    public class Product
    {
        public int Id
        {
            get { return Id; } 
            set { Id = value; }
        }

        public string Name
        {
            get { return Name; }
            set { Name = value; }
        }

        public string Category
        {
            get { return Category; }
            set { Category = value; }
        }

        public decimal Price
        {
            get { return Price; }
            set { Price = value; }
        }
    }

}