using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    public class QuestionController : Controller
    {


        private readonly IQuestion _questionService;
        private readonly HospitalDbContext _context;

        public QuestionController(IQuestion questionService, HospitalDbContext context)
        {
            _questionService = questionService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int totalAnswersCount = _context.Answers.Count();


            var viewModel = new QuestionViewModel
            {
                TotalAnswersCount = totalAnswersCount,
                Questions = await _questionService.GetQuestionsWithAnswerCount()

            };



            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            return View(question);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId == null)
            {
                return NotFound();
            }


            await _questionService.AddQuestionAsync(question, UserId);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult AddAnswer(int questionId)
        {
            var answer = new Answer { QuestionId = questionId };
            return View(answer);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(Answer answer)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId == null)
            {
                return NotFound();
            }

            await _questionService.AddAnswerAsync(answer, UserId);
            return RedirectToAction("Details", new { id = answer.QuestionId });


        }



    }
}