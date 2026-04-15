using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;
using MCS_MyTravel.Repositories;

namespace MCS_MyTravel.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly IBookingRepo _bookingRepo;

        public PaymentServices(
            IPaymentRepo paymentRepo,
            IBookingRepo bookingRepo
            )
        {
            _paymentRepo = paymentRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            ValidatePayment( payment );

            var booking = await _bookingRepo.GetBookingByIdAsync( payment.BookingId );

            if (booking == null)
            {
                throw new KeyNotFoundException("booking did not that that ID");
            }

            await _paymentRepo.AddPaymentAsync(payment);
            return payment;
        }

        public async Task DeletePaymentAsync(Payment payment)
        {
            var doesExist = await _paymentRepo.GetPaymentByIdAsync(payment.Id);

            if (doesExist == null)
            {
                throw new KeyNotFoundException("payment does not exist in DB");
            }

            await _paymentRepo.DeletePaymentAsync(payment);
        }

        public async Task<List<Payment>> GetPaymentsByBookingIdAsync(int bookingId)
        {
            return await _paymentRepo.GetPaymentsByBookingIdAsync(bookingId);
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            ValidatePayment(payment);

            var existingPayment = await _paymentRepo.GetPaymentByIdAsync(payment.Id);
            
            if (existingPayment == null)
            {
                throw new KeyNotFoundException("payment does not exist");
            }

            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.Notes = payment.Notes;

            await _paymentRepo.UpdatePaymentAsync(existingPayment);
            return existingPayment;
        }

        private static void ValidatePayment(Payment payment)
        {
            if (payment.Amount <= 0)
                throw new InvalidOperationException("Payment amount must be greater than 0.");

            if (payment.PaymentDate == default)
                throw new InvalidOperationException("Payment date is required.");

            if (payment.BookingId <= 0)
                throw new InvalidOperationException("Booking is required.");
        }
    }
}
