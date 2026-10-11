using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Payment
{
	public int PaymentId { get; set; }
	public int BookingId { get; set; }
	public decimal Amount { get; set; }
	public string PaymentMethod { get; set; } = "CASH"; // CASH, BANK_TRANSFER, CREDIT_CARD
	public DateTime PaymentDate { get; set; } = DateTime.Now;
	public string Status { get; set; } = "COMPLETED"; // PENDING, COMPLETED, REFUNDED
}