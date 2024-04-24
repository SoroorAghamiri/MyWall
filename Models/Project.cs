namespace MyWebApplication.Models;
public class Project{
    private string name;
    public enum Type{
        Game,
        Website,
        Protocol
    }
    private Type projectType;
    private string programmingLang;
    private DateOnly projectDate;
    private string url;
    public Project(string name, Type type, string programmingLang, DateOnly date, string url){
        this.name = name;
        this.projectType = type;
        this.projectDate = date;
        this.programmingLang = programmingLang;
        this.url = url;
    }

    public string GetName(){
        return this.name;
    }

    public Type GetType(){
        return this.projectType;
    }

    public string GetProgrammingLang(){
        return this.programmingLang;
    }

    public DateOnly GetProjectDate(){
        return this.projectDate;
    }
    public string GetUrl(){
        return this.url;
    }
}