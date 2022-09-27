using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einführung.Models {
    public class Customer{

        private int _id;
        private DateTime _birthdate;
        private decimal _salary;
        private List<Address> _addresses;
        

        public int Id {
            get { return this._id; }
            set { 
                if(value >= 0) {
                    this._id = value;
                } }
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate {
            get { return this._birthdate; }
            set {
                if(value < DateTime.Now) {
                    this._birthdate = value;
                }
            }
        }

        public decimal Salary {
            get { return this._salary; }
            set {
                if(value >= 0.0m) {// m ==> double zu decimal
                    this._salary = value;
                }
            }
        }

        public Gender Gender { get; set; }

        public List<Address> Addresses {
            get { return this._addresses; }//nur get bei listenartigen strukturen, später add oder andere funktionen anbieten
        }

        //ctor's 

        public Customer() : this(0, "", "", DateTime.MinValue, 0.0m, Gender.notSpecified) { }
        public Customer(int id, string firstname, string lastname, DateTime birthdate, decimal salary, Gender gender) {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Birthdate = birthdate;
            this.Salary = salary;
            this.Gender = gender;
            this._addresses = new List<Address>();
        }

        public override string ToString() {
            return this.Id + " " + this.Firstname + " " + this.Lastname + "\n" + this.Birthdate.ToShortDateString() + " " + this.Gender + " " + this.Salary + " Euro";
        }
        //Bei Listen immer Add/Remove - Methoden verwenden (kein set)

        public void AdAddresses(Address address) {
            if(this._addresses == null) {
                this._addresses = new List<Address>();
            }
            this._addresses.Add(address);
        }

        public bool RemoveAddress(int id) {
            foreach(var a in this._addresses) {//var setzt Richtigen Datentyp
                if(a.Id == id) {
                    return this._addresses.Remove(a);
                }
            }
            return false;
        }

    }
}
