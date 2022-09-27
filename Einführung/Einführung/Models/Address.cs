using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einführung.Models {
    public class Address {
        //id, street, streetnr, postalcode, city

        private int _id;

        public int Id {
            get { return this._id; }
            set {
                if(value >= 0) {
                    this._id = value;
                }
            }
        }

        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Streetnumber { get; set; }

        public Address() : this(0, "", "", "", "") { }
        public Address(int id, string postalcode, string city, string street, string streetnumber) {
            this.Id = id;
            this.PostalCode = postalcode;
            this.City = city;
            this.Street = street;
            this.Streetnumber = streetnumber;
        }

        public override string ToString() {
            return this.Id + " " + this.PostalCode + " " + this.City + " " + this.Street + " " + this.Streetnumber + " ";
        }
    }
}
