using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSS.Models.CourseFlolder;

namespace LSS.Models.Forms
{
    public class ExamModerationChecklistForm
    {
        public Exam Exam { get; set; }
        public Boolean TheExamAdheresToApprovedQAForm { get; set; }
        public Boolean AreExamItemsAlignedToTheCLOsListedInTheCourseSyllabus { get; set; }
        public Boolean AreTheExamItemsAlignedToTheSlOsListedInTheCourseSyllabus { get; set; }
        public Boolean IsTheExaminationComprehensiveAndAppropriateGivenTheCourseContentAndClassSchedule { get; set; }
        public Boolean AreTheQuestionsStatmentsClearFreeOFTyposCompleteAndNotMisleanding { get; set; }
        public Boolean DoTheExamItemsTestTheVariousLevelsOFKnowledgeComprehensionAnalysisApplcationEvaluationECT { get; set; }
        public Boolean TheQustionsGivenAreAdequateOrAppropriateToTheAllicatedExamTime { get; set; }
        public Boolean HasAMarkingchemeCovringAllTheXamItemsBEenProvided { get; set; }

    }
}