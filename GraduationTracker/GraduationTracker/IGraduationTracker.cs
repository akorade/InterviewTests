using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public interface IGraduationTracker
    {
        Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student);
    }
}
