using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Students
{
    public class Student
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Average { get; set; }
        public IList<Result> Results { get; set; }
        public virtual void Validate()
        {
            if (String.IsNullOrEmpty(Name))
                throw new StudentUninformedNameException();
            if (Name.Length > 100)
                throw new StudentNameLengthOverflowException();
            if (Age == 0)
                throw new StudentUninformedAgeException();
            if (Age < 0)
                throw new StudentInvalidAgeException();
        }

        public void CalculateAverage()
        {
            Average = 0;
            int AmountOfNotes = Results.Count();
            foreach (var item in Results)
            {
                Average += item.Note;
            }
            Average /= AmountOfNotes;
        }

        public void RoundAverage()
        {
            double AverageTruncated = Math.Truncate(Average);
            double Decimals = Average - AverageTruncated;
            if (Decimals < 0.35)
            {
                Average = Math.Floor(Average);
            }
            else if(Decimals >= 0.35 && Decimals < 0.75)
            {
                Average = AverageTruncated + 0.5;
            }
            else
            {
                Average = Math.Ceiling(Average);
            }  
        }
    }
}
