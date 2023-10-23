using System;
namespace Hospital_System.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }

}

