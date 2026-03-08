# (G43) GreedyVsDynamicScheduler

A console application that compares a greedy scheduling strategy with a dynamic programming solution for selecting the most valuable set of non-overlapping booking requests.

---
## Course Context

**Course**: Data Structures and Algorithms  
**Section**: Greedy Algorithms vs Dynamic Programming

This project demonstrates the trade-off between fast local decision-making and globally optimal scheduling. It shows how a greedy approach can be simpler, while dynamic programming can find a better overall result.

---
## Concepts Demonstrated

- Greedy scheduling
- Dynamic programming
- Weighted interval scheduling
- Comparing local vs global optimisation
- Sorting and compatibility checks
- Console-based algorithm comparison

---
## Project Structure

- **Algorithms** – Greedy and dynamic programming schedulers
- **Data** – Sample booking request dataset
- **Models** – Booking requests and scheduling results
- **Presentation** – Console output formatting
- **Program.cs** – Application entry point

---
## How the Application Works

1. A sample dataset of booking requests is generated.
2. Each request has a start time, end time, and payout value.
3. The greedy scheduler selects the highest-value compatible requests first.
4. The dynamic programming scheduler evaluates optimal combinations using weighted interval scheduling.
5. Both results are displayed side by side for comparison.
6. The console output highlights how the greedy strategy may miss globally optimal solutions.