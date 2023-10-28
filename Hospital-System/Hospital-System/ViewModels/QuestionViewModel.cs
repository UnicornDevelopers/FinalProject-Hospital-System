using Hospital_System.Models;

namespace Hospital_System.ViewModels
{
    public class QuestionViewModel
    {
        public int TotalAnswersCount { get; set; }
        public List<Question> Questions { get; set; }

    }
}
