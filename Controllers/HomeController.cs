using front.Models;
using front.Models.CreateModels;
using front.Services;

namespace front.Controllers;

public class HomeController : Controller
{
    private readonly CreateService _createService;
    private readonly ProjectService _projectService;
    private readonly BlogService _blogService;
    private readonly ContactService _contactService;
    private readonly ServiceService _serviceService;
    private readonly MainContentService _mainContentService;

    public HomeController(
        CreateService createService,
        ProjectService projectService, 
        BlogService blogService, 
        ContactService contactService,
        ServiceService serviceService,
        MainContentService mainContentService) 
        =>
        (_createService, _projectService, _blogService, _contactService, _serviceService, _mainContentService) 
        =
        (createService, projectService, blogService, contactService, serviceService, mainContentService);


    public async Task<IActionResult> Index()
    {
        await SetRandomContent();

        return View();
    }

    public async Task<IActionResult> Projects()
    {
        await SetRandomContent();
        var projects = await _projectService.GetAllProjectsAsync();
        return View(projects);
    }

    public async Task<IActionResult> ProjectDetails(int id)
    {
        await SetRandomContent();
        var project = await _projectService.GetProjectByIdAsync(id);
        return View(project);
    }

    public async Task<IActionResult> Services()
    {
        await SetRandomContent();
        var services = await _serviceService.GetAllServicesAsync();
        return View(services);
    }

    public async Task<IActionResult> Blog()
    {
        await SetRandomContent();
        var posts = await _blogService.GetBlogPostsAsync();
        return View(posts);
    }

    public async Task<IActionResult> BlogDetails(int id)
    {
        await SetRandomContent();
        var post = await _blogService.GetBlogPostByIdAsync(id);
        return View(post);
    }

    public async Task<IActionResult> Contacts()
    {
        await SetRandomContent();
        var contacts = await _contactService.GetAllContactInfoAsync();
        return View(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLead(LeadCreateModel model)
    {
        if (ModelState.IsValid)
        {
            await _createService.CreateLeadAsync(model);
            return RedirectToAction(nameof(Index));
        }

        await SetRandomContent();
        return View(nameof(Index), model);
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

    private async Task SetRandomContent()
    {
        var content = await _mainContentService.GetAllMainContentAsync();
        var random = new Random();
        var randomContent = content[random.Next(content.Count)];
        ViewBag.RandomContent = randomContent;
    }
}
