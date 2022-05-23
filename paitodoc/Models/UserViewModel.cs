namespace paitodoc.Models;

public class UserViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }


    public string Password { get; set; }


    public string Type { get; set; }


    // public string Specialization {get; set; }


   // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}