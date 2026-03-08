using GreedyVsDynamicScheduler.Models;

namespace GreedyVsDynamicScheduler.Data;

public static class BookingRequestFactory
{
    public static IReadOnlyList<BookingRequest> CreateFreelancerDay()
    {
        return
        [
            new BookingRequest("Client A", 1, 4, 70),
            new BookingRequest("Client B", 4, 7, 70),
            new BookingRequest("Client C", 1, 7, 135),
            new BookingRequest("Client D", 7, 9, 50),
            new BookingRequest("Client E", 9, 11, 50),
            new BookingRequest("Client F", 11, 13, 50),
            new BookingRequest("Client G", 13, 15, 50)
        ];
    }
}