﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LSS.Models.LSS_DB_Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LSS_databaseEntities : DbContext
    {
        public LSS_databaseEntities()
            : base("name=LSS_databaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActionsForImprovingTheCourse> ActionsForImprovingTheCourses { get; set; }
        public virtual DbSet<AssessedCours> AssessedCourses { get; set; }
        public virtual DbSet<AssessmentPlanforTheStudentLearningOutcomeTechnique> AssessmentPlanforTheStudentLearningOutcomeTechniques { get; set; }
        public virtual DbSet<byCompletingThisCourseStudentsAreAbleTo> byCompletingThisCourseStudentsAreAbleToes { get; set; }
        public virtual DbSet<CLO> CLOes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAssessmentMapping> CourseAssessmentMappings { get; set; }
        public virtual DbSet<CourseAssessmentSurvay> CourseAssessmentSurvays { get; set; }
        public virtual DbSet<CourseCoordinator> CourseCoordinators { get; set; }
        public virtual DbSet<CourseTeachingStrategy> CourseTeachingStrategies { get; set; }
        public virtual DbSet<CourseTextBook> CourseTextBooks { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<ineffectiveAssessmentStrategi> ineffectiveAssessmentStrategis { get; set; }
        public virtual DbSet<ineffectiveTeachingStrategi> ineffectiveTeachingStrategis { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<ModerationChecklist> ModerationChecklists { get; set; }
        public virtual DbSet<OtherLecturer> OtherLecturers { get; set; }
        public virtual DbSet<PEO> PEOs { get; set; }
        public virtual DbSet<PI> PIs { get; set; }
        public virtual DbSet<ResultOfCourseDirectAssessment> ResultOfCourseDirectAssessments { get; set; }
        public virtual DbSet<schedule> schedules { get; set; }
        public virtual DbSet<SLO> SLOes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Course_Grade> Student_Course_Grade { get; set; }
        public virtual DbSet<Student_PI_Outcome> Student_PI_Outcome { get; set; }
        public virtual DbSet<TeachingAndLearningStrategiesTechnique> TeachingAndLearningStrategiesTechniques { get; set; }
    }
}
