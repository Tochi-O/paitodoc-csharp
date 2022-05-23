namespace paitodoc.Models;

public class AppointmentModel
{
    public int appId{get; set;}
    public string DocName { get; set; }

    public string DocEmail { get; set; }


    public string PatientName { get; set; }


    public string PatientEmail { get; set; }


    public string Date { get; set; }

    public string Time { get; set; }

}