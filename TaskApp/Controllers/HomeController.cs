using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    public class HomeController : Controller
    {
        public static DummySystem System = new DummySystem();
        public static TaskInvoker Current_Tasks = new TaskInvoker();
        public static List<IUserTasks> AvailiableTasks = new List<IUserTasks>();
        public static Boolean AuxFlag = false; // false on startup

        public HomeController()
        {
            // Adds elements for availiable commands if no command is yet added.
            if(!AvailiableTasks.Any() && !AuxFlag)
            {
                AvailiableTasks.Add(new DefragmentHD(System));
                AvailiableTasks.Add(new DiskCleanUp(System));
            }
        }

        public IActionResult Index()
        {
            Current_Tasks.ExecuteTasks();
            TaskView TView = new TaskView(AvailiableTasks, Current_Tasks, System);
            return View(TView);
        }

        public IActionResult AddTask(string name)
        {
            var CurrentTask = AvailiableTasks.Find(x => x.TaskName == name);
            Current_Tasks.AddTask(CurrentTask);
            AvailiableTasks.Remove(CurrentTask);
            if(!AvailiableTasks.Any()){
                AuxFlag = true;
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
