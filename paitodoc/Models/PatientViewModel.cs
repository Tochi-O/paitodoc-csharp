namespace paitodoc.Models;



public class PatientViewModel{
    public List<AppointmentModel> appointmentsList {get; set;}

    
    public List<RequestViewModel> requestsList{ get; set; }

    public List<UserViewModel> doctorsList{ get; set; }

    public RequestViewModel  aRequest { get; set; }
}