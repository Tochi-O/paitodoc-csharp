using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        // var todoListViewlModel = GetAllTodos();
        // return View(todoListViewlModel);
         return View();
    }

    
    public ActionResult LoginUser(UserViewModel usr)
    {

        var usertype="";
        UserViewModel checkUsr = new UserViewModel();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        { SQLitePCL.Batteries.Init();
            using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"SELECT * FROM user WHERE email='{usr.Email}'";
                   
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                           
                            reader.Read();   
                            checkUsr.Id = reader.GetInt32(0);
                            checkUsr.Name = reader.GetString(1);
                            checkUsr.Email = reader.GetString(2);
                            checkUsr.Password = reader.GetString(3);
                            checkUsr.Type = reader.GetString(4);
                            usertype=checkUsr.Type;
                       
                            Console.WriteLine("Id: "+checkUsr.Id);
                            Console.WriteLine("name: "+checkUsr.Name+" email: "+checkUsr.Email+" password: "+checkUsr.Password+" type: "+checkUsr.Type);


                         
                        }
                        else
                        {
                           return RedirectToAction("Login", "Login");
                        }
                    };
                    // try{
                    //     tableCmd.ExecuteNonQuery();
                    // }
                    // catch (Exception ex){
                    //     Console.WriteLine(ex.Message);
                    // }

                    // using (var reader = tableCmd.ExecuteReader())
                    // {
                    //     if (reader.HasRows)
                    //     {
                    //         while (reader.Read())
                    //         {
                    //             todoList.Add(
                    //                 new TodoItem
                    //                 {
                    //                     Id = reader.GetInt32(0),
                    //                     Name = reader.GetString(1)
                    //                 });
                    //         }
                    //     }
                    //     else
                    //     {
                    //         return new TodoViewModel
                    //         {
                    //             TodoList = todoList
                    //         };
                    //     }
                    // };
                }
                
        }
        //return Redirect("https://localhost:7229/");
        if(usr.Password.Equals(checkUsr.Password)){
            UserViewModel data = checkUsr;
        //new Customer() {  
        // CustomerID = 1, CustomerName = "Abcd", Country = "PAK"  
        // };  
        //  TempData["mydata"] = data;  
         Console.WriteLine("email login "+checkUsr.Email);

         var rdrTO= usertype+"Home";
       // return RedirectToAction(rdrTO, rdrTO);
        return RedirectToAction(rdrTO, rdrTO,new { mail =checkUsr.Email });
        }else{
            return RedirectToAction("Login", "Login");

        }
          



    }


}