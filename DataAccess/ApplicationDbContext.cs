using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shifaa.Models;
using static Shifaa.Models.Enums;

namespace Shifaa.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region DbSets
        // Auth & OTP
        public DbSet<ApplicationUserOTP> ApplicationUserOTPs { get; set; }

        // Profile DbSets (1-to-1 with ApplicationUser)
        public DbSet<Member> Members { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalCenter> MedicalCenters { get; set; }

        // Relationships
        public DbSet<DoctorAssignment> DoctorAssignments { get; set; }
        public DbSet<GuardianMember> GuardianMembers { get; set; }

        // Scheduling
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        // Appointments & Prescriptions
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        // Medical Records & Health Data
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<HealthReading> HealthReadings { get; set; }
        public DbSet<AIAlert> AIAlerts { get; set; }

        // Tracking & Notifications
        public DbSet<TrackingLog> TrackingLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // Reviews
        public DbSet<Review> Reviews { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ApplicationUser Configuration
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                // Stores only "guid.jpg" — physical file in wwwroot/Images/Profiles/
                entity.Property(e => e.ProfilePicture).HasMaxLength(260);

                entity.Property(e => e.Language).HasMaxLength(10).HasDefaultValue("ar");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                // 1-to-1 profile relationships
                entity.HasOne(e => e.Member)
                      .WithOne(m => m.User)
                      .HasForeignKey<Member>(m => m.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Caregiver)
                      .WithOne(c => c.User)
                      .HasForeignKey<Caregiver>(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Doctor)
                      .WithOne(d => d.User)
                      .HasForeignKey<Doctor>(d => d.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MedicalCenter)
                      .WithOne(mc => mc.User)
                      .HasForeignKey<MedicalCenter>(mc => mc.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Notifications
                entity.HasMany(e => e.Notifications)
                      .WithOne(n => n.User)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.IsDeleted);
            });
            #endregion

            #region Member Configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(20);
                entity.Property(e => e.BloodType).HasMaxLength(10);
                entity.Property(e => e.Weight).HasPrecision(5, 2);
                entity.Property(e => e.Height).HasPrecision(5, 2);
                entity.Property(e => e.ChronicConditions).HasMaxLength(1000);
                entity.Property(e => e.DrugAllergies).HasMaxLength(500);
                entity.Property(e => e.FoodAllergies).HasMaxLength(500);
                entity.Property(e => e.InsuranceProvider).HasMaxLength(200);
                entity.Property(e => e.InsuranceId).HasMaxLength(100);
                entity.Property(e => e.LastLatitude).HasColumnType("float");
                entity.Property(e => e.LastLongitude).HasColumnType("float");
                entity.Property(e => e.GpsEnabled).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasIndex(e => e.GpsEnabled);
            });
            #endregion

            #region Caregiver Configuration
            modelBuilder.Entity<Caregiver>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(20);
                entity.Property(e => e.RelationshipType).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserId).IsUnique();
            });
            #endregion

            #region Doctor Configuration
            // Doctor is a STANDALONE entity — not owned by any Medical Center
            // The Doctor-MedicalCenter relationship is managed via DoctorAssignment table
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();

                // NO MedicalCenterId here — doctor is standalone
                entity.Property(e => e.Specialty).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ConsultationFee).HasPrecision(8, 2);
                entity.Property(e => e.YearsOfExperience).HasDefaultValue(0);
                entity.Property(e => e.Bio).HasMaxLength(2000);
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.Rating).HasPrecision(3, 2).HasDefaultValue(0.0);
                entity.Property(e => e.TotalRatings).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasIndex(e => e.Specialty);
                entity.HasIndex(e => e.IsAvailable);
            });
            #endregion

            #region MedicalCenter Configuration
            modelBuilder.Entity<MedicalCenter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.FacilityName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FacilityType).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.Latitude).HasColumnType("float");
                entity.Property(e => e.Longitude).HasColumnType("float");
                entity.Property(e => e.WorkingHours).HasMaxLength(500);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.IsVerified).HasDefaultValue(false);
                entity.Property(e => e.Rating).HasPrecision(3, 2).HasDefaultValue(0.0);
                entity.Property(e => e.TotalRatings).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasIndex(e => e.FacilityType);
                entity.HasIndex(e => e.IsVerified);
            });
            #endregion

            #region DoctorAssignment Configuration
            // Many-to-many between Doctor and MedicalCenter
            // Medical Center sends request → Doctor accepts or rejects
            modelBuilder.Entity<DoctorAssignment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.MedicalCenterId).IsRequired();
                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasDefaultValue(DoctorAssignmentStatus.Pending);
                entity.Property(e => e.RequestedAt).HasDefaultValueSql("GETUTCDATE()");

                // FK: Doctor side
                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Assignments)
                      .HasForeignKey(e => e.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK: MedicalCenter side
                entity.HasOne(e => e.MedicalCenter)
                      .WithMany(mc => mc.DoctorAssignments)
                      .HasForeignKey(e => e.MedicalCenterId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Prevent duplicate active assignments
                entity.HasIndex(e => new { e.DoctorId, e.MedicalCenterId }).IsUnique();
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.DoctorId);
                entity.HasIndex(e => e.MedicalCenterId);
            });
            #endregion

            #region GuardianMember Configuration
            // Caregiver links to Member via email lookup + in-app notification
            // Member accepts or declines from the notification panel — no SMS/OTP
            modelBuilder.Entity<GuardianMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CaregiverId).IsRequired();
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(LinkStatus.Pending);
                entity.Property(e => e.RequestedAt).HasDefaultValueSql("GETUTCDATE()");

                // NoAction to avoid multiple cascade path conflicts in SQL Server
                entity.HasOne(e => e.Caregiver)
                      .WithMany(c => c.Members)
                      .HasForeignKey(e => e.CaregiverId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Caregivers)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.NoAction);

                // One caregiver cannot send duplicate requests to same member
                entity.HasIndex(e => new { e.CaregiverId, e.MemberId }).IsUnique();
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.MemberId);
            });
            #endregion

            #region DoctorSchedule Configuration
            // OWNED by Medical Center — Doctor can view but cannot edit
            // Each schedule belongs to a specific Doctor at a specific MedicalCenter
            modelBuilder.Entity<DoctorSchedule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.MedicalCenterId).IsRequired();
                entity.Property(e => e.Day).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();

                // FK: Doctor
                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Schedules)
                      .HasForeignKey(e => e.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK: MedicalCenter (owns the schedule)
                entity.HasOne(e => e.MedicalCenter)
                      .WithMany(mc => mc.Schedules)
                      .HasForeignKey(e => e.MedicalCenterId)
                      .OnDelete(DeleteBehavior.Restrict);

                // One doctor can have one schedule per day per medical center
                entity.HasIndex(e => new { e.DoctorId, e.MedicalCenterId, e.Day }).IsUnique();
                entity.HasIndex(e => e.DoctorId);
                entity.HasIndex(e => e.MedicalCenterId);
            });
            #endregion

            #region TimeSlot Configuration
            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DoctorScheduleId).IsRequired();
                entity.Property(e => e.SlotDate).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();
                entity.Property(e => e.IsBooked).HasDefaultValue(false);

                entity.HasOne(e => e.DoctorSchedule)
                      .WithMany(ds => ds.TimeSlots)
                      .HasForeignKey(e => e.DoctorScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.DoctorScheduleId, e.SlotDate });
                entity.HasIndex(e => e.IsBooked);
                entity.HasIndex(e => e.SlotDate);
            });
            #endregion

            #region Appointment Configuration
            // One TimeSlot = One Appointment (1-to-1)
            // Only Medical Center can confirm/reject — NOT the Doctor
            // 5-minute auto-confirm handled by Hangfire background job
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.MedicalCenterId).IsRequired();
                entity.Property(e => e.TimeSlotId).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(AppointmentStatus.Pending);
                entity.Property(e => e.SymptomSummary).HasMaxLength(2000);

                // Display only — no payment processing in Phase 1
                entity.Property(e => e.AppointmentFee).HasPrecision(8, 2);
                entity.Property(e => e.PaymentMethod).HasMaxLength(100);

                // Populated when Medical Center reschedules
                entity.Property(e => e.RescheduleReason).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Appointments)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Appointments)
                      .HasForeignKey(e => e.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MedicalCenter)
                      .WithMany(mc => mc.Appointments)
                      .HasForeignKey(e => e.MedicalCenterId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 1-to-1: one slot can only ever have one appointment
                entity.HasOne(e => e.TimeSlot)
                      .WithOne(ts => ts.Appointment)
                      .HasForeignKey<Appointment>(e => e.TimeSlotId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.MemberId, e.Status });
                entity.HasIndex(e => new { e.DoctorId, e.Status });
                entity.HasIndex(e => new { e.MedicalCenterId, e.Status });
                entity.HasIndex(e => e.IsDeleted);
            });
            #endregion

            #region Prescription Configuration
            // AppointmentId is NULLABLE — doctor can issue prescription
            // outside of a formal appointment if needed
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.MemberId).IsRequired();

                // Nullable — prescription does not require an appointment
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Prescriptions)
                      .HasForeignKey(e => e.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Prescriptions)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Nullable 1-to-1 with Appointment
                entity.HasOne(e => e.Appointment)
                      .WithOne(a => a.Prescription)
                      .HasForeignKey<Prescription>(e => e.AppointmentId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => e.DoctorId);
                entity.HasIndex(e => e.AppointmentId);
                entity.HasIndex(e => e.IsDeleted);
            });
            #endregion

            #region PrescriptionItem Configuration
            modelBuilder.Entity<PrescriptionItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PrescriptionId).IsRequired();
                entity.Property(e => e.MedicineName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Dose).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Frequency).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MealTiming).IsRequired();
                entity.Property(e => e.Days).IsRequired();

                entity.HasOne(e => e.Prescription)
                      .WithMany(p => p.Items)
                      .HasForeignKey(e => e.PrescriptionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.PrescriptionId);
            });
            #endregion

            #region MedicalRecord Configuration
            // FileName stores only "guid.ext" — NOT a full URL or path
            // Physical file saved to wwwroot/MedicalRecords/Members/{MemberId}/
            // Uploaded by: Member manually, Doctor, or Medical Center (lab/ray results)
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.UploadedBy).IsRequired().HasMaxLength(450);
                entity.Property(e => e.SourceType).IsRequired();

                // Changed from FileUrl to FileName — stores "guid.pdf" only
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(260);

                entity.Property(e => e.FileType).IsRequired();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.MedicalRecords)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Uploader)
                      .WithMany()
                      .HasForeignKey(e => e.UploadedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => e.SourceType);
                entity.HasIndex(e => e.FileType);
            });
            #endregion

            #region HealthReading Configuration
            modelBuilder.Entity<HealthReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.ReadingType).IsRequired();
                entity.Property(e => e.Value).IsRequired().HasPrecision(8, 2);
                entity.Property(e => e.Unit).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Source).IsRequired();
                entity.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.HealthReadings)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => e.ReadingType);
                entity.HasIndex(e => e.Timestamp).IsDescending();
            });
            #endregion

            #region AIAlert Configuration
            // 1-to-1 with HealthReading — one reading triggers at most one alert
            // Alert is pushed to Member AND all linked Caregivers simultaneously
            modelBuilder.Entity<AIAlert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.ReadingId).IsRequired();
                entity.Property(e => e.AlertType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Severity).IsRequired();
                entity.Property(e => e.ThresholdValue).HasPrecision(8, 2);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
                entity.Property(e => e.RecommendedAction).HasMaxLength(500);
                entity.Property(e => e.IsAcknowledged).HasDefaultValue(false);
                entity.Property(e => e.TriggeredAt).HasDefaultValueSql("GETUTCDATE()");
                

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.AIAlerts)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                // 1-to-1: one reading = one alert maximum
                entity.HasOne(e => e.Reading)
                      .WithOne(hr => hr.AIAlert)
                      .HasForeignKey<AIAlert>(e => e.ReadingId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => e.Severity);
                entity.HasIndex(e => e.IsAcknowledged);
            });
            #endregion

            #region TrackingLog Configuration
            // Stores GPS broadcast from Member when GpsEnabled = true
            // Geofence feature is CANCELLED for Phase 1 — do not add geofence fields
            modelBuilder.Entity<TrackingLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.Latitude).IsRequired().HasColumnType("float");
                entity.Property(e => e.Longitude).IsRequired().HasColumnType("float");
                entity.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.TrackingLogs)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => e.Timestamp).IsDescending();
            });
            #endregion

            #region Notification Configuration
            // Covers all notification types including actionable ones
            // (e.g. CaregiverLinkRequest with Accept/Decline embedded in notification panel)
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.IsRead).HasDefaultValue(false);

                // For actionable notifications — stores JSON e.g. {"requestId": 5}
                entity.Property(e => e.ActionPayload).HasMaxLength(500);
                entity.Property(e => e.IsActioned).HasDefaultValue(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => new { e.UserId, e.IsRead });
                entity.HasIndex(e => e.Type);
            });
            #endregion

            #region Review Configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.TargetId).IsRequired();
                entity.Property(e => e.TargetType).IsRequired();
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.Comment).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Reviews)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.MemberId);
                entity.HasIndex(e => new { e.TargetType, e.TargetId });
                entity.HasIndex(e => e.Rating);
            });
            #endregion

            #region ApplicationUserOTP Configuration
            modelBuilder.Entity<ApplicationUserOTP>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OTP).IsRequired().HasMaxLength(6);
                entity.Property(e => e.ApplicationUserId).IsRequired();
                entity.Property(e => e.IsValid).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.ApplicationUser)
                      .WithMany()
                      .HasForeignKey(e => e.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.ApplicationUserId);
                entity.HasIndex(e => e.ExpireAt);
                entity.HasIndex(e => e.IsValid);
            });
            #endregion
        }
    }
}

