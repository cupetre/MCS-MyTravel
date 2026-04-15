using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;
using MCS_MyTravel.Repositories;

namespace MCS_MyTravel.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _bookingRepo;

        public BookingServices(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            if (booking.StartDate == default)
                throw new InvalidOperationException("Start date is required.");

            if (booking.EndDate == default)
                throw new InvalidOperationException("End date is required.");

            if (booking.EndDate < booking.StartDate)
                throw new InvalidOperationException("End date cannot be before start date.");

            try
            {
                await _bookingRepo.AddBookingAsync(booking);
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create booking.", ex);
            }
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            {
                if (bookingId <= 0)
                    throw new ArgumentException("Booking ID must be greater than zero.", nameof(bookingId));

                try
                {
                    Booking? booking = await _bookingRepo.GetBookingByIdAsync(bookingId);

                    if (booking == null)
                        throw new Exception($"Booking with ID {bookingId} was not found.");

                    await _bookingRepo.DeleteBookingAsync(booking);
                }
                catch (Exception ex) when (ex.Message.Contains("was not found"))
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to delete booking with ID {bookingId}.", ex);
                }
            }
        }

        public async Task<List<Booking>> GetBookingsByClientIdAsync(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("Client ID must be greater than zero.", nameof(clientId));

            try
            {
                return await _bookingRepo.GetBookingByClientAsync(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve bookings for client {clientId}.", ex);
            }
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            try
            {
                await _bookingRepo.UpdateNewBookingAsync(booking);
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update booking with ID {booking.Id}.", ex);
            }
        }
    }
}
