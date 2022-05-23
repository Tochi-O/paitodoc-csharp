using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class PatientHomeController : Controller
{
    private readonly ILogger<PatientHomeController> _logger;

    public PatientHomeController(ILogger<PatientHomeController> logger)
    {
        _logger = logger;
    }

    
    public IActionResult PatientHome(string mail)
    {
        // var todoListViewlModel = GetAllTodos();
        // return View(todoListViewlModel);
       //  return View();
        //UserViewModel data = TempData["mydata"] as UserViewModel; 
        PatientViewModel data2 = new PatientViewModel();  
        Console.WriteLine("patients home email "+mail);


        //get all appointments
        data2.appointmentsList = GetAppointments(mail);

        //get all requests
        data2.requestsList = GetRequests(mail);


        //get all doctors
        data2.doctorsList = GetDoctors();


        return View(data2);  
    }

    public List<AppointmentModel> GetAppointments(string email){
        List<AppointmentModel> appointments = new List<AppointmentModel>();
                Console.WriteLine("patients appointmen home email "+email);

        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = $"SELECT * FROM appointment WHERE patientemail = '{email}'";

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
        Console.WriteLine("list of appointments"+appointments.Count);    
        return appointments;
    }

     public List<RequestViewModel> GetRequests(string email){
        List<RequestViewModel> requests = new ();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = $"SELECT * FROM request Where patientemail = '{email}'";
                Console.WriteLine("get requests email "+email);
                  using (var reader = tableCmd.ExecuteReader())
                    {
                         if (reader.HasRows)
                         {
                         Console.WriteLine("length of requests b "+requests.Count);    

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
                                    Console.WriteLine("length of requests a "+requests.Count);    

                            }
                         }
                        // else
                        // {
                        //     return requests;
                        // }
                    };
                }

            }
        Console.WriteLine("length of requests "+requests.Count);    
        return requests;
    }

     public List<UserViewModel> GetDoctors(){
        List<UserViewModel> doctors = new List<UserViewModel>();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = "SELECT * FROM user WHERE type='Doctor'";

                  using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                doctors.Add(
                                    new UserViewModel
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        Email = reader.GetString(2),
                                        Type = reader.GetString(4),
                                    });
                            }
                        }
                        else
                        {
                            return doctors;
                        }
                    };
                }

            }
        return doctors;
    }



    public ActionResult RequestAppointment(PatientViewModel parequest){
        UserViewModel usrData = new UserViewModel();
        RequestViewModel request = new RequestViewModel();
        request = parequest.aRequest;
        Console.WriteLine("name: "+request.DocName+" email: "+request.DocEmail+"name: "+request.PatientName+" email: "+request.PatientEmail+" date: "+request.Date+" type: "+request.Time);

         using (SqliteConnection con= new SqliteConnection("Data Source=db.sqlite"))
        { SQLitePCL.Batteries.Init();
            using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                tableCmd.CommandText = $"INSERT INTO request (docname,docemail,patientname,patientemail,date,time) VALUES ('{request.DocName}','{request.DocEmail}','{request.PatientName}','{request.PatientEmail}','{request.Date}','{request.Time}')";
                   Console.WriteLine("name: "+request.DocName+" email: "+request.DocEmail+"name: "+request.PatientName+" email: "+request.PatientEmail+" date: "+request.Date+" type: "+request.Time);

                    try{
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){
                        Console.WriteLine(ex.Message);
                    }

                   
                }
                using (var tableCmd1 = con.CreateCommand())
                {
                    con.Open();
                    tableCmd1.CommandText = $"SELECT * FROM user WHERE email='{request.PatientEmail}'";
                   
                    using (var reader = tableCmd1.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            usrData.Id = reader.GetInt32(0);
                            usrData.Name = reader.GetString(1);
                            usrData.Email = reader.GetString(2);
                            usrData.Password = reader.GetString(3);
                            usrData.Type = reader.GetString(4);
                            //u=checkUsr.Type;
                            //UserViewModel data = TempData["mydata"] as UserViewModel; 
                            // TempData["mydata"] = usrData;
                            //  return RedirectToAction("PatientHome", "PatientHome");
                            return RedirectToAction("PatientHome", "PatientHome",new {  mail =request.PatientEmail });

                       }
                        else
                        {
                        //    TempData["mydata"] = usrData;
                        //    return RedirectToAction("PatientHome", "PatientHome");
                              return RedirectToAction("PatientHome", "PatientHome",new { mail =request.PatientEmail });

                        }
                    };
                }
                
        }
        
        
        
       // UserViewModel data = TempData["mydata"] as UserViewModel; 
        // TempData["mydata"] = usrData;
        // return RedirectToAction("PatientHome", "PatientHome");

    }
    public ActionResult EditRequest(PatientViewModel patReq){
        UserViewModel usrData = new UserViewModel();
        RequestViewModel req = patReq.aRequest;
        using (SqliteConnection con =
                   new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"UPDATE request SET docname = '{req.DocName}', docemail = '{req.DocEmail}',patientname = '{req.PatientName}', patientemail = '{req.PatientEmail}', date = '{req.Date}', time = '{req.Time}' WHERE Id = '{req.reqId}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return RedirectToAction("PatientHome", "PatientHome",new { mail = req.PatientEmail});

                }
                // using (var tableCmd1 = con.CreateCommand())
                // {
                //     con.Open();
                //     tableCmd1.CommandText = $"SELECT * FROM user WHERE email = '{req.PatientEmail}' ";
                   
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
                //             // TempData["mydata"] = usrData;
                //             //  return RedirectToAction("PatientHome", "PatientHome");
                //             return RedirectToAction("PatientHome", "PatientHome",new { mail = req.PatientEmail});

                //        }
                //         else
                //         {
                //            //TempData["mydata"] = usrData;
                //            //return RedirectToAction("PatientHome", "PatientHome");
                //             return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });

                //         }
                //     };
                // }
                
            }

        
        
        
        // //UserViewModel data = TempData["mydata"] as UserViewModel; 
        // TempData["mydata"] = usrData;
        // return RedirectToAction("PatientHome", "PatientHome");

    }

    [HttpGet]
    public JsonResult PopulateRequestForm(int id)
    {
        var req = GetRequestById(id);
        Console.WriteLine("get req by id 222: "+req.reqId+" "+req.DocName+" "+req.DocEmail+" "+req.PatientName+" "+req.PatientEmail+" "+req.Date+" "+req.Time);

        return Json(req);
    }
    internal  RequestViewModel GetRequestById(int id)
    {
            RequestViewModel req = new();

            using (var connection =
                   new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM request Where reqId = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            req.reqId = reader.GetInt32(0);
                            req.DocName = reader.GetString(1);
                            req.DocEmail = reader.GetString(2);
                            req.PatientName = reader.GetString(3);
                            req.PatientEmail = reader.GetString(4);
                            req.Date = reader.GetString(5);
                            req.Time = reader.GetString(6);
                            Console.WriteLine("get req by id: "+req.reqId+" "+req.DocName+" "+req.DocEmail+" "+req.PatientName+" "+req.PatientEmail+" "+req.Date+" "+req.Time);
                        }
                        else
                        {
                            return req;
                        }
                    };
                }
            }

            return req;
        }


    // public JsonResult Delete(int id)
    // {
    //     using(SqliteConnection con = 
    //      new SqliteConnection("Data Source=db.sqlite"))
    //      {
    //          using (var tableCmd = con.CreateCommand())
    //             {
    //                 con.Open();
    //                 tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
    //                 tableCmd.ExecuteNonQuery();

    //             }
    //      }
    //      return Json(new {});
    // }
    
    public IActionResult DeleteRequest(int rid, string email)
    {
        var id =rid;
        Console.WriteLine("del id"+id);
        UserViewModel usrData = new UserViewModel();
        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
             using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from request WHERE reqId = '{id}'";
                    Console.WriteLine("del id"+id);
                    tableCmd.ExecuteNonQuery();

                }
                //  using (var tableCmd1 = con.CreateCommand())
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
                //            // TempData["mydata"] = usrData;
                //              //return RedirectToAction("PatientHome", "PatientHome");
                //           //  return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });

                //        }
                //         else
                //         {
                //            //TempData["mydata"] = usrData;
                //           // return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });
                //         }
                //     };
                // }
         }
         //get docuser 
         //return redirect to doctor home page
        // return Json(new {});
        //Response.Redirect(HttpCoRequest.RawUrl);
        return RedirectToAction("PatientHome", "PatientHome",new { mail = email });    

    }
    // public ActionResult DeleteRequestApp(int id){

        
        
        
        
    //     UserViewModel data = TempData["mydata"] as UserViewModel; 
    //     TempData["mydata"] = data;
    //     return RedirectToAction("PatientHome", "PatientHome");

    // }
    // public ActionResult DeleteApp(int id){

        
        
        
        
    //     UserViewModel data = TempData["mydata"] as UserViewModel; 
    //     TempData["mydata"] = data;
    //     return RedirectToAction("PatientHome", "PatientHome");

    // }


}