using project.Models;

namespace project.Interfaces;

public interface IStudyTaskService
{
    List<StudyTask> GetAll();
    StudyTask Get(int id);
    void Add(StudyTask task);
    void Delete(int id);
    void Update(StudyTask task);
    int Count { get; }
}
