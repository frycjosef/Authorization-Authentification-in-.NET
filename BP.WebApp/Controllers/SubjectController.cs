using BP.WebApp.Data.Repositories.Interfaces;
using BP.WebApp.Keycloak;
using BP.Common.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.WebApp.Controllers;

[Authorize]
[Route("subjects")]
public class SubjectController : BaseController
{
    private readonly IRepositoryManager _repositoryManager;

    public SubjectController(IConfiguration configuration, KeycloakService service,
        IRepositoryManager repositoryManager) : base(configuration, service)
    {
        _repositoryManager = repositoryManager;
    }

    [HttpGet]
    public IActionResult List()
    {
        if (!KeycloakService.Authorize("ReadSubjects"))
            return RedirectToAction("AccessDenied", "Account");

        var subjects = _repositoryManager.Subjects.ListSubjects();

        return View(subjects);
    }
    
    [HttpGet]
    [Route("create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult Detail(int id)
    {
        if (!KeycloakService.Authorize("ReadSubjects"))
            return RedirectToAction("AccessDenied", "Account");

        return View(_repositoryManager.Subjects.LoadById(id));
    }

    [HttpPost]
    [Route("delete/{id}")]
    public IActionResult DeleteSubject(int id)
    {
        if (!KeycloakService.Authorize("DeleteSubjects"))
            return RedirectToAction("AccessDenied", "Account");

        _repositoryManager.Subjects.Delete(_repositoryManager.Subjects.LoadById(id));

        return RedirectToAction("List");
    }

    [HttpPost]
    [Route("update/{id}")]
    public IActionResult Update(Subject subject, int id)
    {
        if (!KeycloakService.Authorize("EditSubjects.*"))
            return RedirectToAction("AccessDenied", "Account");

        var dbSubject = _repositoryManager.Subjects.LoadById(id);
        
        dbSubject.Name = subject.Name ?? dbSubject.Name;
        dbSubject.Description = subject.Description ?? dbSubject.Description;

        _repositoryManager.Subjects.Attach(dbSubject);
        _repositoryManager.Subjects.SaveChanges();

        return RedirectToAction("Detail", new {id = dbSubject.Id});
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateSubject(Subject subject)
    {
        if (!KeycloakService.Authorize("CreateSubjects.*"))
            return RedirectToAction("AccessDenied", "Account");

        var dbSubject = new Subject()
        {
            Name = subject.Name,
            Description = subject.Description
        };
        
        _repositoryManager.Subjects.Add(dbSubject);
        _repositoryManager.Subjects.SaveChanges();

        return RedirectToAction("Detail", new {id = dbSubject.Id});
    }

}
