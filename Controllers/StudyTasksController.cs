using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Interfaces;

namespace project.Controllers;

[ApiController]
[Route("[controller]")]
public class StudyTasksController : ControllerBase
{
    private readonly IStudyTaskService _studyTaskService;

public StudyTasksController(IStudyTaskService studyTaskService)
{
    _studyTaskService = studyTaskService;
}

[HttpGet]
public IEnumerable<StudyTask> Get()
{
return _studyTaskService.GetAll();
}

[HttpGet("{id}")]
public ActionResult<StudyTask> Get(int id)
{
    var task = _studyTaskService.Get(id);
    if (task == null)
    {
        return NotFound();
    }
    return task;
}

[HttpPost]
public StudyTask Post(StudyTask newTask)
{
    _studyTaskService.Add(newTask);
    return newTask;
}

[HttpPut("{id}")]
public ActionResult<StudyTask> Put(int id, StudyTask updatedTask)
{
   if (id != updatedTask.Id)
    return BadRequest();

var task = _studyTaskService.Get(id);

if (task == null)
    return NotFound();

_studyTaskService.Update(updatedTask);

return NoContent();
}

[HttpDelete("{id}")]
public ActionResult Delete(int id)
{
   var task = _studyTaskService.Get(id);

if (task == null)
    return NotFound();

_studyTaskService.Delete(id);

return NoContent();
}
}