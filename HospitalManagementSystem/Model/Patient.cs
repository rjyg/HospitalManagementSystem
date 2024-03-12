namespace HospitalManagementSystem
{
    public class Patient : Person
    {
        public string PatientID
        {
            get { return ID; }
            set { ID = value; }
        }
        public string DoctorID { get; set; }

        public override string ToString()
        {
            // Combining first and last name for full name
            string patientFullName = this.FirstName + " " + this.LastName;

            // Retrieving the doctor's whose name is associated with this patient
            string doctorName = TxtHandler.GetDoctorName(TxtHandler.GetDoctorForPatient(PatientID)) ?? "N/A";

            // Combining different parts of an address for a full address
            string address = this.StreetNumber + " " + this.Street + ", " + this.City + ", " + this.State;

            // Returning the patient data formatted with the correct padding
            return $"{Helper.Padding(patientFullName, 15)}| {Helper.Padding(doctorName, 20)}| {Helper.Padding(Email, 25)}| {Helper.Padding(Phone, 11)}| {address}";
        }
    }
}
