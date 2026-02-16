## Objective

- Analyse the performance characteristics of BubbleSort, QuickSort, and MergeSort
- determine the best sorting algorithm for different real-world scenarios
- evaluate time and space complexity trade-offs 
- explain the reasoning behind choosing a sorting algorithm

---
## Core Algorithm Characteristics

- Bubble Sort  
	- Time Complexity  
		- Worst: O(n²)  
		- Best (nearly sorted): O(n)  
	- Space Complexity: O(1)  
	- Strength  
		- Very simple  
		- Good for very small or nearly sorted datasets  
	- Weakness  
		- Extremely inefficient for large datasets  

- QuickSort  
	- Time Complexity  
		- Average: O(n log n)  
		- Worst: O(n²)  
	- Space Complexity: O(log n)  
	- Strength  
		- Fast in practice  
		- In-place sorting  
	- Weakness  
		- Performance depends on pivot choice  

- Merge Sort  
	- Time Complexity: O(n log n) (consistent)  
	- Space Complexity: O(n)  
	- Strength  
		- Stable sorting  
		- Predictable runtime  
	- Weakness  
		- Higher memory usage  

---
## Scenario 1: Small Product List (< 20 Items, Nearly Sorted)

- Key Constraints  
	- Very small dataset  
	- Already mostly sorted  
	- Simplicity preferred  

- Best Choice: Bubble Sort  

- Justification  
	- Achieves O(n) in near-sorted case  
	- Minimal memory usage  
	- Low overhead  

---
## Scenario 2: Large Customer Dataset (1 Million Records, Random)

- Key Constraints  
	- Very large dataset  
	- Random order  
	- Memory efficiency important  

- Best Choice: QuickSort  
	- Alternative: Merge Sort (if stability required)  

- Justification  
	- Average O(n log n) performance  
	- Lower memory usage than Merge Sort  
	- Merge Sort only necessary if stable ordering is critical  

---
## Scenario 3: Real-Time Financial Data (Frequent Updates)

- Key Constraints  
	- Continuous updates  
	- Predictable performance required  
	- Stability important  

- Best Choice: Merge Sort  

- Justification  
	- Guaranteed O(n log n) runtime  
	- Stable sorting  
	- Handles merging of new data efficiently  

---
## Scenario 4: User Comments (Thousands, Frequent Additions)

- Key Constraints  
	- Growing dataset  
	- Stability required (timestamp order)  
	- Predictable performance  

- Best Choice: Merge Sort  

- Justification  
	- Stable  
	- Reliable O(n log n)  
	- Efficient for continuous merging  

---
## Scenario 5: Video Recommendations (Millions, Mostly Sorted)

- Key Constraints  
	- Very large dataset  
	- Mostly pre-sorted  
	- Occasional updates  

- Best Choice  
	- Bubble Sort (if strongly near-sorted)  
	- QuickSort (if more random than expected)  

- Justification  
	- Bubble Sort can reach O(n) when nearly sorted  
	- QuickSort balances performance and memory for large datasets  
	- Merge Sort unnecessary due to extra memory cost  

---
## Scenario 6: Real-Time Product Sorting (Thousands of Items)

- Key Constraints  
	- Frequent updates to prices and inventory  
	- Users require immediate sorted results  
	- Memory efficiency important  

- Best Choice: QuickSort  

- Justification  
	- Fast average-case performance (O(n log n))  
	- In-place sorting uses minimal memory  
	- Handles frequent dynamic updates efficiently  

---
## Scenario 7: User Authentication Logs (Millions of Records)

- Key Constraints  
	- Logs must be sorted by timestamp  
	- Stability required  
	- Large dataset  

- Best Choice: Merge Sort  

- Justification  
	- Stable sorting preserves identical timestamps  
	- Consistent O(n log n) performance  
	- Handles large-scale logs reliably  

---
## Scenario 8: Sorting Customer Reviews (Hundreds of Thousands)

- Key Constraints  
	- Sort by rating  
	- Stability not required  
	- Efficient performance critical  

- Best Choice: QuickSort  

- Justification  
	- Fast average-case performance (O(n log n))  
	- Minimal memory usage compared to Merge Sort  
	- Stability unnecessary, so QuickSort outperforms Merge Sort  

---
## Core Decision Patterns

- Large dataset + stability required → Merge Sort  
- Large dataset + stability not required → QuickSort  
- Very small dataset → Bubble Sort  
- Memory-constrained environment → Avoid Merge Sort  
- Real-time sorting → QuickSort preferred  