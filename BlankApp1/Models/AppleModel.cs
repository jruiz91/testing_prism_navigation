using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Models
{
    public class AppleModel
    {
        private Nullable<int> _id;
        private string _name;
        private string _colour;

        public Nullable<int> ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }
        public AppleModel Clone()
        {
            return new AppleModel()
            {
                ID = this.ID,
                Colour = this.Colour,
                Name = this.Name
            };
        }
    }
}
