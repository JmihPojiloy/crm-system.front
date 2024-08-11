using front.Models;
using front.Models.ContentModels;
using front.Models.CreateModels;
using front.Services;

namespace front.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiService _apiService;
        private readonly BlogService _blogService;
        private readonly ContactService _contactService;
        private readonly MainContentService _mainContentService;
        private readonly ProjectService _projectService;
        private readonly ServiceService _serviceService;

        public AdminController(ApiService apiService,
            BlogService blogService,
            ContactService contactService,
            MainContentService mainContentService,
            ProjectService projectService,
            ServiceService serviceService)
            {
                _apiService = apiService;
                _blogService = blogService;
                _contactService = contactService;
                _mainContentService = mainContentService;
                _projectService = projectService;
                _serviceService = serviceService;
            }

        //Lead methods
        public async Task<IActionResult> Index()
        {
            var leads = await _apiService.GetLeadsAsync();
            return View(leads);
        }

        public async Task<IActionResult> EditLead(int id)
        {
            var lead = await _apiService.GetLeadByIdAsync(id);
            return View(lead);
        }

        [HttpPost]
        public async Task<IActionResult> EditLead(int id, LeadModel lead)
        {
            await _apiService.UpdateLeadAsync(id, lead);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteLead(int id)
        {
            await _apiService.DeleteLeadAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Main Content Methods
        public async Task<IActionResult> MainContent()
        {
            var mainContents = await _mainContentService.GetAllMainContentAsync();
            return View(mainContents);
        }

        public async Task<IActionResult> EditMainContent(int id)
        {
            var content = await _mainContentService.GetMainContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "MainContent";
            return PartialView("_EditForm", content);
        }

        [HttpPost]
        public async Task<IActionResult> EditMainContent(MainContentModel model)
        {
            if (ModelState.IsValid)
            {
                await _mainContentService.UpdateMainContentAsync(model);
                return RedirectToAction(nameof(MainContent));
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "MainContent";
            return PartialView("_EditForm", model);
        }

        public IActionResult AddMainContent()
        {
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "MainContent";
            return PartialView("_EditForm", new MainContentCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddMainContent(MainContentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _mainContentService.AddMainContentAsync(model);
                return RedirectToAction(nameof(MainContent));
            }
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "MainContent";
            return PartialView("_EditForm", model);
        }

        public async Task<IActionResult> DeleteMainContent(int id)
        {
            var content = await _mainContentService.GetMainContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            ViewData["EntityName"] = "MainContent";
            return PartialView("_DeleteConfirmation", content);
        }

        [HttpPost, ActionName("DeleteMainContent")]
        public async Task<IActionResult> DeleteMainContentConfirmed(int id)
        {
            await _mainContentService.DeleteMainContentAsync(id);
            return RedirectToAction(nameof(MainContent));
        }

        // Project Methods
        public async Task<IActionResult> Project()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return View(projects);
        }

        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Project";
            return PartialView("_EditForm", project);
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.UpdateProjectAsync(model);
                return RedirectToAction(nameof(Project));
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Project";
            return PartialView("_EditForm", model);
        }

        public IActionResult AddProject()
        {
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Project";
            return PartialView("_EditForm", new ProjectCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.AddProjectAsync(model);
                return RedirectToAction(nameof(Project));
            }
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Project";
            return PartialView("_EditForm", model);
        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["EntityName"] = "Project";
            return PartialView("_DeleteConfirmation", project);
        }

        [HttpPost, ActionName("DeleteProject")]
        public async Task<IActionResult> DeleteProjectConfirmed(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToAction(nameof(Project));
        }

        // Service Methods
        public async Task<IActionResult> Service()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return View(services);
        }

        public async Task<IActionResult> EditService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Service";
            return PartialView("_EditForm", service);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.UpdateServiceAsync(model);
                return RedirectToAction(nameof(Service));
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Service";
            return PartialView("_EditForm", model);
        }

        public IActionResult AddService()
        {
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Service";
            return PartialView("_EditForm", new ServiceCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.AddServiceAsync(model);
                return RedirectToAction(nameof(Service));
            }
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Service";
            return PartialView("_EditForm", model);
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["EntityName"] = "Service";
            return PartialView("_DeleteConfirmation", service);
        }

        [HttpPost, ActionName("DeleteService")]
        public async Task<IActionResult> DeleteServiceConfirmed(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToAction(nameof(Service));
        }

        // BlogPost Methods
        public async Task<IActionResult> BlogPost()
        {
            var posts = await _blogService.GetBlogPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> EditBlogPost(int id)
        {
            var post = await _blogService.GetBlogPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "BlogPost";
            return PartialView("_EditForm", post);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlogPost(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogService.UpdateBlogPostAsync(model);
                return RedirectToAction(nameof(BlogPost));
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "BlogPost";
            return PartialView("_EditForm", model);
        }

        public IActionResult AddBlogPost()
        {
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "BlogPost";
            return PartialView("_EditForm", new BlogPostCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogPost(BlogPostCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogService.AddBlogPostAsync(model);
                return RedirectToAction(nameof(BlogPost));
            }
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "BlogPost";
            return PartialView("_EditForm", model);
        }

        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var post = await _blogService.GetBlogPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["EntityName"] = "BlogPost";
            return PartialView("_DeleteConfirmation", post);
        }

        [HttpPost, ActionName("DeleteBlogPost")]
        public async Task<IActionResult> DeleteBlogPostConfirmed(int id)
        {
            await _blogService.DeleteBlogPostAsync(id);
            return RedirectToAction(nameof(BlogPost));
        }

        // Contact Methods
        public async Task<IActionResult> Contact()
        {
            var contacts = await _contactService.GetAllContactInfoAsync();
            return View(contacts);
        }

        public async Task<IActionResult> EditContact(int id)
        {
            var contact = await _contactService.GetContactInfoByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Contact";
            return PartialView("_EditForm", contact);
        }

        [HttpPost]
        public async Task<IActionResult> EditContact(ContactInfoModel model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactInfoAsync(model);
                return RedirectToAction(nameof(Contact));
            }
            ViewData["IsEdit"] = true;
            ViewData["EntityName"] = "Contact";
            return PartialView("_EditForm", model);
        }

        public IActionResult AddContact()
        {
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Contact";
            return PartialView("_EditForm", new ContactInfoCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactInfoCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddContactInfoAsync(model);
                return RedirectToAction(nameof(Contact));
            }
            ViewData["IsEdit"] = false;
            ViewData["EntityName"] = "Contact";
            return PartialView("_EditForm", model);
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _contactService.GetContactInfoByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["EntityName"] = "Contact";
            return PartialView("_DeleteConfirmation", contact);
        }

        [HttpPost, ActionName("DeleteContact")]
        public async Task<IActionResult> DeleteContactConfirmed(int id)
        {
            await _contactService.DeleteContactInfoAsync(id);
            return RedirectToAction(nameof(Contact));
        }
    }
}