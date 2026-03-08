## Objective 

- Analyse characteristics of Linear Search and Binary Search  
- Determine when each algorithm is most effective  
- Apply understanding to real-world scenarios  
- Justify algorithm choice using complexity reasoning  

---
## Core Algorithm Characteristics

- **Linear Search**:
	- Sequentially checks each element  
	- Works on sorted and unsorted datasets  
	- No preprocessing required  
	- Time Complexity  
		- Worst case: O(n)  
	- Space Complexity  
		- O(1) (constant memory)  
	- Strengths  
		- Simple implementation  
		- Effective for small datasets  
		- Suitable for frequently changing data  
	- Limitations  
		- Inefficient for large datasets  

- **Binary Search**:
	- Repeatedly divides sorted dataset in half  
	- Requires data to be sorted  
	- Uses left, right, and midpoint tracking  
	- Time Complexity  
		- O(log n)  
	- Space Complexity  
		- O(1) (iterative implementation)  
	- Strengths  
		- Highly efficient for large datasets  
		- Scales well as input size increases  
	- Limitations  
		- Cannot operate on unsorted data  
		- Sorting may add O(n log n) preprocessing cost  

- **Key Decision Factors**:
	- Is the dataset sorted?  
	- How large is the dataset?  
	- Does the dataset change frequently?  
	- Is preprocessing (sorting) acceptable?  

---
## Scenario 1: Unsorted Product Catalog (50 Items)

- Dataset Characteristics  
	- Small size  
	- Unsorted  
	- No need for frequent repeated searches  

- Algorithm Comparison  
	- Linear Search  
		- O(n) acceptable for 50 items  
		- No sorting required  
	- Binary Search  
		- Requires sorting first (O(n log n))  
		- Sorting cost unnecessary  

- Best Choice  
	- Linear Search  

- Justification  
	- Sorting introduces unnecessary overhead  
	- Linear search is efficient enough for small lists  
	- Simplicity outweighs theoretical speed advantage  

---
## Scenario 2: Customer ID in Sorted Database (500,000 Records)

- Dataset Characteristics  
	- Very large dataset  
	- Already sorted  
	- Fast lookup required  

- Algorithm Comparison  
	- Linear Search  
		- O(n) could require scanning hundreds of thousands of records  
		- Too slow for large-scale systems  
	- Binary Search  
		- O(log n) dramatically reduces comparisons  
		- Efficient for large sorted datasets  

- Best Choice  
	- Binary Search  

- Justification  
	- Dataset is already sorted  
	- Logarithmic time complexity ensures fast lookup  
	- Scales efficiently with database growth  

---
## Scenario 3: Frequently Changing Playlist

- Dataset Characteristics  
	- Dynamically updated  
	- Songs added/removed often  
	- Sorting after each change is expensive  

- Algorithm Comparison  
	- Linear Search  
		- Works without sorting  
		- O(n) acceptable for small-to-medium playlists  
	- Binary Search  
		- Requires re-sorting after every modification  
		- Sorting cost outweighs search benefit  

- Best Choice  
	- Linear Search  

- Justification  
	- Avoids repeated sorting overhead  
	- Better suited for dynamic datasets  
	- Maintains system efficiency  

---
## Scenario 4: Help Desk Ticket System

- Dataset Characteristics  
	- Tickets stored in order received  
	- Frequently updated  
	- Not sorted by keyword  

- Algorithm Comparison  
	- Linear Search  
		- Works on unsorted data  
		- No sorting overhead  
	- Binary Search  
		- Requires constant re-sorting  
		- Inefficient for dynamic ticket system  

- Best Choice  
	- Linear Search  

- Justification  
	- Dataset is not sorted  
	- Sorting would slow system performance  
	- Linear search supports frequent updates  

---
## Scenario 5: Username Lookup in Sorted List (1,000,000 Users)

- Dataset Characteristics  
	- Very large dataset  
	- Already sorted  
	- Fast login verification required  

- Algorithm Comparison  
	- Linear Search  
		- O(n) inefficient for 1 million records  
	- Binary Search  
		- O(log n) drastically reduces comparisons  
		- Highly scalable  

- Best Choice  
	- Binary Search  

- Justification  
	- Sorted dataset enables divide-and-conquer approach  
	- Logarithmic performance ensures fast authentication  
	- Significantly more efficient at scale  

---
## Overall Insight

- Use Linear Search when  
	- Dataset is small  
	- Data is unsorted  
	- Data changes frequently  
	- Simplicity is prioritized  

- Use Binary Search when  
	- Dataset is large  
	- Data is sorted  
	- Fast repeated lookups are required  
	- Scalability is critical  

- Core Principle  
	- Algorithm efficiency depends on dataset structure and scale  
	- Sorting overhead must be considered before choosing binary search  