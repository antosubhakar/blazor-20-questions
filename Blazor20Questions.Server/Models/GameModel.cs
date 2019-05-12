﻿using Blazor20Questions.Shared;
using System;

namespace Blazor20Questions.Server.Models
{
    public class GameModel
    {
        public Guid Id { get; set; }
        public bool Won { get; set; }
        public bool Lost { get; set; }
        public DateTime Expires { get; set; }
        public string Subject { get; set; }
        public int TotalQuestions { get; set; }
        public int QuestionsTaken { get; set; }
        public bool GuessesCountAsQuestions { get; set; }

        public bool IsComplete => Won || Lost || DateTime.UtcNow > Expires;

        public GameResponse ToResponseModel()
        {
            var response = new GameResponse
            {
                Id = Id,
                Won = Won,
                Complete = IsComplete,
                EndTime = Expires,
                QuestionsRemaining = TotalQuestions - QuestionsTaken,
                GuessesCountAsQuestions = GuessesCountAsQuestions
            };

            if (IsComplete) {
                response.Subject = Subject;
            }

            return response;
        }

        private static string Fuzz(string s)
        {
            return s.ToLower().Replace(" ", "");
        }

        public bool GuessMatches(string guess)
        {
            return Fuzz(guess) == Fuzz(Subject);
        }
    }
}
