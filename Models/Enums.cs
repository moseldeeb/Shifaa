namespace Shifaa.Models
{
    public class Enums
    {
          public enum FacilityType
        {
            Clinic = 1,
            DiagnosisCenter = 2,
            RaysCenter = 3
        }

        public enum AppointmentStatus
        {
            Pending = 0,
            Confirmed = 1,
            Rejected = 2,
            Cancelled = 3,
            Completed = 4,
            Rescheduled = 5
        }

        public enum DoctorAssignmentStatus
        {
            Pending = 0,
            Active = 1,
            Rejected = 2,
            Terminated = 3
        }

        public enum LinkStatus
        {
            Pending = 0,
            Accepted = 1,
            Rejected = 2
        }

        public enum DayOfWeek
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }

        public enum FileType
        {
            PDF = 1,
            Image = 2
        }

        public enum ReadingType
        {
            BloodPressureSystolic = 1,
            BloodPressureDiastolic = 2,
            Glucose = 3,
            HeartRate = 4
        }

        public enum ReadingSource
        {
            Manual = 1,
            Wearable = 2
        }

        public enum AlertSeverity
        {
            Low = 1,
            Medium = 2,
            High = 3,
            Critical = 4
        }

        public enum NotificationType
        {
            // Member notifications
            MedicationReminder = 1,
            AppointmentReminder = 2,
            AppointmentStatusChanged = 3,
            CaregiverLinkRequest = 4,
            AIHealthAlert = 5,
            LabResultUploaded = 6,

            // Caregiver notifications
            MemberAcceptedLink = 7,
            MemberRejectedLink = 8,
            MemberAIHealthAlert = 9,
            MemberMissedMedication = 10,
            MemberAppointmentUpdate = 11,

            // Doctor notifications
            NewAppointmentRequest = 12,
            AppointmentCancelled = 13,
            NewMessage = 14,
            MedicalCenterAssignmentRequest = 15,
            NewLabResultUploaded = 16,

            // Medical Center notifications
            DoctorAssignmentResponse = 17,
            NewAppointmentBooked = 18
        }

        public enum TargetType
        {
            Doctor = 1,
            MedicalCenter = 2
        }

        public enum MealTiming
        {
            BeforeEating = 1,
            AfterEating = 2,
            WithFood = 3,
            AnyTime = 4
        }

        // Bitmask for days — allows combining days
        [Flags]
        public enum PrescriptionDays
        {
            None = 0,
            Sunday = 1,
            Monday = 2,
            Tuesday = 4,
            Wednesday = 8,
            Thursday = 16,
            Friday = 32,
            Saturday = 64,
            Everyday = Sunday | Monday | Tuesday | Wednesday
                     | Thursday | Friday | Saturday
        }

        public enum SourceType
        {
            Member = 1,
            Doctor = 2,
            MedicalCenter = 3
        }
    }
}
