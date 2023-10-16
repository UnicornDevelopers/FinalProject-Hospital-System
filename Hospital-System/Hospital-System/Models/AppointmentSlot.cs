using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital_System.Models;

namespace Hospital_System.Models
{
	public class AppointmentSlot
	{
		public int Id { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime DateOfSlot { get; set; }
		public TimeSpan SlotHour { get; set; }
		public List<Appointment>? Appointments { get; set; }

		public int DoctorId { get; set; }


		[ForeignKey("DoctorId")]
		public Doctor doctor { get; set; }

	}
}


/*

public void InitializeTimeSlots(Doctor doctor, DateTime startDate, DateTime endDate, TimeSpan startHour, TimeSpan endHour)
{
    // Loop over each day between startDate and endDate
    for (var date = startDate; date <= endDate; date = date.AddDays(1))
    {
        // Loop over each hour between startHour and endHour
        for (var hour = startHour; hour < endHour; hour = hour.Add(TimeSpan.FromHours(1)))
        {
            // Create a new AppointmentSlot
            var appointmentSlot = new AppointmentSlot
            {
                DoctorId = doctor.DoctorId,
                SlotDate = date,
                SlotHour = hour,
                Appointments = new List<Appointment>()
            };

            // Add the AppointmentSlot to the doctor's list
            doctor.AppointmentSlots.Add(appointmentSlot);
        }
    }
}*/