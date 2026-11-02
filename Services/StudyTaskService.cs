using Microsoft.AspNetCore.Hosting;
using project.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using project.Interfaces;

namespace project.Services;

public class StudyTaskService : IStudyTaskService
{
    private readonly List<StudyTask> tasks;
    private readonly string filePath;

    public StudyTaskService(IWebHostEnvironment webHost)
    {
        filePath = Path.Combine(
            webHost.ContentRootPath,
            "Data",
            "StudyTasks.json");

        var json = File.ReadAllText(filePath);

        tasks = JsonSerializer.Deserialize<List<StudyTask>>(json)
            ?? throw new InvalidOperationException(
                "StudyTasks.json must contain a JSON array.");
    }

    public List<StudyTask> GetAll()
    {
        return tasks;
    }

    public StudyTask Get(int id)
{
    return tasks.FirstOrDefault(task => task.Id == id);
}

private void SaveToFile()
{
    var json = JsonSerializer.Serialize(tasks);
    File.WriteAllText(filePath, json);
}

public void Add(StudyTask task)
{
    if (tasks.Count == 0)
        task.Id = 1;
    else
        task.Id = tasks.Max(t => t.Id) + 1;

    tasks.Add(task);
    SaveToFile();
}

public void Update(StudyTask task)
{
    var index = tasks.FindIndex(t => t.Id == task.Id);

    if (index == -1)
        return;

    tasks[index] = task;
    SaveToFile();
}

public void Delete(int id)
{
    var task = Get(id);

    if (task is null)
        return;

    tasks.Remove(task);
    SaveToFile();
}

public int Count => tasks.Count;

}