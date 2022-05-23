using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class DoctorHomeController : Controller
{
    private readonly ILogger<DoctorHomeController> _logger;

    public DoctorHomeController(ILogger<DoctorHomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult DoctorHome(string mail)
    {
        // var todoListViewlModel = GetAllTodos();
        // return View(todoListViewlModel);
        // return View();
        //UserViewModel data = TempData["mydata"] as UserViewModel;
        DoctorViewModel data2 = new DoctorViewModel();  
 

        //get all appointments
        data2.appointmentsList = GetAppointments(mail);

        //get all requests
        data2.requestsList = GetRequests(mail);


        //get all doctors
       // data2.doctorsList = GetDoctors();
  
        return View(data2);  
    }
    public List<AppointmentModel> GetAppointments(string email){
        List<AppointmentModel> appointments = new List<AppointmentModel>();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = $"SELECT * FROM appointment WHERE docemail = '{email}'";

                  using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                appointments.Add(
                                    new AppointmentModel
                                    {
                                        appId = reader.GetInt32(0),
                                        DocName = reader.GetString(1),
                                        DocEmail = reader.GetString(2),
                                        PatientName = reader.GetString(3),
                                        PatientEmail = reader.GetString(4),
                                        Date = reader.GetString(5),
                                        Time = reader.GetString(6),
                                    });
                            }
                        }
                        else
                        {
                            return appointments;
                        }
                    };
                }

            }
        return appointments;
    }

     public List<RequestViewModel> GetRequests(string email){
        List<RequestViewModel> requests = new List<RequestViewModel>();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = $"SELECT * FROM request WHERE docemail='{email}'";

                  using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                requests.Add(
                                    new RequestViewModel
                                    {
                                        reqId = reader.GetInt32(0),
                                        DocName = reader.GetString(1),
                                        DocEmail = reader.GetString(2),
                                        PatientName = reader.GetString(3),
                                        PatientEmail = reader.GetString(4),
                                        Date = reader.GetString(5),
                                        Time = reader.GetString(6),
                                    });

                            }
                        }
                        else
                        {
                            return requests;
                        }
                    };
                }

            }
        return requests;
    }

    //acceptreq
    public JsonResult AcceptRequest(int id)
    {
        UserViewModel usrData = new UserViewModel();
        RequestViewModel request = new RequestViewModel();
        AppointmentModel appointment = new AppointmentModel();
        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
             //get request
            // make appointment
            //add appointment to db
            //delete request
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"SELECT * from request WHERE reqId = '{id}'";
                    Console.WriteLine("request id "+id);
                    //tableCmd.ExecuteNonQuery();
                     using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            request.reqId = reader.GetInt32(0);
                            request.DocName = reader.GetString(1);
                            request.DocEmail = reader.GetString(2);
                            request.PatientName = reader.GetString(3);
                            request.PatientEmail = reader.GetString(4);
                            request.Date = reader.GetString(5);
                            request.Time = reader.GetString(6);
                            Console.WriteLine("get req by id accept : "+request.reqId+" "+request.DocName+" "+request.DocEmail+" "+request.PatientName+" "+request.PatientEmail+" "+request.Date+" "+request.Time);


                        }
                        // else
                        // {
                        //     return req;
                        // }
                    };

                }
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT into appointment (docname, docemail,patientname,patientemail,date,time) VALUES ('{request.DocName}','{request.DocEmail}','{request.PatientName}','{request.PatientEmail}','{request.Date}','{request.Time}')";
                    tableCmd.ExecuteNonQuery();

                }

                 using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from request WHERE reqId = '{id}'";
                    tableCmd.ExecuteNonQuery();

                }
                // using (var tableCmd1 = con.CreateCommand())
                // {
                //     con.Open();
                //     tableCmd1.CommandText = $"SELECT * FROM user WHERE email='{id.PatientEmail}'";
                   
                //     using (var reader = tableCmd1.ExecuteReader())
                //     {
                //         if (reader.HasRows)
                //         {
                //             reader.Read();
                //             usrData.Id = reader.GetInt32(0);
                //             usrData.Name = reader.GetString(1);
                //             usrData.Email = reader.GetString(2);
                //             usrData.Password = reader.GetString(3);
                //             usrData.Type = reader.GetString(4);
                //             //u=checkUsr.Type;
                //             //UserViewModel data = TempData["mydata"] as UserViewModel; 
                //             //TempData["mydata"] = usrData;
                //             // return RedirectToAction("DoctorHome", "DoctorHome");
                //             return RedirectToAction("DoctorHome", "DoctorHome",new { user =usrData.Email });

                //        }
                //         else
                //         {
                //           // TempData["mydata"] = usrData;
                //            return RedirectToAction("DoctorHome", "DoctorHome", new{user = usrData.Email});
                //         }
                //     };
                // }
                
         }
         //get docuser 
         //return redirect to doctor home page
        return Json(new {});
    }

