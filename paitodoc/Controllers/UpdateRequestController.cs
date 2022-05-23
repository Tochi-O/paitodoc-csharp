using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class UpdateRequestController : Controller
{
    private readonly ILogger<UpdateRequestController> _logger;

    public UpdateRequestController(ILogger<UpdateRequestController> logger)
    {
        _logger = logger;
    }

    
    public IActionResult UpdateRequest(int Id)
    {
        // var todoListViewlModel = GetAllTodos();
        // return View(todoListViewlModel);
       //  return View();
        //UserViewModel data = TempData["mydata"] as UserViewModel; 
        RequestViewModel data2 = new ();  
        Console.WriteLine("update request home email "+Id);

        data2 = GetRequestById(Id);


        ViewBag.reqId = data2.reqId;
        ViewBag.DocName = data2.DocName;
        ViewBag.DocEmail = data2.DocEmail;
        ViewBag.PatientName = data2.PatientName;
        ViewBag.PatientEmail = data2.PatientEmail;
        ViewBag.Date = data2.Date;
        ViewBag.Time = data2.Time;


        return View(data2);  
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


    public ActionResult EditRequest(RequestViewModel req){
        UserViewModel usrData = new UserViewModel();
       // RequestViewModel req = patReq.aRequest;
        Console.WriteLine("updated req by id: "+req.reqId+" "+req.DocName+" "+req.DocEmail+" "+req.PatientName+" "+req.PatientEmail+" "+req.Date+" "+req.Time);

        using (SqliteConnection con =
                   new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"UPDATE request SET docname = '{req.DocName}', docemail = '{req.DocEmail}',patientname = '{req.PatientName}', patientemail = '{req.PatientEmail}', date = '{req.Date}', time = '{req.Time}' WHERE reqId = '{req.reqId}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                        
    
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // using (var tableCmd1 = con.CreateCommand())
                // {
                //     con.Open();
                //     tableCmd1.CommandText = $"SELECT * FROM user WHERE email='{req.PatientEmail}'";
                   
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
                //             return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });

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

            return RedirectToAction("PatientHome", "PatientHome",new { mail =req.PatientEmail });

        
        
        // //UserViewModel data = TempData["mydata"] as UserViewModel; 
        // TempData["mydata"] = usrData;
        // return RedirectToAction("PatientHome", "PatientHome");

    }

}