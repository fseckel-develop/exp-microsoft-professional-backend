
- Objective  
	- Analyse structured logs to identify performance patterns and recurring errors  
	- Practice scanning logs systematically before using advanced tooling  
- Focus Areas  
	- Performance-related patterns (duration analysis)  
	- Recurring errors (error-level entries)  
	- Log structure consistency and usefulness  

---
## Example 1: Logging for an E-Commerce Web App

```JSON
{
  "timestamp": "2024-04-10T14:21:03Z",
  "level": "info",
  "route": "/products",
  "status": 200,
  "duration": "120ms"
}
{
  "timestamp": "2024-04-10T14:21:05Z",
  "level": "info",
  "route": "/checkout",
  "status": 200,
  "duration": "340ms"
}
{
  "timestamp": "2024-04-10T14:21:09Z",
  "level": "error",
  "route": "/cart",
  "status": 500,
  "message": "NullReferenceException at CartController.cs:57"
}
{
  "timestamp": "2024-04-10T14:21:10Z",
  "level": "info",
  "route": "/checkout",
  "status": 200,
  "duration": "362ms"
}
{
  "timestamp": "2024-04-10T14:21:14Z",
  "level": "error",
  "route": "/cart",
  "status": 500,
  "message": "NullReferenceException at CartController.cs:57"
}
```

### Step 1: Performance Patterns

- Review entries containing the `"duration"` field  
- Compare response times across routes  

- Observations  
	- `/products`  
		- Duration: 120ms  
		- Acceptable response time  
	- `/checkout`  
		- Durations: 340ms, 362ms  
		- Consistently above 300ms  
		- Noticeably slower than other routes  

- Conclusion  
	- `/checkout` shows consistent performance degradation  
	- Likely candidate for optimization or deeper investigation  

### Step 2: Recurring Errors

- Review entries with `"level": "error"`  

- Observations  
	- Route: `/cart`  
		- Error: `NullReferenceException at CartController.cs:57`  
		- Occurred twice  
		- Same file and line number  

- Conclusion  
	- Reproducible bug in `CartController.cs` (line 57)  
	- Not random — indicates a consistent failure point  

### Step 3: Log Structure Evaluation

- Log format characteristics  
	- Consistent key-value structure  
	- Clearly labeled fields:  
		- `timestamp`  
		- `level`  
		- `route`  
		- `status`  
		- `duration` or `message`  

- Why this matters  
	- Easy visual scanning  
	- Simple filtering by route or error level  
	- Suitable for automated monitoring and dashboards  

- Overall Findings  
	- `/checkout` → Potential performance issue  
	- `/cart` → Repeated, identifiable bug  
	- Structured format → Enables efficient pattern detection  


---
## Example 2: Logging for a Search Engine

```JSON
{
  "timestamp": "2024-05-01T10:00:01Z",
  "level": "info",
  "route": "/login",
  "status": 200,
  "duration": "85ms"
}
{
  "timestamp": "2024-05-01T10:00:03Z",
  "level": "info",
  "route": "/search",
  "status": 200,
  "duration": "422ms"
}
{
  "timestamp": "2024-05-01T10:00:04Z",
  "level": "error",
  "route": "/search",
  "status": 500,
  "message": "TimeoutException at SearchService.cs:102"
}
{
  "timestamp": "2024-05-01T10:00:06Z",
  "level": "info",
  "route": "/login",
  "status": 200,
  "duration": "89ms"
}
{
  "timestamp": "2024-05-01T10:00:08Z",
  "level": "info",
  "route": "/search",
  "status": 200,
  "duration": "438ms"
}
{
  "timestamp": "2024-05-01T10:00:09Z",
  "level": "error",
  "route": "/search",
  "status": 500,
  "message": "TimeoutException at SearchService.cs:102"
}
```

### Step 1: Performance Patterns

- Review `"duration"` values across routes  

- Observations  
	- `/login`  
		- Durations: 85ms, 89ms  
		- Fast and consistent  
	- `/search`  
		- Durations: 422ms, 438ms  
		- Significantly slower than `/login`  
		- Appears multiple times with high latency  

- Conclusion  
	- `/search` is consistently slow  
	- Indicates possible performance bottleneck  

### Step 2: Recurring Errors

- Review `"level": "error"` entries  

- Observations  
	- Route: `/search`  
		- Error: `TimeoutException at SearchService.cs:102`  
		- Occurred multiple times  
		- Same file and line reference  

- Conclusion  
	- Repeated timeout issue in `SearchService.cs` (line 102)  
	- Possible causes:  
		- Slow database queries  
		- External service delay  
		- Resource contention  

### Step 3: Log Structure Evaluation

- Log format characteristics  
	- Consistent structure across entries  
	- Clearly defined fields:  
		- `timestamp`  
		- `level`  
		- `route`  
		- `status`  
		- `duration`  
		- `message`  

- Effectiveness  
	- Easy to identify slow endpoints  
	- Simple detection of repeated errors  
	- Well-suited for automation and dashboard visualization  

- Overall Findings  
	- `/search` → Performance bottleneck and repeated timeout error  
	- `/login` → Stable and fast  
	- Structured logging → Enables rapid issue identification and system insight  