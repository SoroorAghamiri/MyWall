using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;
using System.Linq;
using System.Text.Encodings.Web;


namespace MyWebApplication.Controllers;
public class SoftwarePrjs : Controller{
    private List<Project> projects = new List<Project>();
    public IActionResult Index(){
        projects = GenerateProjectCards();
        ViewData["Projects"] = projects;
        return View("~/Views/Home/SoftwarePrjs.cshtml");
    }
    // public IActionResult ShowCards(){
    //     return View(GenerateProjectCards());
    // }

    public IActionResult Sort(string requestedMode){
        projects = GenerateProjectCards();
        PrintList(projects);
        // Convert string to sortmode enum
        if (Enum.TryParse(requestedMode, true, out SortModes receivedMode))
        {
            // Parsing succeeded, receivedMode contains the parsed enum value
            Console.WriteLine("Sort mode: " + receivedMode.ToString());
            switch(receivedMode){
                case SortModes.Alphabetical:
                    projects.Sort((x, y) => (x.GetName().CompareTo(y.GetName())));
                    PrintList(projects);
                break;
                case SortModes.Date:
                    PrintList(projects);
                    SortBasedOnDate(projects, 0, projects.Count - 1);
                break;
                case SortModes.ProgrammingLang:
                    projects.Sort((x, y)=> (x.GetProgrammingLang().CompareTo(y.GetProgrammingLang())));
                break;
                case SortModes.Type:
                    projects.Sort((x, y)=> (x.GetType().CompareTo(y.GetType())));
                break;
            }
        }
        else
        {
            Console.WriteLine("Converting unsuccessful");
            // Parsing failed
        }
        
        ViewData["Projects"] = projects;
        return View("~/Views/Home/SoftwarePrjs.cshtml");
    }

    private void PrintList(List<Project> given){
        int n = given.Count; 
        for (int i = 0; i < n; ++i)
            Console.Write(given[i].GetName() + " ");
        Console.WriteLine("Empty Line");
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

    private void SortBasedOnDate(List<Project> unsorted, int l , int r){
        if(l < r){
            int m = l + (r - l) / 2;

            SortBasedOnDate(unsorted, l, m);
            SortBasedOnDate(unsorted, m+1, r);

            Console.Write(projects[0].GetName() + " ");

            Merge(unsorted, l , m , r);
        }
    }

    private void Merge(List<Project> list, int l, int m, int r){
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        List<Project> left = new List<Project>(new Project[n1]);
        List<Project> right = new List<Project>(new Project[n2]);
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            left[i] = list[l + i];
        for (j = 0; j < n2; ++j)
            right[j] = list[m + 1 + j];

        i = 0;
        j = 0;

        // Initial index of merged subarray array
        //Put Latest date first -> CompareTo > 0: left[i] is the latest date
        int k = l;
        while (i < n1 && j < n2) {
            if (left[i].GetProjectDate().CompareTo(right[j].GetProjectDate())>=0) {
                list[k] = left[i];
                i++;
            }
            else {
                list[k] = right[j];
                j++;
            }
            k++;
        }

        // Copy remaining elements
        // of L[] if any
        while (i < n1) {
            list[k] = left[i];
            i++;
            k++;
        }

        // Copy remaining elements
        // of R[] if any
        while (j < n2) {
            list[k] = right[j];
            j++;
            k++;
        }
    }
    
}