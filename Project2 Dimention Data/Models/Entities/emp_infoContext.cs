using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project2_Dimention_Data.Models.Entities
{
    public partial class emp_infoContext : DbContext
    {
        public emp_infoContext()
        {
        }

        public emp_infoContext(DbContextOptions<emp_infoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeePerf> EmployeePerves { get; set; }
        public virtual DbSet<Jobdetail> Jobdetails { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<ManagerRating> ManagerRatings { get; set; }
        public virtual DbSet<PrimaryTable> PrimaryTables { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePerf>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AFB3EC6DF6BA8F64");

                entity.ToTable("EmployeePerf");

                entity.Property(e => e.EmpId).HasColumnName("empID");

                entity.Property(e => e.EmpNumber).HasColumnName("emp_number");

                entity.Property(e => e.EnvironmentSat).HasColumnName("environment_sat");

                entity.Property(e => e.JobSat).HasColumnName("job_sat");

                entity.Property(e => e.RelationshipSat).HasColumnName("relationship_sat");

                entity.Property(e => e.WorkLifeBalance).HasColumnName("work_life_balance");

                entity.HasOne(d => d.EmpNumberNavigation)
                    .WithMany(p => p.EmployeePerves)
                    .HasForeignKey(d => d.EmpNumber)
                    .HasConstraintName("FK__EmployeeP__emp_n__5FB337D6");
            });

            modelBuilder.Entity<Jobdetail>(entity =>
            {
                entity.HasKey(e => e.JobDetailsId)
                    .HasName("PK__jobdetai__D136C0208025B11E");

                entity.ToTable("jobdetails");

                entity.Property(e => e.JobDetailsId).HasColumnName("JobDetailsID");

                entity.Property(e => e.Attrition)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("attrition");

                entity.Property(e => e.BusinessTravel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("business_travel");

                entity.Property(e => e.Department)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.EmpNumber).HasColumnName("emp_number");

                entity.Property(e => e.EmployeeCount).HasColumnName("employee_count");

                entity.Property(e => e.JobInvolvement).HasColumnName("job_involvement");

                entity.Property(e => e.JobLevel).HasColumnName("job_level");

                entity.Property(e => e.JobRole)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("job_role");

                entity.Property(e => e.Overtime)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("overtime");

                entity.Property(e => e.StandardHours).HasColumnName("standard_hours");

                entity.Property(e => e.StockOptionLevel).HasColumnName("stock_option_level");

                entity.Property(e => e.YearsAtCompany).HasColumnName("years_at_company");

                entity.Property(e => e.YearsCurrentManager).HasColumnName("years_current_manager");

                entity.Property(e => e.YearsCurrentRole).HasColumnName("years_current_role");

                entity.Property(e => e.YearsLastPromotion).HasColumnName("years_last_promotion");

                entity.HasOne(d => d.EmpNumberNavigation)
                    .WithMany(p => p.Jobdetails)
                    .HasForeignKey(d => d.EmpNumber)
                    .HasConstraintName("FK__jobdetail__emp_n__5DCAEF64");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.EmpNum).HasColumnName("emp_num");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("name_user");

                entity.Property(e => e.Passwordhash)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Passwordsalt)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("passwordsalt");

                entity.Property(e => e.SurnameUser)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("surname_user");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_role");

                entity.HasOne(d => d.EmpNumNavigation)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.EmpNum)
                    .HasConstraintName("FK__login__emp_num__6B24EA82");
            });

            modelBuilder.Entity<ManagerRating>(entity =>
            {
                entity.HasKey(e => e.RatingId)
                    .HasName("PK__ManagerR__2D290D496DA942E8");

                entity.ToTable("ManagerRating");

                entity.Property(e => e.RatingId).HasColumnName("ratingID");

                entity.Property(e => e.EmpNumber).HasColumnName("emp_number");

                entity.Property(e => e.PerformanceRating).HasColumnName("performance_rating");

                entity.Property(e => e.TrainingTimesLastYear).HasColumnName("training_times_last_year");

                entity.HasOne(d => d.EmpNumberNavigation)
                    .WithMany(p => p.ManagerRatings)
                    .HasForeignKey(d => d.EmpNumber)
                    .HasConstraintName("FK__ManagerRa__emp_n__656C112C");
            });

            modelBuilder.Entity<PrimaryTable>(entity =>
            {
                entity.HasKey(e => e.EmpNumber)
                    .HasName("PK__PrimaryT__AD2DEBA7D6BAEF00");

                entity.ToTable("PrimaryTable");

                entity.Property(e => e.EmpNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_number");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.DistanceFromHome).HasColumnName("distance_from_home");

                entity.Property(e => e.Education).HasColumnName("education");

                entity.Property(e => e.EducationField)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("education_field");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("marital_status");

                entity.Property(e => e.NumCompaniesWorked).HasColumnName("num_companies_worked");

                entity.Property(e => e.NumWorkingYears).HasColumnName("num_working_years");

                entity.Property(e => e.Over18)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("over_18")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(e => e.RatesId)
                    .HasName("PK__Rates__4E33DFC334651313");

                entity.Property(e => e.RatesId).HasColumnName("ratesID");

                entity.Property(e => e.DailyRate).HasColumnName("daily_rate");

                entity.Property(e => e.EmpNumber).HasColumnName("emp_number");

                entity.Property(e => e.HourlyRate).HasColumnName("hourly_rate");

                entity.Property(e => e.MonthlyRate).HasColumnName("monthly_rate");

                entity.HasOne(d => d.EmpNumberNavigation)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.EmpNumber)
                    .HasConstraintName("FK__Rates__emp_numbe__619B8048");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.SalaryId).HasColumnName("salaryID");

                entity.Property(e => e.EmpNumber).HasColumnName("emp_number");

                entity.Property(e => e.MonthlyIncome).HasColumnName("monthly_income");

                entity.Property(e => e.PercentSalaryHike).HasColumnName("percent_salary_hike");

                entity.HasOne(d => d.EmpNumberNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpNumber)
                    .HasConstraintName("FK__Salary__emp_numb__6383C8BA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
