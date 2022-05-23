using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class SignupController : Controller
{
    private readonly ILogger<SignupController> _logger;

    public SignupController(ILogger<SignupController> logger)
    {
        _logger = logger;
    }

    public IActionResult Signup()
    {
        // var todoListViewlModel = GetAllTodos();
        // return View(todoListViewlModel);
         return View();
    }

    public ActionResult SignupUser(UserViewModel usr)
    {
        var usertype="";
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        { SQLitePCL.Batteries.Init();
            
                    con.Open();
                    Console.WriteLine(usr.Name);
                    Console.WriteLine(usr.Email);
                    Console.WriteLine(usr.Password);
                    Console.WriteLine(usr.Type);

                 using (var tableCmd1 = con.CreateCommand())
                 {
                    con.Open();
                    tableCmd1.CommandText = $"SELECT * from user WHERE email = '{usr.Email}'";
                    //tableCmd1.ExecuteNonQuery();
                        using (var reader = tableCmd1.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {

                                using (var tableCmd = con.CreateCommand())
                                {
                                    tableCmd.CommandText = $"INSERT INTO user (name, email, password, type) VALUES ('{usr.Name}', '{usr.Email}', '{ usr.Password}', '{usr.Type}')";
                                    try{
                                        tableCmd.ExecuteNonQuery();
                                        //UserViewModel data = usr;
                                        // new UserViewModel() {  
                                        // u = 1, CustomerName = "Abcd", Country = "PAK"  
                                        // };  
                                        // TempData["mydata"] = data;  
                                        
                                    }
                                    catch (Exception ex){
                                        Console.WriteLine(ex.Message);
                                    }
                                    var rdrTO= usr.Type+"Home";
                                        //return RedirectToAction(rdrTO, rdrTO);  
                                    return RedirectToAction(rdrTO, rdrTO,new { mail =usr.Email });

                                }
                            }else{
                                return RedirectToAction("Signup", "Signup");

                            }

                    }


                    

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
        



    }

}