using Microsoft.AspNetCore.Mvc;
using StackOverflowClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflowClone.Controllers
{
    [Authorize]
    public class StackOverFlowController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public StackOverFlowController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Question(int id)
        {
            ViewBag.QuestionId = id;
            return View(_db.Answers.Where(x => x.QuestionId == id));
        }
        public IActionResult CreateAnswer(int id)
        {
            ViewBag.QuestionId = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            answer.User = currentUser;
            int temp = answer.Id;
            answer.Id = answer.QuestionId;
            answer.QuestionId = temp;
            _db.Answers.Add(answer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
