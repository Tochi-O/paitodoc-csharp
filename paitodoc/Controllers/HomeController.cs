using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using paitodoc.Models;
using Microsoft.Data.Sqlite;
using paitodoc.Models.ViewModels;
namespace paitodoc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var todoListViewlModel = GetAllTodos();
        return View(todoListViewlModel);
        // return View();
    }
[HttpGet]
    public JsonResult PopulateForm(int id)
    {
        var todo = GetById(id);
        return Json(todo);
    }
    internal TodoItem GetById(int id)
    {
            TodoItem todo = new();

            using (var connection =
                   new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM todo Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            todo.Id = reader.GetInt32(0);
                            todo.Name = reader.GetString(1);
                        }
                        else
                        {
                            return todo;
                        }
                    };
                }
            }

            return todo;
        }
    internal TodoViewModel GetAllTodos()
    {
        List<TodoItem> todoList = new();
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = "SELECT * FROM todo";

                  using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoList.Add(
                                    new TodoItem
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1)
                                    });
                            }
                        }
                        else
                        {
                            return new TodoViewModel
                            {
                                TodoList = todoList
                            };
                        }
                    };
                }

            }
             return new TodoViewModel
            {
                TodoList = todoList
            };
        }

    public RedirectResult Insert(TodoItem todo)
    {
        using (SqliteConnection con=
        new SqliteConnection("Data Source=db.sqlite"))
        { SQLitePCL.Batteries.Init();
            using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.Name}')";
                    try{
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){
                        Console.WriteLine(ex.Message);
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
        return Redirect("https://localhost:7229/");



    }

    public JsonResult Delete(int id)
    {
        using(SqliteConnection con = 
         new SqliteConnection("Data Source=db.sqlite"))
         {
             using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();

                }
         }
         return Json(new {});
    }

     public RedirectResult Update(TodoItem todo)
        {
            using (SqliteConnection con =
                   new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"UPDATE todo SET name = '{todo.Name}' WHERE Id = '{todo.Id}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return Redirect("https://localhost:7229/");
        }
    


//login view


//login btn


//register view

//register btn


//patient home page

//patient view requests
//pateint view appointments
//update request
//del request
//view doctors list
//add new request

//doctor home page
//view requests
//accept request
//view appointments
//cancel appointments


    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
