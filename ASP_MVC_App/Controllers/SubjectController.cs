using ASP_MVC_App.Data.Repositories.Interfaces;
using ASP_MVC_App.Keycloak;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC_App.Controllers;

[Authorize]
[Route("subjects")]
public class SubjectController : BaseController
{
    private readonly IRepositoryManager _repositoryManager;
    
    public SubjectController(IConfiguration configuration, KeycloakService service, IRepositoryManager repositoryManager) : base(configuration, service)
    {
        _repositoryManager = repositoryManager;
    }
    
    [HttpGet]
    public IActionResult List()
    {
        if(!KeycloakService.Authorize("ReadSubjects"))
            return RedirectToAction("AccessDenied", "Account");

        var subjects = _repositoryManager.Subjects.ListSubjects();

        return View(subjects);
    }
}
