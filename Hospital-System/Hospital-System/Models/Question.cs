namespace Hospital_System.Models
{
    public class Question
    {
       
            public int Id { get; set; }
            public string Subject { get; set; }
            public string QuestionText { get; set; }
            public int PatientId { get; set; }
            public Patient Patient { get; set; }
            public List<Answer>? Answers { get; set; }

            public int? AnswerCount { get; set; }
     }
}
