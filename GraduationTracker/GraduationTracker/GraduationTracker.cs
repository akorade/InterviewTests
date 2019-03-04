using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class GraduationTracker : IGraduationTracker
    {
        /// Use dependency injection for repository so that it can be mocked when testing.
        IRepository repository;
        public GraduationTracker(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// This Method based on Average checks whether the student is graduated or not.
        /// </summary>
        /// <param name="diploma"></param>
        /// <param name="student"></param>
        /// <returns>tuple of whether graduated and standing</returns>
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;
            int totalCourses = 0;
            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                var requirement = repository.GetRequirement(diploma.Requirements[i]);
                var requiredCoursesCompleted = student.Courses.Where(c => requirement.Courses.Contains(c.Id));
                average += requiredCoursesCompleted.Sum(c => c.Mark);
                credits += requiredCoursesCompleted.Where(rcp => rcp.Mark > requirement.MinimumMark).Count() * requirement.Credits;
                totalCourses = requirement.Courses.Count();
            }
            average = average / totalCourses;

            // No Case for STANDING.SumaCumLaude defined: requirement not clear
            if (average < 50)
                return new Tuple<bool, STANDING>(false, STANDING.Remedial);
            else if (average < 80)
                return new Tuple<bool, STANDING>(true, STANDING.Average);
            else if (average < 95)
                return new Tuple<bool, STANDING>(true, STANDING.MagnaCumLaude);
            else
                return new Tuple<bool, STANDING>(false, STANDING.MagnaCumLaude);
        }
    }
}
