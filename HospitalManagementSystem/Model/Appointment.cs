namespace HospitalManagementSystem
{
    public class Appointment
    {
        public string DoctorID { get; set; }
        public string PatientID { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            // Retrieve the doctor object associated with the appointment
            Doctor doctor = TxtHandler.GetDoctorDetails(this.DoctorID);

            // Retrieve the patient object associated with the appointment
            Patient patient = TxtHandler.GetPatientDetails(this.PatientID);

            // Combining first and last name for full name for the doctor
            string doctorFullName = doctor.FirstName + " " + doctor.LastName;

            // Combining first and last name for full name for the patient
            string patientFullName = patient.FirstName + " " + patient.LastName;

            // Combining different parts of an address for a full address for the doctor
            string address = doctor.StreetNumber + " " + doctor.Street + ", " + doctor.City + ", " + doctor.State;

            // Returning the appointment data formatted with the correct padding
            return $"{Helper.Padding(doctorFullName, 20)}| {Helper.Padding(patientFullName, 20)}| {Helper.Padding(this.Description, 30)}";
        }

    }
}