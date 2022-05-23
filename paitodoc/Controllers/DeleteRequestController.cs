using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class DeleteRequestController : Controller
{
    private readonly ILogger<DeleteRequestController> _logger;

    public DeleteRequestController(ILogger<DeleteRequestController> logger)
    {
        _logger = logger;
    }

    



public IActionResult DeleteRequest(int reqid)
{
        var id =reqid;
        Console.WriteLine("del id"+id);
        var email ="";
      //  Console.WriteLine("del email"+request.PatientEmail);
        RequestViewModel request = new();
        //UserViewModel usrData = new UserViewModel();
        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
               using (var tableCmd1 = con.CreateCommand())
                {
                    con.Open();
                    tableCmd1.CommandText = $"SELECT * FROM request WHERE reqId ='{id}'";
                   
                    using (var reader = tableCmd1.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            request.reqId = reader.GetInt32(0);
                            // usrData.Name = reader.GetString(1);
                            // usrData.Email = reader.GetString(2);
                            // usrData.Password = reader.GetString(3);
                            request.PatientEmail = reader.GetString(4);
                            email=request.PatientEmail;
                            //u=checkUsr.Type;
                            //UserViewModel data = TempData["mydata"] as UserViewModel; 
                           // TempData["mydata"] = usrData;
                             //return RedirectToAction("PatientHome", "PatientHome");
                          //  return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });

                       }
                        else
                        {
                           //TempData["mydata"] = usrData;
                          // return RedirectToAction("PatientHome", "PatientHome",new { user =usrData });
                        }
                    };
                }
             using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from request WHERE reqId = '{id}'";
                    Console.WriteLine("del id"+id);
                    tableCmd.ExecuteNonQuery();
                    return RedirectToAction("PatientHome", "PatientHome",new { mail = email });    


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

    }


}