using Hospital_System.Data;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models.Services
{
    public class QuestionService : IQuestion
    {
        private readonly HospitalDbContext _context;

        public QuestionService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .ThenInclude(a => a.Doctor)
                .FirstOrDefaultAsync(q => q.Id == id);
        }


        public async Task AddQuestionAsync(Question question, string userId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null)
            {
                throw new Exception("Patient not found");

            }
            question.PatientId = patient.Id;
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAnswerAsync(Answer answer, string userId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");

            }
            answer.DoctorId = doctor.Id;
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Answer>> GetAnswersForQuestionAsync(int questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }




        public async Task<List<Question>> GetQuestionsWithAnswerCount()
        {
            var questions = await _context.Questions
                        .Include(q => q.Patient)
                        .ToListAsync();


            foreach (var question in questions)
            {
                int answerCount = await GetAnswerCountForQuestion(question.Id);
                question.AnswerCount = answerCount;
            }

            return questions;
        }

        public async Task<int> GetAnswerCountForQuestion(int questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .CountAsync();
        }




    }
}


