using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;
using System.Linq;
using System.Text.Encodings.Web;


namespace MyWebApplication.Controllers;
public class SoftwarePrjs : Controller{

    public IActionResult Index(){
        List<Project> projects = GenerateProjectCards();
        ViewData["Projects"] = projects;
        return View("~/Views/Home/SoftwarePrjs.cshtml");
    }
    // public IActionResult ShowCards(){
    //     return View(GenerateProjectCards());
    // }

    public IActionResult Sort(string requestedMode){
        // Convert string to sortmode enum
        if (Enum.TryParse(requestedMode, true, out SortModes receivedMode))
        {
            // Parsing succeeded, receivedMode contains the parsed enum value
            Console.WriteLine("Sort mode: " + receivedMode.ToString());
        }
        else
        {
            Console.WriteLine("Converting unsuccessful");
            // Parsing failed
        }
        return View("~/Views/Home/SoftwarePrjs.cshtml");
    }

    public List<Project> GenerateProjectCards(){
        //Read text file line by line
        List<Project> myProjects = new List<Project>();
        try{
            var lines = System.IO.File.ReadLines("wwwroot/Texts/projects.txt");
            foreach (var line in lines){
                // Console.WriteLine(line);
                string[] allWords = line.Split(" ");
                
                Enum.TryParse(allWords[1], out Project.Type prjType);
                
                DateOnly prjDate = DateOnly.Parse(allWords[3]);
                
                Project newProj = new Project(allWords[0], prjType, allWords[2], prjDate, allWords[4]);
                
                myProjects.Add(newProj);
            }
            
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }  
        return myProjects;      
    }

    
}