public JsonResult DeleteApp(int id)
    {
        UserViewModel usrData = new UserViewModel();
        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
             using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from appointment WHERE appId = '{id}'";
                    tableCmd.ExecuteNonQuery();

                }
                //  using (var tableCmd1 = con.CreateCommand())
                // {
                //     con.Open();
                //     tableCmd1.CommandText = $"SELECT 1 FROM user WHERE email='{id.PatientEmail}'";
                   
                //     using (var reader = tableCmd1.ExecuteReader())
                //     {
                //         if (reader.HasRows)
                //         {
                //             reader.Read();
                //             usrData.Id = reader.GetInt32(0);
                //             usrData.Name = reader.GetString(1);
                //             usrData.Email = reader.GetString(2);
                //             usrData.Password = reader.GetString(3);
                //             usrData.Type = reader.GetString(4);
                //             //u=checkUsr.Type;
                //             //UserViewModel data = TempData["mydata"] as UserViewModel; 
                //             //TempData["mydata"] = usrData;
                //            // return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });
                             
                //        }
                //         else
                //         {
                //            //TempData["mydata"] = usrData;
                //            // return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });

                //         }
                //     };
                // }
         }
         //get docuser 
         //return redirect to doctor home page
         return Json(new {});
    }



     //acceptreq
    public ActionResult AcceptAppointment(int id)
    {
        //UserViewModel usrData = new UserViewModel();
        RequestViewModel request = new RequestViewModel();
        AppointmentModel appointment = new AppointmentModel();
        //var id  = request.reqId;

        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
             Console.WriteLine("request id "+id);
             Console.WriteLine("get req by id accept appointment: "+request.reqId+" "+request.DocName+" "+request.DocEmail+" "+request.PatientName+" "+request.PatientEmail+" "+request.Date+" "+request.Time);

             //get request
            // make appointment
            //add appointment to db
            //delete request
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"SELECT * from request WHERE reqId = '{id}'";
                    Console.WriteLine("request id "+id);
                    Console.WriteLine("get req by id accept : "+request.reqId+" "+request.DocName+" "+request.DocEmail+" "+request.PatientName+" "+request.PatientEmail+" "+request.Date+" "+request.Time);

                    //tableCmd.ExecuteNonQuery();
                     using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            request.reqId = reader.GetInt32(0);
                            request.DocName = reader.GetString(1);
                            request.DocEmail = reader.GetString(2);
                            request.PatientName = reader.GetString(3);
                            request.PatientEmail = reader.GetString(4);
                            request.Date = reader.GetString(5);
                            request.Time = reader.GetString(6);
                            Console.WriteLine("get req by id accept : "+request.reqId+" "+request.DocName+" "+request.DocEmail+" "+request.PatientName+" "+request.PatientEmail+" "+request.Date+" "+request.Time);


                        }
                        // else
                        // {
                        //     return req;
                        // }
                    };

                }
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT into appointment (docname, docemail,patientname,patientemail,date,time) VALUES ('{request.DocName}','{request.DocEmail}','{request.PatientName}','{request.PatientEmail}','{request.Date}','{request.Time}')";
                    tableCmd.ExecuteNonQuery();

                }

                 using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from request WHERE reqId = '{id}'";
                    tableCmd.ExecuteNonQuery();

                }
                
                return RedirectToAction("DoctorHome", "DoctorHome",new { mail =request.DocEmail });

                
         }

    }

}