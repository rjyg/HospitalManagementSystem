namespace HospitalManagementSystem
{
    public class Doctor : Person
    {
        public string DoctorID
        {
            get { return ID; }
            set { ID = value; }
        }

        public override string ToString()
        {
            // Combining first and last name for full name
            string doctorFullName = this.FirstName + " " + this.LastName;

            // Combining different parts of an address for a full address
            string address = this.StreetNumber + " " + this.Street + ", " + this.City + ", " + this.State;

            // Returning the doctor data formatted with the correct padding
            return $"{Helper.Padding(doctorFullName, 20)}| {Helper.Padding(this.Email, 25)}| {Helper.Padding(this.Phone, 15)}| {address}";
        }
    }
}

